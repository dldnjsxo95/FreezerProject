using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCamMovement : MonoBehaviour
{
    public TargetTag_P1 targetTag_P1;
    public CamManagement_P1 camManagement_P1;

    [ContextMenu("CamMove_P1")]
    public void CamMove_P1()
	{
        camManagement_P1.MoveTo(targetTag_P1);
	}


}
