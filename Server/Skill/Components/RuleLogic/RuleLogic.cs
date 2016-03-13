

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
        public virtual List<int> GetRuleResult(RuleData data, RuleComponent component)
        {
            return new List<int>();
        }
    }
    public class AreaDetectRule : IRuleLogic
    {
        public override void ParseData(RuleData data, List<string> args)
        {
            if(args.Count < 4)
                return;
            AreaDetectData param = new AreaDetectData();
            param.Range = Convert.ToSingle(args[0]);
            param.Offset = ComponentUtil.StringToVector2(args[1]);
            param.DetectCount = Convert.ToInt32(args[2]);
            param.DetectInterval = Convert.ToInt32(args[3]);

            data.CustomData.AddData(param);
        }
        public override List<int> GetRuleResult(RuleData data, RuleComponent component)
        {
            AreaDetectData area = data.CustomData.GetData<AreaDetectData>();
            if (area == null)
                return new List<int>();
        }
    }
    public class RectDetectRule : IRuleLogic
    {
        public override void ParseData(RuleData data, List<string> args)
        {
            if (args.Count < 6)
                return;
            RectDetectData param = new RectDetectData();
            param.Length = Convert.ToSingle(args[0]);
            param.Width = Convert.ToSingle(args[1]);
            param.Offset = ComponentUtil.StringToVector2(args[2]);
            param.Rotation = Convert.ToSingle(args[3]);
            param.DetectCount = Convert.ToInt32(args[4]);
            param.DetectInterval = Convert.ToInt32(args[5]);

            data.CustomData.AddData(param);
        }
        public override List<int> GetRuleResult(RuleData data, RuleComponent component)
        {
            RectDetectData rect = data.CustomData.GetData<RectDetectData>();
            if (rect == null)
                return new List<int>();

        }
    }

    public class LockTargetRule : IRuleLogic
    {
        public override List<int> GetRuleResult(RuleData data, RuleComponent component)
        {
            List<int> list = new List<int>();
            list.Add(component.TargetId);
            return list;
        }
    }
}
