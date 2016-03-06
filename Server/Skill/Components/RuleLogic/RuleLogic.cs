

using System.Collections.Generic;
namespace RPGSkill
{
    public enum RuleLogicType
    {
        LockTarget = 0,
        AreaDetect = 1,
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
            AreaDetectData param = new AreaDetectData();
            data.CustomData.AddData(param);
            //TODO:解析参数
        }
        public override List<int> GetRuleResult(RuleData data, RuleComponent component)
        {
            AreaDetectData area = data.CustomData.GetData<AreaDetectData>();
            float range = area.Range;
            return base.GetRuleResult(data, component);
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
