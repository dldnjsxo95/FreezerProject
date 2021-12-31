using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Futuregen
{
    /// <summary>
    /// 카메라 위치 데이터.
    /// </summary>
    [Serializable]
    public struct CameraLocation
    {
        public CameraTargetID Id;
        public Transform Trans;

        public Vector3 LookPosition
        {
            get
            {
                return Trans.position + Trans.forward;
            }
        }
    }

    /// <summary>
    /// 카메라 조작을 관리한다.
    /// </summary>
    public sealed class CameraManager : MonoSingleton<CameraManager>
    {
        private class CameraState
        {
            public float Yaw;
            public float Pitch;
            public float Roll;
            public float X;
            public float Y;
            public float Z;

            public void SetFromTransform(Transform transform)
            {
                Pitch = transform.eulerAngles.x;
                Yaw = transform.eulerAngles.y;
                Roll = transform.eulerAngles.z;
                X = transform.position.x;
                Y = transform.position.y;
                Z = transform.position.z;
            }

            public void RotatedTranslate(Vector3 translation, bool holdY)
            {
                Vector3 rotatedTranslation = Quaternion.Euler(Pitch, Yaw, Roll) * translation;

                X += rotatedTranslation.x;
                Y = holdY ? Y : Y + rotatedTranslation.y;
                Z += rotatedTranslation.z;
            }

            public void LerpTowards(CameraState target, float positionLerpPct, float rotationLerpPct)
            {
                Yaw = Mathf.Lerp(Yaw, target.Yaw, rotationLerpPct);
                Pitch = Mathf.Lerp(Pitch, target.Pitch, rotationLerpPct);
                Roll = Mathf.Lerp(Roll, target.Roll, rotationLerpPct);

                X = Mathf.Lerp(X, target.X, positionLerpPct);
                Y = Mathf.Lerp(Y, target.Y, positionLerpPct);
                Z = Mathf.Lerp(Z, target.Z, positionLerpPct);
            }

            public void UpdateTransform(Transform transform)
            {
                transform.eulerAngles = new Vector3(Pitch, Yaw, Roll);
                //transform.position = new Vector3(X, Y, Z);
            }
        }

        [Header("Settings")]
        [SerializeField] private bool _enableConrtrol = true;
        [SerializeField] private Camera _targetCamera;
        [SerializeField] private GameObject _movePointEffect;

        [Header("Zoom")]
        [SerializeField] [Range(1.0f, 10.0f)] private float _zoomSpeed = 10.0f;
        [SerializeField] [Range(0.0f, 1.0f)] private float _zoomSmoothTime = 0.2f;
        [SerializeField] private float _minFov = 20.0f;
        [SerializeField] private float _maxFov = 60.0f;

        [Header("Movement")]
        [SerializeField] [Range(1.0f, 20.0f)] private float _moveSpeed = 10.0f;
        [SerializeField] [Range(0.001f, 1.0f)] private float _moveSmoothTime = 0.4f;
        [SerializeField] private bool _holdY = true;

        [Header("Rotation")]
        [SerializeField] [Range(1.0f, 20.0f)] private float _rotateSpeed = 10.0f;
        [SerializeField] [Range(0.001f, 1.0f)] private float _rotateSmoothTime = 0.4f;
        [SerializeField] private bool _invertY = true;

        [Header("Location")]
        [SerializeField] private List<CameraLocation> _locations;
        [SerializeField] [Range(0.0f, 2.0f)] private float _moveLocationTime = 1.0f;
        [SerializeField] [Range(0.0f, 1.0f)] private float _arriveDistance = 0.6f;
        [SerializeField] [ReadOnly] private CameraTargetID _currentTargetId;

        private CameraState _cameraState = new CameraState();
        private CameraState _interpolatingCameraState = new CameraState();

        private float _targetFov;
        private float _zoomVelocity;
        private Vector3 _translation;
        private CharacterController _characterController;

        public bool EnableControl
        {
            get => _enableConrtrol;
            set
            {
                if (value)
                {
                    UpdateCameraState();
                }
                _enableConrtrol = value;
            }
        }

        private void Awake()
        {
            _targetFov = _maxFov;

            // 카메라 초기 위치 설정.
            if (_targetCamera == null)
            {
                _targetCamera = Camera.main;
            }
            _characterController = _targetCamera.GetComponent<CharacterController>();

            UpdateCameraState();
        }

        private void Update()
        {
            if (EnableControl)
            {
                // 카메라 이동.
                _cameraState.RotatedTranslate(_translation, _holdY);

                // CharacterController 이동.
                Vector3 rotatedTranslation = Quaternion.Euler(_cameraState.Pitch, _cameraState.Yaw, _cameraState.Roll) * _translation;
                rotatedTranslation.y = _holdY ? 0.0f : rotatedTranslation.y;
                _characterController.Move(rotatedTranslation);

                // 카메라 회전.
                float positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / _moveSmoothTime) * Time.deltaTime);
                float rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / _rotateSmoothTime) * Time.deltaTime);
                _interpolatingCameraState.LerpTowards(_cameraState, positionLerpPct, rotationLerpPct);

                _interpolatingCameraState.UpdateTransform(_targetCamera.transform);
            }
        }

        /// <summary>
        /// 카메라 상태 갱신.
        /// </summary>
        private void UpdateCameraState()
        {
            Transform targetTransform = _targetCamera.transform;

            _cameraState.SetFromTransform(targetTransform);
            _interpolatingCameraState.SetFromTransform(targetTransform);
        }

        /// <summary>
        /// 카메라 줌 인/아웃.
        /// </summary>
        /// <param name="scrollValue"></param>
        public void Zoom(float scrollValue)
        {
            if (EnableControl)
            {
                _targetFov += scrollValue * (_zoomSpeed * -10.0f);
                _targetFov = Mathf.Clamp(_targetFov, _minFov, _maxFov);

                _targetCamera.fieldOfView = Mathf.SmoothDamp(_targetCamera.fieldOfView, _targetFov, ref _zoomVelocity, _zoomSmoothTime);
            }
        }

        /// <summary>
        /// 카메라 이동 정보 갱신.
        /// </summary>
        /// <param name="direction"></param>
        public void UpdateMovement(Vector3 direction)
        {
            _translation = direction * Time.deltaTime * _moveSpeed;
        }

        /// <summary>
        /// 카메라 회전 정보 갱신.
        /// </summary>
        /// <param name="inputRotation"></param>
        public void UpdateRotation(Vector2 inputRotation)
        {
            Vector2 mouseMovement = inputRotation * Time.deltaTime * (_rotateSpeed * 10.0f);
            mouseMovement.y = _invertY ? -mouseMovement.y : mouseMovement.y;

            _cameraState.Yaw += mouseMovement.x;
            _cameraState.Pitch += mouseMovement.y;
        }

        /// <summary>
        /// 해당 위치를 바라보도록 카메라 이동.
        /// </summary>
        /// <param name="targetId"></param>        
        public void SetCameraLookTarget(CameraTargetID targetId)
        {
            StopAllCoroutines();
            StartCoroutine(MoveToLocation(targetId));
        }

        /// <summary>
        /// 카메라 이동 기능. (Location 사용)
        /// </summary>
        /// <param name="targetId"></param>
        /// <returns></returns>
        private IEnumerator MoveToLocation(CameraTargetID targetId)
        {
            EnableControl = false;

            _currentTargetId = targetId;
            CameraLocation location = _locations.Find((loc) => loc.Id.Equals(targetId));

            Transform targetTransform = _targetCamera.transform;

            Vector3 startPos = targetTransform.position;
            Vector3 endPos = location.Trans.position;

            Quaternion startRot = targetTransform.rotation;
            Quaternion endRot = Quaternion.LookRotation(location.LookPosition - location.Trans.position);

            float elapsed = 0.0f;

            while (elapsed <= _moveLocationTime)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _moveLocationTime);

                targetTransform.position = Vector3.Lerp(startPos, endPos, Easing.InOutQuad(t));
                targetTransform.rotation = Quaternion.Slerp(startRot, endRot, Easing.InOutQuad(t));

                yield return new WaitForFixedUpdate();
            }

            EnableControl = true;
        }

        /// <summary>
        /// 해당 좌표로 카메라 이동.
        /// </summary>
        /// <param name="targetId"></param>        
        public void SetCameraTargetPosition(Vector3 position)
        {
            StopAllCoroutines();

            _movePointEffect.transform.position = position;

            Vector3 holdYPosition = new Vector3(position.x, _targetCamera.transform.position.y, position.z);
            StartCoroutine(MoveToPosition(holdYPosition));
        }

        /// <summary>
        /// 카메라 이동 기능. (Vector3 사용)
        /// </summary>
        /// <param name="targetPosition"></param>
        /// <returns></returns>
        private IEnumerator MoveToPosition(Vector3 targetPosition)
        {
            EnableControl = false;

            _movePointEffect.SetActive(true);

            _currentTargetId = CameraTargetID.None;

            Transform targetTransform = _targetCamera.transform;

            Vector3 startPos = targetTransform.position;
            Vector3 endPos = targetPosition;

            //Quaternion startRot = targetTransform.rotation;
            //Quaternion endRot = Quaternion.LookRotation(location.LookPositon - location.Trans.position);

            float elapsed = 0.0f;

            while (elapsed <= _moveLocationTime)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / _moveLocationTime);

                targetTransform.position = Vector3.Lerp(startPos, endPos, Easing.InOutQuad(t));
                //targetTransform.rotation = Quaternion.Slerp(startRot, endRot, Easing.InOutQuad(t));

                yield return new WaitForFixedUpdate();
            }

            _movePointEffect.SetActive(false);

            EnableControl = true;
        }

        /// <summary>
        /// 카메라가 Location 범위에 있는지 확인.
        /// </summary>
        /// <param name="targetId">대상 Location ID.</param>
        /// <returns></returns>
        public bool IsArriveLocation(CameraTargetID targetId)
        {
            // 이동이 끝나고 나서 확인해야 함.
            if (!EnableControl)
            {
                return false;
            }
            return Vector3.Distance(_targetCamera.transform.position, _locations[Convert.ToInt32(targetId)].Trans.position) <= _arriveDistance;
        }
    }
}
