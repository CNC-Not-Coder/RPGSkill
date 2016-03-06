
using System.Collections.Generic;

namespace RPGSkill
{
    
    public class RuleLogicManager
    {
        private Dictionary<RuleLogicType, IRuleLogic> m_Rules = new Dictionary<RuleLogicType, IRuleLogic>();
        private static RuleLogicManager s_Instance = new RuleLogicManager();
        public static RuleLogicManager Instance
        {
            get { return s_Instance; }
        }
        public void Init()
        {
            RegisterRule(RuleLogicType.LockTarget, new LockTargetRule());
            RegisterRule(RuleLogicType.AreaDetect, new AreaDetectRule());
        }
        protected void RegisterRule(RuleLogicType logicType, IRuleLogic logic)
        {
            m_Rules.Add(logicType, logic);
        }

        public IRuleLogic GetRuleByType(RuleLogicType logicType)
        {
            IRuleLogic ret;
            if( m_Rules.TryGetValue(logicType, out ret))
            {
                return ret;
            }
            return null;
        }
    }
}
