using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button3D : MonoBehaviour
{
    public void OnMouseDown()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
    }
}
