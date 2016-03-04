

namespace RPGSkill
{
    public class SkillComponent
    {
        public SkillInstance SkillInst = null;
        public bool IsActive = false;
        protected int startTime = 0;
        public virtual void Init(int id)
        {//数据加载

        }
        public virtual void Reset()
        {//重置临时数据
            IsActive = false;
        }
        public virtual bool Tick(long deltaTime)
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
