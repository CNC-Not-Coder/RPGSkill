
/*
 * Buffer实例类，和技能实例基本一样，Buffer可以叠加
 * Usage:
 * Init --> Start
 * Reset --> Start
 * 对于子弹类型的Buffer需要根据位移进行时间缩放
 * */
using System.Collections.Generic;
namespace RPGSkill
{
    public class BufferInstance
    {
        private int m_Id = -1;
        private bool m_IsActive = false;

        private float m_ExecSpeed;
        private long m_curTime = 0;
        private List<SkillComponent> m_Components = new List<SkillComponent>();
        private InstanceData m_InstanceData = new InstanceData();

        public int SenderId
        {
            get { return m_InstanceData.SenderId; }
        }
        public int TargetId
        {
            get { return m_InstanceData.TargetId; }
        }

        public bool IsActive
        {
            get { return m_IsActive; }
        }

        public int GetId()
        {
            return m_Id;
        }

        public float ExecSpeed
        {
            get { return m_ExecSpeed; }
            set { m_ExecSpeed = value; }
        }

        public bool Init(int skillId, List<SkillComponent> components)
        {
            m_Id = skillId;
            m_Components = new List<SkillComponent>();
            m_Components.AddRange(components);

            return Load(skillId);
        }
        protected bool Load(int skillId)
        {
            return true;
        }
        public void Start(int sender, int target)
        {
            Reset();
            m_IsActive = true;

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
            m_IsActive = false;
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
            m_ExecSpeed = 1f;
            m_curTime = 0;
            m_IsActive = false;
            m_InstanceData = new InstanceData();

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
                int activeCt = 0;
                int ct = m_Components.Count;
                for (int i = 0; i < ct; i++)
                {
                    if (m_Components[i].IsActive)
                    {
                        activeCt++;
                        bool isContinue = m_Components[i].Tick(deltaTime, m_curTime, m_InstanceData);
                        if (!isContinue) m_Components[i].IsActive = false;
                    }
                }
                if (activeCt < 1)
                {
                    Stop();
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
        public InstanceData GetInstanceData()
        {
            return m_InstanceData;
        }
    }
}
