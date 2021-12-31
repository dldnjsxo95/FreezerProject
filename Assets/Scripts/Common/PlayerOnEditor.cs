using UnityEngine;
public class PlayerOnEditor : MonoBehaviour
{
#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 rot = transform.eulerAngles;

            rot.y += Input.GetAxis("Mouse X");
            rot.x += -1 * Input.GetAxis("Mouse Y");

            Quaternion q = Quaternion.Euler(rot);


            transform.rotation = Quaternion.Slerp(transform.rotation, q, 2f);

        }
    }
#endif
}
