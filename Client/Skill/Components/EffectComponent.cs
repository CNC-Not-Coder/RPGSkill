
/*
 * 特效播放组件
 * 负责给目标挂接特效，对于子弹特效，需要根据发送者和接收者以及延时做位移处理
 * */
using DataTableSpace;
using UnityEngine;

namespace RPGSkill
{
    public class EffectComponent : SkillComponent
    {
        private int m_Id = -1;
        private int m_RemainTime = 0;
        private string m_Resource = "";
        private string m_Bone = "";
        private bool m_IsAttach = false;
        private Vector3 m_Position = Vector3.zero;
        private Vector3 m_Rotation = Vector3.zero;
        private Vector3 m_Scale = Vector3.one;
        private bool m_UseOnCastPoint = false;
        private bool m_UseOnTarget = true;
        private bool m_IsBullet = false;
        private int m_BulletTime = 0;
        public override void Init(int id)
        {//加载数据
            Tab_EffectData data = Tab_EffectDataProvider.Instance.GetDataById(id);
            if (data == null)
                return;
            m_Id = id;
            startTime = data.StartTime;
            m_RemainTime = data.DeleteTime;
            m_Resource = data.Resource;
            m_Bone = data.Bone;
            m_IsAttach = data.IsAttach;
            m_Position = ComponentUtil.StringToVec3(data.Position);
            m_Rotation = ComponentUtil.StringToVec3(data.Rotation);
            m_Scale = ComponentUtil.StringToVec3(data.Scale);
            m_UseOnCastPoint = data.UseOnCastPoint;
            m_UseOnTarget = data.UseOnTarget;
            m_IsBullet = data.IsBullet;
            m_BulletTime = data.BulletTime;
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override bool Tick(long deltaTime, long curTime, InstanceData instanceData)
        {
            if (curTime < startTime)
                return true;
            if(m_UseOnCastPoint)
            {//场景特效，只和传入的释放点有关
                Vector3 castPoint = new Vector3(instanceData.Cast_x, instanceData.Cast_y, instanceData.Cast_z);
                GameObject sender = ComponentUtil.GetGameObjectById(instanceData.SenderId);
                if (sender != null)
                {
                    ApplyOnCastPoint(sender, castPoint);
                }
            }
            else
            {
                int objId = -1;
                if (m_UseOnTarget)
                {
                    objId = instanceData.TargetId;
                }
                else
                {
                    objId = instanceData.SenderId;
                }
                GameObject target = ComponentUtil.GetGameObjectById(objId);
                if (target != null)
                {
                    ApplyOnObj(target);
                }
            }

            return false;
        }
        protected void ApplyOnObj(GameObject target)
        {
            GameObject effectObj = ComponentUtil.InstantiateEffect(m_Resource, m_RemainTime);
            if (effectObj == null)
                return;
            Transform bone = target.transform.Find(m_Bone);
            if (bone == null)
                return;
            effectObj.transform.parent = bone;
            effectObj.transform.localPosition = m_Position;
            effectObj.transform.localRotation = Quaternion.Euler(m_Rotation);
            effectObj.transform.localScale = m_Scale;
            if (!m_IsAttach)
            {
                effectObj.transform.parent = null;
            }
            if (m_IsBullet)
            {//挂接子弹脚本，给定时间内从当前位置运动到目标的 腰结点

            }
        }
        protected void ApplyOnCastPoint(GameObject senderObj, Vector3 castPoint)
        {//此时只有position,rotation,scale参数有效，而且默认以sender的朝向为参考方向
            GameObject effectObj = ComponentUtil.InstantiateEffect(m_Resource, m_RemainTime);
            if (effectObj == null)
                return;
            effectObj.transform.parent = senderObj.transform;
            Vector3 localOffset = senderObj.transform.InverseTransformPoint(castPoint);
            effectObj.transform.localPosition = localOffset + m_Position;
            effectObj.transform.localRotation = Quaternion.Euler(m_Rotation);
            effectObj.transform.localScale = m_Scale;
            effectObj.transform.parent = null;
        }
    }
}
