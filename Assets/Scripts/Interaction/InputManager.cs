using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Futuregen
{
    /// <summary>
    /// 사용자 입력을 관리한다.
    /// </summary>
    public sealed class InputManager : MonoSingleton<InputManager>
    {
        /// <summary>
        /// 마우스 포인터 Id.
        /// </summary>
        private const int _pointerId = -1;

        /// <summary>
        /// 현재 선택된 오브젝트.
        /// </summary>
        [SerializeField] [ReadOnly] private GameObject _selectedObject;

        private GameObject _prevSelectedObject;

        /// <summary>
        /// 마우스 휠 이벤트.
        /// </summary>
        public UnityAction<float> OnWheel;

        private void Update()
        {
            // 마우스 포인터가 UI에 위치하지 않을 경우에만 입력 확인.
            if (!EventSystem.current.IsPointerOverGameObject(_pointerId))
            {
                CheckMouseCameraZoom();
                CheckMouseCameraMove();
                CheckMouseCameraLookRotation();
                CheckMouseObjectRaycast();
            }

            // 키보드 입력 확인.
            CheckKeyboardCameraTranslation();
            CheckKeybordEscapePressed();
            CheckKeyboardSpacePressed();
        }

        #region Mouse input
        /// <summary>
        /// 카메라 확대/축소. (마우스 휠 스크롤)
        /// </summary>
        private void CheckMouseCameraZoom()
        {
            if (_selectedObject == null)
            {
                CameraManager.Instance.Zoom(Input.GetAxis("Mouse ScrollWheel"));
            }
            else
            {
                CameraManager.Instance.Zoom(0.0f);

                float wheel;
                if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
                {
                    wheel = 1.0f;
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
                {
                    wheel = -1.0f;
                }
                else
                {
                    wheel = 0.0f;
                }
                OnWheel?.Invoke(wheel);
            }
        }

        /// <summary>
        /// 카메라 이동. (마우스 오른쪽 클릭)
        /// </summary>
        private void CheckMouseCameraMove()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                int layerMask = (1 << LayerMask.NameToLayer("Ground")) + (1 << LayerMask.NameToLayer("Wall"));

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    if (hit.transform.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
                    {
                        CameraManager.Instance.SetCameraTargetPosition(hit.point);
                    }
                }
            }
        }

        /// <summary>
        /// 카메라 회전. (마우스 왼쪽 드래그)
        /// </summary>
        private void CheckMouseCameraLookRotation()
        {
            if (Input.GetMouseButton(0))
            {
                CameraManager.Instance.UpdateRotation(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
            }
        }

        /// <summary>
        /// 오브젝트 레이캐스트. (마우스 왼쪽 클릭)
        /// </summary>
        private void CheckMouseObjectRaycast()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int layerMask = -1;
            layerMask -= (1 << LayerMask.NameToLayer("Ground")) + (1 << LayerMask.NameToLayer("Wall"));

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                _selectedObject = hit.collider.gameObject;

                // InteractableObject 체크.
                if (_selectedObject.GetComponent<InteractableObject>() != null)
                {
                    if (_prevSelectedObject != _selectedObject)
                    {
                        if (_prevSelectedObject != null)
                        {
                            InteractableObject prevObj = _prevSelectedObject.GetComponent<InteractableObject>();
                            prevObj.OnExit();
                        }

                        InteractableObject currObj = _selectedObject.GetComponent<InteractableObject>();
                        currObj.OnEnter();

                        _prevSelectedObject = _selectedObject;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        InteractableObject obj = _selectedObject.GetComponent<InteractableObject>();
                        obj.OnClick();

                        Debug.Log($"<color=orange>상호작용 대상: {hit.transform.name}</color>");
                    }
                }
            }
            else
            {
                if (_selectedObject != null)
                {
                    if (_prevSelectedObject != null && _prevSelectedObject != _selectedObject)
                    {
                        _prevSelectedObject.GetComponent<InteractableObject>()?.OnExit();
                    }

                    _selectedObject.GetComponent<InteractableObject>()?.OnExit();
                    _selectedObject = null;

                    _prevSelectedObject = null;
                }
            }
        }
        #endregion

        #region Keyboard input
        /// <summary>
        /// 카메라 이동. (WASD키, 방향키)
        /// </summary>
        private void CheckKeyboardCameraTranslation()
        {
            Vector3 direction = Vector3.zero;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                direction += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                direction += Vector3.back;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                direction += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                direction += Vector3.right;
            }

            CameraManager.Instance.UpdateMovement(direction);
        }

        /// <summary>
        /// 프로그램 종료. (ESC키)
        /// </summary>
        private void CheckKeybordEscapePressed()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DialogEventArgs dialogEventArgs = new DialogEventArgs(DialogType.QuestionBox)
                {
                    Title = "프로그램 종료",
                    Message = "종료 하시겠습니까?",
                    FirstButtonEvent = () =>
                    {
                        Application.Quit();
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#endif
                    }
                };
                DialogManager.Instance.GenerateDialog(dialogEventArgs);
            }
        }

        /// <summary>
        /// 스크립트 넘기기. (Space키)
        /// </summary>
        private void CheckKeyboardSpacePressed()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ContentManager.Instance.StartNextStep();
            }
        }
        #endregion
    }
}
