using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardObject : MonoBehaviour
{
    public Transform camRoot;

    public float distance = 20f;

    public bool isCursor = false;

    private void Start()
    {
        if (camRoot == null)
        {
            camRoot = Camera.main.transform;
        }

        transform.position = (transform.position - camRoot.position).normalized * distance;

    }

    private void Update()
    {
        transform.LookAt(transform.position + (transform.position - camRoot.position).normalized);

        //if (isCursor)
        //{
        //    transform.localEulerAngles += new Vector3(30f, 0, 0);
        //}
        transform.position = (transform.position - camRoot.position).normalized * distance;

        Vector3 zeroZ = transform.eulerAngles;
        zeroZ.z = 0;
        transform.eulerAngles = zeroZ;
    }

}
