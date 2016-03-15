

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
        public virtual List<int> GetRuleResult(RuleData data)
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
        public override List<int> GetRuleResult(RuleData data)
        {
            AreaDetectData area = data.CustomData.GetData<AreaDetectData>();
            if (area == null)
                return new List<int>();
            //Vector2 pos = component.TargetId
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
        public override List<int> GetRuleResult(RuleData data)
        {
            RectDetectData rect = data.CustomData.GetData<RectDetectData>();
            if (rect == null)
                return new List<int>();

        }
    }

    public class LockTargetRule : IRuleLogic
    {
        public override List<int> GetRuleResult(RuleData data)
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
