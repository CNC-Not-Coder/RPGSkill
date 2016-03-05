
/*
 * Buffer实例类，和技能实例基本一样，Buffer可以叠加
 * Usage:
 * Init --> Start
 * Reset --> Start
 * */
using System.Collections.Generic;
namespace RPGSkill
{
    public class BufferInstance
    {
        private int m_BufferId;
        private int m_LogicId;
        private int m_Delay;
        private bool m_IsBullet;
        private List<int> m_EffectIds;
        private List<int> m_AnimationIds;
        private List<int> m_SoundIds;

        private long m_curTime;
        private float m_BufferSpeed;//对于子弹类型的Buffer需要根据位移进行时间缩放
        private List<SkillComponent> m_Components;
        private InstanceData m_InstanceData;

        public int SenderId;
        public int TargetId;
        public bool IsActive;

        public bool Init(int bufferId)
        {
            m_BufferId = bufferId;
            return Load(bufferId);
        }
        protected bool Load(int bufferId)
        {
            //TODO:区分服务器和客户端，这个方法要摘出去
            return false;
        }
        public void Start(int sender, int target)
        {
            Reset();
            IsActive = true;
            SenderId = sender;
            TargetId = target;

            m_InstanceData = new InstanceData();
            m_InstanceData.SenderId = sender;
            m_InstanceData.TargetId = target;

            if (m_Components != null)
            {
                int ct = m_Components.Count;
                for (int i = 0; i < ct; i++)
                {
                    m_Components[i].Start();
                }
            }
        }
        public void Stop()
        {
            IsActive = false;
            if (m_Components != null)
            {
                int ct = m_Components.Count;
                for (int i = 0; i < ct; i++)
                {
                    m_Components[i].Stop();
                }
            }
        }
        public void Reset()
        {
            m_BufferSpeed = 1f;
            m_curTime = 0;
            SenderId = -1;
            TargetId = -1;
            IsActive = false;
            m_InstanceData = null;

            if (m_Components != null)
            {
                int ct = m_Components.Count;
                for (int i = 0; i < ct; i++)
                {
                    m_Components[i].Reset();
                }
            }
        }
        /// <summary>
        /// deltaTime in ms
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Tick(long deltaTime)
        {
            if (!IsActive)
                return;
            m_curTime += deltaTime;
            if (m_Components != null)
            {
                int ct = m_Components.Count;
                for (int i = 0; i < ct; i++)
                {
                    if (m_Components[i].IsActive)
                    {
                        bool isContinue = m_Components[i].Tick(deltaTime, m_curTime, m_InstanceData);
                        if (!isContinue) m_Components[i].IsActive = false;
                    }
                }
            }
        }
        protected void AddComponent(SkillComponent component)
        {
            if (m_Components == null)
            {
                m_Components = new List<SkillComponent>();
            }
            m_Components.Add(component);
        }
        public long CurTime
        {
            get { return m_curTime; }
        }
    }
}
