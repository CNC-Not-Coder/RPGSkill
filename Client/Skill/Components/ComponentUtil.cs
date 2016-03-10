

using System;
using UnityEngine;

namespace RPGSkill
{
    public class ComponentUtil
    {
        private static char[] Seperator = { ' ', '|', ',' };
        public static Vector3 StringToVec3(string str)
        {
            if (string.IsNullOrEmpty(str))
                return Vector3.zero;
            string[] ps = str.Split(Seperator, StringSplitOptions.RemoveEmptyEntries);
            if(ps != null && ps.Length == 3)
            {
                float x = Convert.ToSingle(ps[0]);
                float y = Convert.ToSingle(ps[1]);
                float z = Convert.ToSingle(ps[2]);
                return new Vector3(x, y, z);
            }
            return Vector3.zero;
        }
        public static GameObject GetGameObjectById(int objId)
        {
            throw new NotImplementedException();
        }
        public static GameObject InstantiateEffect(string path, int remainTime)
        {
            throw new NotImplementedException();
        }
        public static WrapMode StringToWrapMode(string str)
        {
            WrapMode mode = WrapMode.Default;
            Enum.TryParse(str, true, out mode);
            return mode;
        }
        public static AnimationBlendMode StringToBlendMode(string str)
        {
            AnimationBlendMode mode = AnimationBlendMode.Blend;
            Enum.TryParse(str, true, out mode);
            return mode;
        }

        internal static Vector3 GetObjPosition(int objId)
        {
            throw new NotImplementedException();
        }

        internal static float GetObjDir(int objId)
        {
            throw new NotImplementedException();
        }

        internal static void SetObjPosition(int objId, Vector3 now)
        {
            throw new NotImplementedException();
        }
    }
}
