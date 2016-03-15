
/*
 * Usage:
 * Init --> Start
 * Reset --> Start
 * 可以考虑缓存起来
 * */
using DataTableSpace;
using System.Collections.Generic;

namespace RPGSkill
{
    public class SkillInstance
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
        public void Start(int sender, int target, float cast_x, float cast_y, float cast_z)
        {
            Reset();
            m_IsActive = true;

            m_InstanceData = new InstanceData();
            m_InstanceData.SenderId = sender;
            m_InstanceData.TargetId = target;
            m_InstanceData.InstanceId = m_Id;
            m_InstanceData.Cast_x = cast_x;
            m_InstanceData.Cast_y = cast_y;
            m_InstanceData.Cast_z = cast_z;
            m_InstanceData.SkillId = m_Id;

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

            if(m_Components != null)
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
            if(m_Components != null)
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
