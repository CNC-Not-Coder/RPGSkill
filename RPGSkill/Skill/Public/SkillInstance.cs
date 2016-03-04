
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
        private int m_SkillId;
        private List<int> m_RuleIds;
        private List<int> m_EffectIds;
        private List<int> m_SoundIds;
        private List<int> m_AnimIds;

        private float m_SkillSpeed;
        private long m_curTime;
        private List<SkillComponent> m_Components;

        public int SenderId;
        public int TargetId;
        public bool IsActive;

        public int GetId()
        {
            return m_SkillId;
        }

        public bool Init(int skillId)
        {
            m_SkillId = skillId;

            return Load(skillId);
        }
        protected bool Load(int skillId)
        {
            Tab_SkillData data = Tab_SkillDataProvider.Instance.GetDataById(skillId);
            if (data == null)
                return false;
            m_RuleIds = new List<int>(data.RuleIdList);
            m_EffectIds = new List<int>(data.EffectIdList);
            m_SoundIds = new List<int>(data.SoundIdList);
            m_AnimIds = new List<int>(data.AnimationIdList);

            m_Components = new List<SkillComponent>();

            //TODO:区分服务器和客户端
            //if(IsServer)...
            int ct = m_RuleIds.Count;
            for (int i = 0; i < ct; i++)
            {
                if (m_RuleIds[i] == -1) break;
                RuleComponent rc = new RuleComponent();
                rc.Init(m_RuleIds[i]);
                AddComponent(rc);
            }
            ct = m_EffectIds.Count;
            for (int i = 0; i < ct; i++)
            {
                if (m_EffectIds[i] == -1) break;
                EffectComponent ec = new EffectComponent();
                ec.Init(m_EffectIds[i]);
                AddComponent(ec);
            }
            ct = m_SoundIds.Count;
            for (int i = 0; i < ct; i++)
            {
                if (m_SoundIds[i] == -1) break;
                SoundComponent sc = new SoundComponent();
                sc.Init(m_SoundIds[i]);
                AddComponent(sc);
            }
            ct = m_AnimIds.Count;
            for (int i = 0; i < ct; i++)
            {
                if (m_AnimIds[i] == -1) break;
                AnimationComponent ac = new AnimationComponent();
                ac.Init(m_AnimIds[i]);
                AddComponent(ac);
            }

            return true;
        }
        public void Start(int sender, int target)
        {
            Reset();
            IsActive = true;
            SenderId = sender;
            TargetId = target;
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
            m_SkillSpeed = 1f;
            m_curTime = 0;
            SenderId = -1;
            TargetId = -1;
            IsActive = false;

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
            if(m_Components != null)
            {
                int ct = m_Components.Count;
                for (int i = 0; i < ct; i++)
                {
                    if (m_Components[i].IsActive)
                    {
                        bool isContinue = m_Components[i].Tick(deltaTime);
                        if (!isContinue) m_Components[i].IsActive = false;
                    }
                }
            }
        }
        protected void AddComponent(SkillComponent component)
        {
            if (component == null)
                return;
            m_Components.Add(component);
            component.SkillInst = this;
        }
        public long CurTime
        {
            get { return m_curTime; }
        }
    }
}
