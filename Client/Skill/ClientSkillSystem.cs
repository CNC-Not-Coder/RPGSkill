

using DataTableSpace;
using System.Collections.Generic;

namespace RPGSkill
{
    public class ClientSkillSystem
    {
        private List<SkillInstance> m_ActiveSkills = new List<SkillInstance>();
        private Dictionary<int, List<SkillInstance>> m_UnActiveSkills = new Dictionary<int, List<SkillInstance>>();
        public void StartSkill(int skillId, int sender, int target, float cast_x, float cast_y, float cast_z)
        {
            if (skillId == -1 || sender == -1)
                return;

            SkillInstance instance = GetUnActiveSkillInstance(skillId);
            if (instance != null)
            {
                m_ActiveSkills.Add(instance);
                instance.Reset();
                instance.Start(sender, target, cast_x, cast_y, cast_z);
            }
        }

        public void StopSkill(int skillId)
        {
            SkillInstance instance = m_ActiveSkills.Find(p => p.GetId() == skillId);
            if (instance != null)
            {
                instance.Stop();
            }
        }

        public void Tick()
        {
            long delta = 0;//TODO:
            int ct = m_ActiveSkills.Count;
            for (int i = ct - 1; i >= 0; i--)
            {
                SkillInstance instance = m_ActiveSkills[i];
                if (instance.IsActive)
                {
                    instance.Tick(delta);
                }
                else
                {
                    m_ActiveSkills.RemoveAt(i);
                    AddUnActiveSkillInstance(instance);
                }
            }
        }
        protected void AddUnActiveSkillInstance(SkillInstance instance)
        {
            if (instance.IsActive)
                return;
            int skillId = instance.GetId();
            if (m_UnActiveSkills.ContainsKey(skillId))
            {
                List<SkillInstance> skills = m_UnActiveSkills[skillId];
                skills.Add(instance);
            }
            else
            {
                List<SkillInstance> list = new List<SkillInstance>();
                list.Add(instance);
                m_UnActiveSkills.Add(skillId, list);
            }

        }
        protected SkillInstance GetUnActiveSkillInstance(int skillId)
        {
            if (m_UnActiveSkills.ContainsKey(skillId))
            {
                List<SkillInstance> skills = m_UnActiveSkills[skillId];
                int ct = skills.Count;
                if (ct > 0)
                {
                    SkillInstance ret = skills[ct - 1];
                    skills.RemoveAt(ct - 1);
                    return ret;
                }
            }

            return GetNewSkillInstance(skillId);
        }
        protected SkillInstance GetNewSkillInstance(int skillId)
        {
            Tab_SkillData data = Tab_SkillDataProvider.Instance.GetDataById(skillId);
            if (data == null)
                return null;

            List<int> RuleIds = new List<int>(data.RuleIdList);
            List<SkillComponent> list = new List<SkillComponent>();
            list.AddRange(LoadSkillComponent<EffectComponent>(data.EffectIdList));
            list.AddRange(LoadSkillComponent<SoundComponent>(data.SoundIdList));
            list.AddRange(LoadSkillComponent<AnimationComponent>(data.AnimationIdList));
            list.AddRange(LoadSkillComponent<MoveComponent>(data.MoveIdList));

            SkillInstance instance = new SkillInstance();
            if (instance.Init(skillId, list))
            {
                return instance;
            }
            return null;
        }
        protected List<T> LoadSkillComponent<T>(List<int> ids) where T : SkillComponent, new()
        {
            List<T> list = new List<T>();
            int ct = ids.Count;
            for (int i = 0; i < ct; i++)
            {
                T component = new T();
                component.Init(ids[i]);
                list.Add(component);
            }

            return list;
        }
    }
}
