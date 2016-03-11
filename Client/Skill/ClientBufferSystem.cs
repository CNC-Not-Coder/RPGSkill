

using DataTableSpace;
using System.Collections.Generic;
namespace RPGSkill
{
    class ClientBufferSystem
    {
        private Dictionary<int, List<BufferInstance>> m_Buffers = new Dictionary<int, List<BufferInstance>>();
        private Dictionary<int, List<BufferInstance>> m_UnActiveBuffers = new Dictionary<int, List<BufferInstance>>();

        public void SendBufferToTarget(int bufferId, int skillId, int sender, int target)
        {
            //客户端的Buffer只是负责表现
            if (bufferId == -1 || target == -1)
                return;
            List<BufferInstance> buffs = null;
            if (m_Buffers.ContainsKey(target))
            {
                buffs = m_Buffers[target];
            }
            else
            {
                buffs = new List<BufferInstance>();
            }
            BufferInstance instance = GetUnActiveBufferInstance(bufferId);
            if(instance != null)
            {
                buffs.Add(instance);
                instance.Reset();
                instance.Start(sender, target, skillId);
            }
        }
        public void Tick()
        {
            long delta = 0;//TODO:
            foreach(KeyValuePair<int, List<BufferInstance>> pair in m_Buffers)
            {
                List<BufferInstance> buffs = pair.Value;
                int ct = buffs.Count;
                for (int i = ct - 1; i >= 0; i--)
                {
                    BufferInstance instance = buffs[i];
                    if(instance.IsActive)
                    {
                        instance.Tick(delta);
                    }
                    else
                    {
                        buffs.RemoveAt(i);
                        AddUnActiveBufferInstance(instance);
                    }
                }
            }
        }
        protected void AddUnActiveBufferInstance(BufferInstance instance)
        {
            if (instance.IsActive)
                return;
            int bufferId = instance.GetId();
            if (m_UnActiveBuffers.ContainsKey(bufferId))
            {
                List<BufferInstance> buffers = m_UnActiveBuffers[bufferId];
                buffers.Add(instance);
            }
            else
            {
                List<BufferInstance> list = new List<BufferInstance>();
                list.Add(instance);
                m_UnActiveBuffers.Add(bufferId, list);
            }

        }
        protected BufferInstance GetUnActiveBufferInstance(int bufferId)
        {
            if (m_UnActiveBuffers.ContainsKey(bufferId))
            {
                List<BufferInstance> skills = m_UnActiveBuffers[bufferId];
                int ct = skills.Count;
                if (ct > 0)
                {
                    BufferInstance ret = skills[ct - 1];
                    skills.RemoveAt(ct - 1);
                    return ret;
                }
            }

            return GetNewBufferInstanceInstance(bufferId);
        }
        protected BufferInstance GetNewBufferInstanceInstance(int bufferId)
        {
            Tab_BufferData data = Tab_BufferDataProvider.Instance.GetDataById(bufferId);
            if (data == null)
                return null;

            List<SkillComponent> list = new List<SkillComponent>();
            list.AddRange(LoadSkillComponent<MoveComponent>(data.MoveIdList));
            list.AddRange(LoadSkillComponent<EffectComponent>(data.EffectIdList));
            list.AddRange(LoadSkillComponent<SoundComponent>(data.SoundIdList));
            list.AddRange(LoadSkillComponent<AnimationComponent>(data.AnimationIdList));

            BufferInstance instance = new BufferInstance();
            if (instance.Init(bufferId, list))
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
