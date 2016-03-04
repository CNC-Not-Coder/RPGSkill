

using System.Collections.Generic;
namespace RPGSkill
{
    enum RuleLogicType
    {
        LockTarget = 0,
        AreaDetect,
    }
    public class IRuleLogic
    {
        public virtual void ParseData(RuleData data, List<string> args)
        {

        }
        public virtual List<int> GetRuleResult(RuleData data, SkillInstance instance)
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
            //TODO:
        }
        public override List<int> GetRuleResult(RuleData data, SkillInstance instance)
        {
            return base.GetRuleResult(data, instance);
        }
    }

    public class LockTargetRule : IRuleLogic
    {
        public override List<int> GetRuleResult(RuleData data, SkillInstance instance)
        {
            List<int> list = new List<int>();
            list.Add(instance.TargetId);
            return list;
        }
    }
}
