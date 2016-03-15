
namespace RPGSkill
{
    public class RuleData
    {
        public int RuleId = -1;
        public TypedDataCollection CustomData = new TypedDataCollection();

        public bool IsActive = false;
        public int Sender = -1;
        public int Target = -1;
    }
    public class AreaDetectData
    {
        public int DetectCount = 0;//执行次数
        public int DetectInterval = 0;//执行间隔

        public float Range = 0f;
        public Vector2 Offset = Vector2.zero;//以Sender朝向为基准的偏移
        public float Rotation = 0f;//以Sender朝向为基准的旋转

        //临时数据
        public long NextDetectTime = 0;
        public int LeftDetectCount = 0;
    }

    public class RectDetectData
    {
        public int DetectCount = 0;//执行次数
        public int DetectInterval = 0;//执行间隔

        public float Length = 0f;
        public float Width = 0f;
        public Vector2 Offset = Vector2.zero;//以Sender朝向为基准的偏移
        public float Rotation = 0f;//以Sender朝向为基准的旋转

        //临时数据
        public long NextDetectTime = 0;
        public int LeftDetectCount = 0;
    }
}
