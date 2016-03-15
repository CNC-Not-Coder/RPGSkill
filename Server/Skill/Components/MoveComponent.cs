

using DataTableSpace;
using System;
namespace RPGSkill
{
    public class MoveComponent : SkillComponent
    {
        enum CurveType
        {
            Straight = 0, //直线
            Parabolic = 1, //抛物线，固定曲率的
        }
        private int m_Id = -1;
        private bool m_IsLockTarget = true;
        private float m_Distance = 0f;
        private int m_MoveTime = 0;
        private CurveType m_CurType = CurveType.Straight;
        private float m_Direction = 0f;
        private bool m_IsRelativeSender = true;
        private bool m_UseOnTarget = true;
        private bool m_AdjustTowardSender = false;

        private bool m_FirstAdd = false;
        private Vector2 m_SrcPos = Vector2.zero;
        private Vector2 m_DestPos = Vector2.zero;
        public override void Init(int id)
        {
            Tab_MoveData data = Tab_MoveDataProvider.Instance.GetDataById(id);
            if (data == null)
                return;
            m_Id = id;
            startTime = data.StartTime;
            m_IsLockTarget = data.IsLockTarget;
            m_Distance = data.Distance;
            m_MoveTime = data.MoveTime;
            m_CurType = (CurveType)data.CurveType;
            m_Direction = data.Direction;
            m_IsRelativeSender = data.IsRelativeSender;
            m_UseOnTarget = data.UseOnTarget;
            m_AdjustTowardSender = data.AdjustTowardSender;
        }
        public override void Start()
        {
            m_FirstAdd = true;
            m_SrcPos = Vector2.zero;
            m_DestPos = Vector2.zero;
            base.Start();
        }
        public override void Reset()
        {
            m_FirstAdd = false;
            m_SrcPos = Vector2.zero;
            m_DestPos = Vector2.zero;
            base.Reset();
        }

        public override bool Tick(long deltaTime, long curTime, InstanceData instanceData)
        {
            if (curTime < startTime)
                return true;
            if (curTime > startTime + m_MoveTime)
                return false;
            if(m_FirstAdd)
            {
                m_FirstAdd = false;
                if(m_AdjustTowardSender)
                {
                    AdjustTowardSender(instanceData.SenderId, instanceData.TargetId);
                }
                if (m_IsLockTarget)
                {//锁定目标移动，这种情况下，配置的距离和方向都无效，应该通过Sender和Target求得
                    if (instanceData.TargetId == -1)
                        return false;

                    if(m_UseOnTarget)
                    {//表示希望将位移应用到目标上
                        m_SrcPos = ComponentUtil.GetObjPosition(instanceData.TargetId);
                        m_DestPos = ComponentUtil.GetObjPosition(instanceData.SenderId);
                    }
                    else
                    {//位移应用到发送者上
                        m_SrcPos = ComponentUtil.GetObjPosition(instanceData.SenderId);
                        m_DestPos = ComponentUtil.GetObjPosition(instanceData.TargetId);
                    }
                    
                }
                else
                {//无锁定的位移
                    if(m_UseOnTarget)
                    {
                        if (instanceData.TargetId == -1)
                            return false;
                        m_SrcPos = ComponentUtil.GetObjPosition(instanceData.TargetId);
                    }
                    else
                    {
                        m_SrcPos = ComponentUtil.GetObjPosition(instanceData.SenderId);
                    }

                    float dirAngel = 0f;
                    if (m_IsRelativeSender)
                    {//以发送者的方向为参考，旋转m_Direction度
                        dirAngel = ComponentUtil.GetObjDir(instanceData.SenderId);
                    }
                    else
                    {
                        if (instanceData.TargetId == -1)
                            return false;
                        dirAngel = ComponentUtil.GetObjDir(instanceData.TargetId);
                    }
                    dirAngel = dirAngel - m_Direction;
                    float dirRadius = ComponentUtil.Deg2Rad * dirAngel;
                    Vector2 moveDir = new Vector2((float)Math.Cos(dirRadius), (float)Math.Sin(dirRadius));
                    m_DestPos = m_SrcPos + moveDir * m_Distance;
                    //TODO:这里需要验证m_DestPos可行性
                }
            }

            float t = (float)(curTime - startTime) / (float)m_MoveTime;
            Vector2 now = Vector2.Lerp(m_SrcPos, m_DestPos, t);
            if(m_UseOnTarget)
            {
                ComponentUtil.SetObjPosition(instanceData.TargetId, now);
            }
            else
            {
                ComponentUtil.SetObjPosition(instanceData.SenderId, now);
            }

            return true;
        }
        protected void AdjustTowardSender(int senderId, int targetId)
        {
            float senderDir = ComponentUtil.GetObjDir(senderId);
            ComponentUtil.SetObjDir(targetId, senderDir + 180f);
        }
    }
}
