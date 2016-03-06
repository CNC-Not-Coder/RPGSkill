

/*
 * 目标选取规则
 * 负责按照指定的参数设定选取符合条件的目标，然后发送相应的BufferList
 * */
using DataTableSpace;
using System.Collections.Generic;
namespace RPGSkill
{
    public class RuleComponent : SkillComponent
    {
        public int Id = -1;
        public int LogicId = -1;
        public List<int> Buffers = new List<int>();

        private IRuleLogic m_Logic = null;
        private RuleData m_RuleData = new RuleData();

        public int SenderId = -1;
        public int TargetId = -1;

        private bool IsFirstAdd = false;
        
        public override void Init(int id)
        {
            Tab_RuleData data = Tab_RuleDataProvider.Instance.GetDataById(id);
            if (data == null)
                return;

            Id = id;
            LogicId = data.LogicId;
            startTime = data.StartTime;

            RuleLogicType ruleType = (RuleLogicType)LogicId;
            m_RuleData = new RuleData();
            m_RuleData.RuleId = id;
            IRuleLogic logic = RuleLogicManager.Instance.GetRuleByType(ruleType);
            m_Logic = logic;

            m_Logic.ParseData(m_RuleData, data.ParamList);
        }
        public override void Reset()
        {
            IsFirstAdd = false;
            base.Reset();
        }
        public override bool Tick(long deltaTime, long curTime, InstanceData instanceData)
        {
            if (curTime < startTime)
                return true;
            if (m_Logic == null || m_RuleData == null)
                return false;
            if(IsFirstAdd)
            {
                IsFirstAdd = false;
                SenderId = instanceData.SenderId;
                TargetId = instanceData.TargetId;
                m_RuleData.CustomData.Clear();
            }
            
            List<int> targets = m_Logic.GetRuleResult(m_RuleData, this);
            //给目标发送BUFFER
            int ct = targets.Count;
            for (int i = 0; i < ct; i++)
            {
                int len = Buffers.Count;
                for (int j = 0; j < len; j++)
                {
                    ServerBufferSystem.Instance.SendBufferToTarget(Buffers[j], instanceData.InstanceId, SenderId, TargetId);
                }
            }
            
            return false;
        }
        public override void Start()
        {
            IsFirstAdd = true;
            base.Start();
        }
        public override void Stop()
        {
            base.Stop();
        }
    }
}
