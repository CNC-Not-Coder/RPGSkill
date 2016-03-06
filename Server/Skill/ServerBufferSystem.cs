

using System.Collections.Generic;
namespace RPGSkill
{
    public class ServerBufferSystem
    {
        private Dictionary<int, List<BufferInstance>> m_Buffers = new Dictionary<int, List<BufferInstance>>();
        private static ServerBufferSystem s_Instance = new ServerBufferSystem();
        
        public static ServerBufferSystem Instance
        {
            get { return s_Instance; }
        }
        public void SendBufferToTarget(int bufferId, int sender, int targetId)
        {
            //如果是子弹，这里需要根据距离计算buffer的时间缩放
            //BufferInstance.ExecSpeed = ...

            //同步给客户端

        }
        public List<BufferData> GetBufferDatasByObjId(int objId)
        {
            List<BufferData> list = new List<BufferData>();
            if(m_Buffers.ContainsKey(objId))
            {
                List<BufferInstance> buffs = m_Buffers[objId];
                int ct = buffs.Count;
                for (int i = 0; i < ct; i++)
                {
                    InstanceData inst = buffs[i].GetInstanceData();
                    BufferData data = inst.CustomData.GetData<BufferData>();
                    if (data != null)
                        list.Add(data);
                }
            }
            return list;
        }
        public void Tick()
        {
            long delta = 0;//TODO:
            foreach(KeyValuePair<int, List<BufferInstance>> pair in m_Buffers)
            {
                List<BufferInstance> buffs = pair.Value;
                int ct = buffs.Count;
                for (int i = 0; i < ct; i++)
                {
                    buffs[i].Tick(delta);
                }
            }
        }
    }
}
