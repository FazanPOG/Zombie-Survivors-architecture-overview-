using System;
using System.Collections;
using UnityEngine;

namespace Modules.Services.Scripts
{
    public class TimerService : MonoBehaviour, ITimerService
    {
        public void StartTimer(float time, Action endTimerCallback)
        {
            StartCoroutine(Timer(time, endTimerCallback));
        }

        public void WaitUntil(Func<bool> predicate, Action endWaitUntilCallback)
        {
            StartCoroutine(WaitUntilPredicate(predicate, endWaitUntilCallback));
        }

        private IEnumerator Timer(float time, Action endTimerCallback) 
        {
            yield return new WaitForSeconds(time);
            endTimerCallback?.Invoke();
        }

        private IEnumerator WaitUntilPredicate(Func<bool> predicate, Action callback) 
        {
            yield return new WaitUntil(predicate);
            callback?.Invoke();
        }
    }
}
