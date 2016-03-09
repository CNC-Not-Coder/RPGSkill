
using System;
//服务器的组件帮助类
namespace RPGSkill
{
    public struct Vector2
    {
        public float x;
        public float y;
        public Vector2(float X, float Y)
        {
            x = X;
            y = Y;
        }
        public static Vector2 forward
        {
            get { return new Vector2(1, 0); }
        }
    }
    public class ComponentUtil
    {
        public static void SendBufferToTarget(int bufferId, int skillId, int sender, int target)
        {
            //需要根据sender获得Scene对象，然后拿到BufferSystem
            throw new NotImplementedException();
        }
        public static float GetDistance(int objId1, int objId2)
        {
            //需要根据objId获得Scene对象
            throw new NotImplementedException();
        }
        public static void SetObjPosition(int objId, float x, float y, float z)
        {
            throw new NotImplementedException();
        }
        public static Vector2 GetObjPosition(int objId)
        {
            throw new NotImplementedException();
        }
    }
}
