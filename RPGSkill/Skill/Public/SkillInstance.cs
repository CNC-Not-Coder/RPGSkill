

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

        public int GetId()
        {
            return m_SkillId;
        }

        public bool Init(int skillId)
        {
            Reset();
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

            //区分服务器和客户端
            //if(IsServer)...
            int ct = m_RuleIds.Count;
            for (int i = 0; i < ct; i++)
            {
                if (m_RuleIds[i] == -1) break;
                RuleComponent rc = new RuleComponent();
                rc.Init(m_RuleIds[i]);
                m_Components.Add(rc);
            }
            ct = m_EffectIds.Count;
            for (int i = 0; i < ct; i++)
            {
                if (m_EffectIds[i] == -1) break;
                EffectComponent ec = new EffectComponent();
                ec.Init(m_EffectIds[i]);
                m_Components.Add(ec);
            }
            ct = m_SoundIds.Count;
            for (int i = 0; i < ct; i++)
            {
                if (m_SoundIds[i] == -1) break;
                SoundComponent sc = new SoundComponent();
                sc.Init(m_SoundIds[i]);
                m_Components.Add(sc);
            }
            ct = m_AnimIds.Count;
            for (int i = 0; i < ct; i++)
            {
                if (m_AnimIds[i] == -1) break;
                AnimationComponent ac = new AnimationComponent();
                ac.Init(m_AnimIds[i]);
                m_Components.Add(ac);
            }

            return true;
        }
        public void Start()
        {

        }
        public void Stop()
        {

        }
        public void Reset()
        {
            m_SkillSpeed = 1f;
            m_curTime = 0;
        }
        /// <summary>
        /// deltaTime in ms
        /// </summary>
        /// <param name="deltaTime"></param>
        public void Tick(long deltaTime)
        {
            if(m_Components != null)
            {
                int ct = m_Components.Count;
                for (int i = 0; i < ct; i++)
                {
                    m_Components[i].Tick(deltaTime);
                }
            }
        }
    }
}
