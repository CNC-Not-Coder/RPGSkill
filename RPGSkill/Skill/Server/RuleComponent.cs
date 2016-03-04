

/*
 * 目标选取规则
 * 负责按照指定的参数设定选取符合条件的目标，然后发送相应的BufferList
 * */
using DataTableSpace;
using System.Collections.Generic;
namespace RPGSkill
{
    class RuleComponent : SkillComponent
    {
        public int Id = -1;
        public int LogicId = -1;
        public List<int> Buffers = new List<int>();

        private IRuleLogic m_Logic = null;
        private RuleData m_RuleData = new RuleData();
        
        public override void Init(int id)
        {
            Tab_RuleData data = Tab_RuleDataProvider.Instance.GetDataById(id);
            if (data == null)
                return;

            Id = id;
            LogicId = data.LogicId;
            startTime = data.StartTime;

            RuleLogicType ruleType = (RuleLogicType)LogicId;
            IRuleLogic logic = RuleLogicManager.Instance.GetRuleByType(ruleType);
            m_Logic = logic;

            m_Logic.ParseData(m_RuleData, data.ParamList);
        }
        public override void Reset()
        {
            base.Reset();
        }
        public override bool Tick(long deltaTime)
        {
            if (SkillInst.CurTime < startTime)
                return true;
            if (m_Logic == null || m_RuleData == null)
                return false;
            List<int> targets = m_Logic.GetRuleResult(m_RuleData, SkillInst);
            //TODO:给目标发送BUFFER
            return false;
        }
        public override void Start()
        {
            base.Start();
        }
        public override void Stop()
        {
            base.Stop();
        }
    }
}
