
/*
 * 特效播放组件
 * 负责给目标挂接特效，对于子弹特效，需要根据发送者和接收者以及延时做位移处理
 * */
namespace RPGSkill
{
    public class EffectComponent : SkillComponent
    {
        public override void Init(int id)
        {
            base.Init(id);
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override bool Tick(long deltaTime, long curTime, InstanceData instanceData)
        {
            return base.Tick(deltaTime, curTime, instanceData);
        }
    }
}
