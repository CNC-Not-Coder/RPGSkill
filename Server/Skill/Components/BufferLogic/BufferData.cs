

namespace RPGSkill
{
    public class BufferData
    {
        //配置数据
        public int BufferId = -1;
        public int LogicId = -1;
        public TypedDataCollection CustomData = new TypedDataCollection();

        //临时数据
        public int SenderId = -1;
        public int TargetId = -1;
        public int SkillId = -1;
        public bool IsActive = false;
    }
}
