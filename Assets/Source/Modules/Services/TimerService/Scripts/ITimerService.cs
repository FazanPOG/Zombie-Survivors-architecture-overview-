using System;

namespace Modules.Services.Scripts
{
    public interface ITimerService
    {
        void StartTimer(float time, Action endTimerCallback);
        void WaitUntil(Func<bool> predicate, Action endWaitUntilCallback);
    }
}
