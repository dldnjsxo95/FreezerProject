using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Futuregen
{
    /// <summary>
    /// 각 Step의 여러가지 이벤트 행동을 등록할 수 있도록 하는 Delegate.
    /// </summary>
    /// <returns></returns>
    public delegate IEnumerator StepEvent();

    /// <summary>
    /// 프로그램에 필요한 이벤트를 등록하고 관리한다.
    /// </summary>
    public sealed class EventManager : MonoSingleton<EventManager>
    {
        // Inspector에서 실시간 확인 용도.
        [SerializeField] [ReadOnly] private EventID _currentEventID;

        /// <summary>
        /// 등록된 모든 이벤트 목록.
        /// </summary>
        private Dictionary<EventID, List<StepEvent>> _stepEvents = new Dictionary<EventID, List<StepEvent>>();
        /// <summary>
        /// 현재 Step의 내레이션 재생시간.
        /// </summary>
        private float _narrationPlayTime = 0.0f;
        /// <summary>
        /// 모든 이벤트가 종료되었는지 판별하기 위한 값.
        /// </summary>
        private bool _isInvokeEnd = false;

        /// <summary>
        /// 현재 실행되고 있는 이벤트 ID.
        /// </summary>
        public EventID CurrentEventID => _currentEventID;

        private void OnDestroy()
        {
            ReleaseEvent();
        }

        /// <summary>
        /// 이벤트 등록.
        /// </summary>
        /// <param name="id">이벤트 ID.</param>
        /// <param name="stepEvent">이벤트 행동.</param>
        public void ResisterEvent(EventID id, StepEvent stepEvent)
        {
            if (_stepEvents.ContainsKey(id))
            {
                _stepEvents[id].Add(stepEvent);
            }
            else
            {
                List<StepEvent> stepEvents = new List<StepEvent>();
                stepEvents.Add(stepEvent);
                _stepEvents.Add(id, stepEvents);

                Debug.Log($"<color=cyan>이벤트 등록. (ID: {id})</color>");
            }
        }

        /// <summary>
        /// 모든 이벤트 해제.
        /// </summary>
        private void ReleaseEvent()
        {
            _stepEvents.Clear();
        }

        /// <summary>
        /// Step 이벤트 실행.
        /// </summary>
        /// <param name="id">실행할 이벤트 ID.</param>
        public void InvokeEvent(EventID id)
        {
            _currentEventID = id;
            _isInvokeEnd = false;

            // 이벤트가 없을 경우, 내레이션 재생시간 동안 대기한다.
            if (id.Equals(EventID.NONE))
            {
                StartCoroutine(Utility.Delay(_narrationPlayTime, () => _isInvokeEnd = true));
                return;
            }

            // 동록된 이벤트가 없는 경우, Step 데이터와 프로그램의 확인이 필요하므로 오류 출력.
            if (!_stepEvents.ContainsKey(id))
            {
                Debug.LogError($"<color=cyan>등록된 이벤트가 없습니다. (ID: {id})</color>");
                return;
            }

            StartCoroutine(EventRoutine(_stepEvents[id]));
            Debug.Log($"<color=cyan>이벤트 시작. (ID: {id})</color>");
        }

        /// <summary>
        /// Step과 연관없는 이벤트 실행.
        /// </summary>
        /// <param name="id">실행할 이벤트 ID.</param>
        public void InvokeUniqueEvent(EventID id)
        {
            foreach (StepEvent stepEvent in _stepEvents[id])
            {
                StartCoroutine(stepEvent());
            }
            Debug.Log($"<color=cyan>이벤트 호출. (ID: {id})</color>");
        }

        /// <summary>
        /// 전체 이벤트 진행 프로세스.
        /// </summary>
        /// <param name="stepEventList">실행할 이벤트 목록.</param>
        /// <returns></returns>
        private IEnumerator EventRoutine(List<StepEvent> stepEventList)
        {
            int eventCount = 0;

            // 각 이벤트를 실행한다.
            foreach (StepEvent stepEvent in stepEventList)
            {
                StartCoroutine(UnitRoutine(stepEvent, () => eventCount++));
            }

            // 모든 이벤트가 종료될 때까지 대기.
            while (eventCount < stepEventList.Count)
            {
                yield return null;
            }

            Debug.Log($"<color=cyan>이벤트 종료. (ID: {_currentEventID})</color>");
            _isInvokeEnd = true;
        }

        /// <summary>
        /// 각 이벤트 진행 프로세스.
        /// </summary>
        /// <param name="stepEvent">실행할 이벤트.</param>
        /// <param name="callback">이벤트가 완료되었다고 알리는 Callback.</param>
        /// <returns></returns>
        private IEnumerator UnitRoutine(StepEvent stepEvent, UnityAction callback)
        {
            yield return StartCoroutine(stepEvent.Invoke());
            callback?.Invoke();
        }

        /// <summary>
        /// 현재 실행하고 있는 이벤트가 모두 종료되었는지 확인.
        /// </summary>
        /// <returns></returns>
        public bool CheckEventEnd()
        {
            return _isInvokeEnd;
        }

        /// <summary>
        /// 현재 Step의 내레이션 재생시간 설정.
        /// </summary>
        /// <param name="playTime">내레이션 재생시간.</param>
        public void SetNarrationPlayTime(float playTime)
        {
            _narrationPlayTime = playTime;
        }
    }
}
