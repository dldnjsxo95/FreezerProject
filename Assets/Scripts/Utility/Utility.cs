using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Futuregen
{
    /// <summary>
    /// 프로그램에 사용하는 여러가지 유틸리티 함수 제공.
    /// </summary>
    public sealed class Utility
    {
        /// <summary>
        /// 원하는 시간만큼 대기 후, Callback 실행.
        /// </summary>
        /// <param name="time">대기 시간.</param>
        /// <param name="callback">대기 후, 실행할 명령.</param>
        /// <returns></returns>
        public static IEnumerator Delay(float time, UnityAction callback)
        {
            yield return new WaitForSeconds(time);
            callback?.Invoke();
        }
    }
}
