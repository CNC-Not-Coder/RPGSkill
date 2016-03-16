

using System;
using System.Collections.Generic;
namespace RPGSkill
{
    public enum RuleLogicType
    {
        LockTarget = 0,
        AreaDetect = 1,
        RectDetect = 2,
    }
    public class IRuleLogic
    {
        public virtual void ParseData(RuleData data, List<string> args)
        {

        }
        public virtual void Reset(RuleData data)
        {

        }
        public virtual List<int> GetRuleResult(RuleData data, long curTime)
        {
            return new List<int>();
        }
    }
    public class AreaDetectRule : IRuleLogic
    {
        public override void ParseData(RuleData data, List<string> args)
        {
            if(args.Count < 5)
                return;
            AreaDetectData param = new AreaDetectData();
            param.DetectCount = Convert.ToInt32(args[0]);
            param.DetectInterval = Convert.ToInt32(args[1]);

            param.Range = Convert.ToSingle(args[2]);
            param.Offset = ComponentUtil.StringToVector2(args[3]);
            param.Rotation = Convert.ToSingle(args[4]);

            data.CustomData.AddData(param);
        }
        public override void Reset(RuleData data)
        {
            AreaDetectData area = data.CustomData.GetData<AreaDetectData>();
            if (area == null)
                return;
            area.LeftDetectCount = area.DetectCount;
            area.NextDetectTime = 0;
        }
        public override List<int> GetRuleResult(RuleData data, long curTime)
        {
            List<int> result = new List<int>();
            AreaDetectData area = data.CustomData.GetData<AreaDetectData>();
            if (area == null)
                return result;
            if(area.LeftDetectCount < 1)
            {
                data.IsActive = false;
                return result;
            }
            
            if(curTime > area.NextDetectTime)
            {
                area.NextDetectTime += area.DetectInterval;
                area.LeftDetectCount--;

                //以技能释放点为中心，方向以玩家朝向为参考
                float senderDir = ComponentUtil.GetObjDir(data.Sender);
                float dirRadius = ComponentUtil.Deg2Rad * (senderDir + area.Rotation);
                Vector2 moveDir = new Vector2((float)Math.Cos(dirRadius), (float)Math.Sin(dirRadius));
                Vector2 destPos = data.CastPosition + moveDir + area.Offset;
                List<int> hitObjs = ComponentUtil.GetObjectsByRadius(destPos, area.Range);
                result.AddRange(hitObjs);
            }
            return result;
        }
    }
    public class RectDetectRule : IRuleLogic
    {
        public override void ParseData(RuleData data, List<string> args)
        {
            if (args.Count < 6)
                return;
            RectDetectData param = new RectDetectData();
            param.DetectCount = Convert.ToInt32(args[0]);
            param.DetectInterval = Convert.ToInt32(args[1]);

            param.Length = Convert.ToSingle(args[2]);
            param.Width = Convert.ToSingle(args[3]);
            param.Offset = ComponentUtil.StringToVector2(args[4]);
            param.Rotation = Convert.ToSingle(args[5]);

            data.CustomData.AddData(param);
        }
        public override void Reset(RuleData data)
        {
            RectDetectData rect = data.CustomData.GetData<RectDetectData>();
            if (rect == null)
                return;
            rect.LeftDetectCount = rect.DetectCount;
            rect.NextDetectTime = 0;
        }
        public override List<int> GetRuleResult(RuleData data, long curTime)
        {
            List<int> result = new List<int>();
            RectDetectData rect = data.CustomData.GetData<RectDetectData>();
            if (rect == null)
                return result;
            if (rect.LeftDetectCount < 1)
            {
                data.IsActive = false;
                return result;
            }

            if (curTime > rect.NextDetectTime)
            {
                rect.NextDetectTime += rect.DetectInterval;
                rect.LeftDetectCount--;

                //以技能释放点为中心，方向以玩家朝向为参考
                float senderDir = ComponentUtil.GetObjDir(data.Sender);
                float dirRadius = ComponentUtil.Deg2Rad * (senderDir + rect.Rotation);
                Vector2 moveDir = new Vector2((float)Math.Cos(dirRadius), (float)Math.Sin(dirRadius));
                Vector2 destPos = data.CastPosition + moveDir + rect.Offset;

                //为了计算长方形的Begin和End，需要先计算这两个向量
                Vector3 toward = new Vector3(moveDir.x, 0, moveDir.y);
                Vector3 right = Vector3.Cross(toward.normalized, Vector3.up);
                Vector2 right2 = new Vector2(right.x, right.z);

                Vector2 halfCrossLine = moveDir.normalized * (0.5f * rect.Width) + right2.normalized * (0.5f * rect.Length);
                Vector2 beg = new Vector2(destPos.x, destPos.y) - halfCrossLine;
                Vector2 end = new Vector2(destPos.x, destPos.y) + halfCrossLine;

                List<int> hitObjs = ComponentUtil.GetObjectsByRect(destPos, beg, end);
                result.AddRange(hitObjs);
            }
            return result;
        }
    }

    public class LockTargetRule : IRuleLogic
    {
        public override List<int> GetRuleResult(RuleData data, long curTime)
        {
            List<int> list = new List<int>();
            if (data.Target != -1)
            {
                list.Add(data.Target);
            }
            data.IsActive = false;
            return list;
        }
    }
}
