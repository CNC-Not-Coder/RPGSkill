

using DataTableSpace;
using System.Collections.Generic;
namespace RPGSkill
{
    public class BufferComponent : SkillComponent
    {
        public int Id = -1;
        public int LogicId = -1;
        public int Delay = 0;

        private bool IsFirstAdd = false;
        private BufferData m_BufferData = null;
        private IBufferLogic m_Logic = null;
        public override void Init(int id)
        {//数据加载
            Tab_BufferData data = Tab_BufferDataProvider.Instance.GetDataById(id);
            if (data == null)
                return;
            Id = id;
            LogicId = data.LogicId;
            Delay = data.Delay;

            m_BufferData = new BufferData();
            m_BufferData.BufferId = id;
            m_BufferData.LogicId = LogicId;

            BufferLogicType logicType = (BufferLogicType)LogicId;
            IBufferLogic logic = BufferLogicManager.Instance.GetBufferLogicByType(logicType);
            logic.ParseData(m_BufferData, data.ParamList);

            m_Logic = logic;
        }
        public override void Reset()
        {//重置临时数据
            IsActive = false;
            IsFirstAdd = false;
        }
        public override bool Tick(long deltaTime, long curTime, InstanceData instanceData)
        {
            if (curTime < Delay)
                return true;
            //第一次添加，对Target上所有的Buffer调用一次叠加逻辑
            if(IsFirstAdd)
            {
                IsFirstAdd = false;
                m_BufferData.SenderId = instanceData.SenderId;
                m_BufferData.TargetId = instanceData.TargetId;
                m_BufferData.CustomData.Clear();
                //获得Target上的所有BufferData
                List<BufferData> buffers = ServerBufferSystem.Instance.GetBufferDatasByObjId(m_BufferData.TargetId);
                if(buffers != null)
                {
                    int ct = buffers.Count;
                    for (int i = 0; i < ct; i++)
                    {
                        IBufferLogic logic = BufferLogicManager.Instance.GetBufferLogicByType((BufferLogicType)buffers[i].LogicId);
                        if(logic != null)
                        {
                            bool isContinue = logic.OnOtherBuffer(buffers[i]);
                            if (!isContinue) break;
                        }
                    }
                }
                if (m_Logic != null)
                {
                    m_Logic.Start(m_BufferData);
                }
                instanceData.CustomData.AddData(m_BufferData);
            }
            //例行检查状态
            if(m_Logic != null)
            {
                m_Logic.Tick(m_BufferData);
            }
            return false;
        }
        public override void Start()
        {
            IsActive = true;
            IsFirstAdd = true;
        }
        public override void Stop()
        {
            IsActive = false;
        }
    }
}
