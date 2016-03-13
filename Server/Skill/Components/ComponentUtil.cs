
using System;
using System.Collections.Generic;
//服务器的组件帮助类
namespace RPGSkill
{
    public struct Vector2
    {
        public float x;
        public float y;
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public float magnitude
        {
            get { return (float)Math.Sqrt(x * x + y * y); }
        }
        public Vector2 normalized
        {
            get
            {
                Vector2 vector = new Vector2(this.x, this.y);
                vector.Normalize();
                return vector;
            }
        }
        public static Vector2 zero
        {
            get
            {
                return new Vector2(0f, 0f);
            }
        }

        public static Vector2 forward
        {
            get 
            { 
                return new Vector2(1, 0); 
            }
        }
        public void Normalize()
        {
            float magnitude = this.magnitude;
            if (magnitude > 1E-05f)
            {
                this = (Vector2)(this / magnitude);
            }
            else
            {
                this = zero;
            }
        }
        public static Vector2 Lerp(Vector2 from, Vector2 to, float t)
        {
            t = Clamp01(t);
            return new Vector2(from.x + ((to.x - from.x) * t), from.y + ((to.y - from.y) * t));
        }
        private static float Clamp01(float value)
        {
            if (value < 0f)
            {
                return 0f;
            }
            if (value > 1f)
            {
                return 1f;
            }
            return value;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
        public static Vector2 operator-(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }
        public static Vector2 operator *(Vector2 a, float d)
        {
            return new Vector2(a.x * d, a.y * d);
        }
        public static Vector2 operator *(float d, Vector2 a)
        {
            return new Vector2(a.x * d, a.y * d);
        }
        public static Vector2 operator /(Vector2 a, float d)
        {
            return new Vector2(a.x / d, a.y / d);
        }


    }
    public class ComponentUtil
    {
        public static void SendBufferToTarget(int bufferId, int skillId, int sender, int target)
        {
            //需要根据sender获得Scene对象，然后拿到BufferSystem
            throw new NotImplementedException();
        }
        public static void SetObjPosition(int objId, Vector2 dest)
        {
            //需要根据objId获得Scene对象
            throw new NotImplementedException();
        }
        public static Vector2 GetObjPosition(int objId)
        {
            throw new NotImplementedException();
        }
        public static float GetObjDir(int objId)
        {
            throw new NotImplementedException();
        }

        public static List<BufferData> GetBufferDatasByObjId(int objId)
        {
            //需要根据objId获得Scene对象，然后获得ServerBufferSystem
            throw new NotImplementedException();
        }
        private static char[] Seperator = { ' ', '|', ',' };
        public static Vector2 StringToVector2(string str)
        {
            if (string.IsNullOrEmpty(str))
                return Vector2.zero;
            string[] ps = str.Split(Seperator, StringSplitOptions.RemoveEmptyEntries);
            if (ps != null && ps.Length == 2)
            {
                float x = Convert.ToSingle(ps[0]);
                float y = Convert.ToSingle(ps[1]);
                return new Vector2(x, y);
            }
            return Vector2.zero;
        }
        public static List<int> GetObjectsByRadius(Vector2 pos, float range)
        {
            throw new NotImplementedException();
        }

        public const float Deg2Rad = 0.01745329f;
        public const float Rad2Deg = 57.29578f;
        public const float PI = 3.141593f;
    }
}
