

using DataTableSpace;
using System.Collections.Generic;
namespace RPGSkill
{
    /// <summary>
    /// 一个Scene一个BufferSystem，所以这里没有单例
    /// </summary>
    public class ServerBufferSystem
    {
        private Dictionary<int, List<BufferInstance>> m_Buffers = new Dictionary<int, List<BufferInstance>>();
        private Dictionary<int, List<BufferInstance>> m_UnActiveBuffers = new Dictionary<int, List<BufferInstance>>();

        public void SendBufferToTarget(int bufferId, int skillId, int sender, int target)
        {
            //这个暂时不做
            //如果是子弹，这里需要根据距离计算buffer的时间缩放
            //BufferInstance.ExecSpeed = ...
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

            BufferComponent component = new BufferComponent();
            component.Init(bufferId);
            List<SkillComponent> list = new List<SkillComponent>();
            list.Add(component);
            list.AddRange(LoadSkillComponent<MoveComponent>(data.MoveIdList));

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
