

using System.Collections.Generic;
namespace RPGSkill
{
    public enum BufferLogicType
    {
        OneTime = 0,
        Interval = 1,
        Circular = 2,
    }
    public class IBufferLogic
    {
        public virtual void ParseData(BufferData data, List<string> args) { }
        public virtual void Start(BufferData data) {}
        public virtual void Stop(BufferData data) { }
        public virtual void Interupt(BufferData data) { }
        public virtual void Tick(BufferData data) { }
        public virtual bool OnOtherBuffer(BufferData data)
        {
            bool IsContinue = true;
            return IsContinue;
        }
    }
    //下面仅仅是举例子。距离buffer分类要看需求

    //单体一次性Buffer
    public class OneTimeBuffer : IBufferLogic
    {

    }
    //单体持续Buffer
    public class IntervalBuffer : IBufferLogic
    {

    }
    //光环Buffer
    public class CircularBuffer : IBufferLogic
    {

    }
}
