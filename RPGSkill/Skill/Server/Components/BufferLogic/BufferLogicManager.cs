

using System.Collections.Generic;
namespace RPGSkill
{
    public class BufferLogicManager
    {
        private Dictionary<BufferLogicType, IBufferLogic> m_Buffers = new Dictionary<BufferLogicType, IBufferLogic>();
        private static BufferLogicManager s_Instance = new BufferLogicManager();
        public static BufferLogicManager Instance
        {
            get { return s_Instance; }
        }
        public void Init()
        {
            RegisterBuffer(BufferLogicType.OneTime, new OneTimeBuffer());
            RegisterBuffer(BufferLogicType.Interval, new IntervalBuffer());
            RegisterBuffer(BufferLogicType.Circular, new CircularBuffer());
        }
        protected void RegisterBuffer(BufferLogicType logicType, IBufferLogic logic)
        {
            m_Buffers.Add(logicType, logic);
        }

        public IBufferLogic GetBufferLogicByType(BufferLogicType logicType)
        {
            IBufferLogic ret;
            if (m_Buffers.TryGetValue(logicType, out ret))
            {
                return ret;
            }
            return null;
        }
    }
}
