
namespace RPGSkill
{
    public class RuleData
    {
        public int RuleId = -1;
        public TypedDataCollection CustomData = new TypedDataCollection();
    }
    public class AreaDetectData
    {
        public float Range = 0f;
        public Vector2 Offset = Vector2.zero;//以目标朝向为基准的偏移
        public int DetectCount = 0;//执行次数
        public int DetectInterval = 0;//执行间隔
    }

    public class RectDetectData
    {
        public float Length = 0f;
        public float Width = 0f;
        public Vector2 Offset = Vector2.zero;//以目标朝向为基准的偏移
        public float Rotation = 0f;//以目标朝向为基准的旋转
        public int DetectCount = 0;//执行次数
        public int DetectInterval = 0;//执行间隔
    }
}
