

using DataTableSpace;
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
        }

        public override bool Tick(long deltaTime, long curTime, InstanceData instanceData)
        {
            if (curTime < startTime)
                return true;
            if (curTime > startTime + m_MoveTime)
                return false;

            float fDistance = 0f;
            float fDirection = 0f;
            if(m_IsLockTarget)
            {
                
            }
            else
            {
                fDistance = m_Distance;
                fDirection = m_Direction;
            }


            return true;
        }
    }
}
