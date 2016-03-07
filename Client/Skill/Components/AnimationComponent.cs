

using DataTableSpace;
using UnityEngine;

namespace RPGSkill
{
    public class AnimationComponent : SkillComponent
    {
        private int m_Id = -1;
        private string m_ClipName = "";
        private int m_Layer = 0;
        private float m_Speed = 1f;
        private float m_Weight = 1f;
        private WrapMode m_WrapMode = WrapMode.Default;
        private AnimationBlendMode m_BlendMode = AnimationBlendMode.Blend;
        private float m_CrossTime = 0f;
        private bool m_UseOnTarget = true;
        public override void Init(int id)
        {
            Tab_AnimationData data = Tab_AnimationDataProvider.Instance.GetDataById(id);
            if (data == null)
                return;
            m_Id = id;
            startTime = data.StartTime;
            m_ClipName = data.ClipName;
            m_Layer = data.Layer;
            m_Speed = data.Speed;
            m_Weight = data.Weight;
            m_WrapMode = ComponentUtil.StringToWrapMode(data.WrapMode);
            m_BlendMode = ComponentUtil.StringToBlendMode(data.BlendMode);
            m_CrossTime = data.CrossTime;
            m_UseOnTarget = data.UseOnTarget;
        }
        public override bool Tick(long deltaTime, long curTime, InstanceData instanceData)
        {
            if (curTime < startTime)
                return true;
            int objId = -1;
            if(m_UseOnTarget)
            {
                objId = instanceData.TargetId;
            }
            else
            {
                objId = instanceData.SenderId;
            }
            GameObject go = ComponentUtil.GetGameObjectById(objId);
            if(go != null)
            {
                ApplyOnObj(go);
            }

            return false;
        }
        protected void ApplyOnObj(GameObject go)
        {
            Animation anim = go.GetComponent<Animation>();
            if (anim == null)
                return;
            AnimationClip clip = anim.GetClip(m_ClipName);
            if (clip == null)
                return;
            AnimationState state = anim[m_ClipName];
            state.layer = m_Layer;
            state.speed = m_Speed;
            state.weight = m_Weight;
            state.wrapMode = m_WrapMode;
            state.blendMode = m_BlendMode;
            if(m_CrossTime > 0f)
            {
                anim.CrossFade(m_ClipName, m_CrossTime, PlayMode.StopSameLayer);
            }
            else
            {
                anim.Play(m_ClipName, PlayMode.StopSameLayer);
            }
        }
    }
}
