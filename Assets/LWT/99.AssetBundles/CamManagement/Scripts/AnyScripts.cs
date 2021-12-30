using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyScripts : MonoBehaviour
{
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            CamManagement.Instance.MoveTo(TargetTag.Turbo_RefrigCondenser);

    }

}
