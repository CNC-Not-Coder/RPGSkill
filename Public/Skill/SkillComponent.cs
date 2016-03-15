

namespace RPGSkill
{
    public class InstanceData
    {
        public int InstanceId = -1;
        public int SenderId = -1;
        public int TargetId = -1;
        public int SkillId = -1;
        public float Cast_x = 0f;
        public float Cast_y = 0f;
        public float Cast_z = 0f;
        public TypedDataCollection CustomData = new TypedDataCollection();
    }
    public class SkillComponent
    {
        public bool IsActive = false;
        protected int startTime = 0;
        public virtual void Init(int id)
        {//数据加载

        }
        public virtual void Reset()
        {//重置临时数据
            IsActive = false;
        }
        public virtual bool Tick(long deltaTime, long curTime, InstanceData instanceData)
        {
            return false;
        }
        public virtual void Start()
        {
            IsActive = true;
        }
        public virtual void Stop()
        {
            IsActive = false;
        }
    }
}
