using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour

{
    // Start is called before the first frame update
    [SerializeField] private GamePlay followDisk;
    [SerializeField] private Vector3 positionCam;
    [SerializeField] private GameObject winDisk;
    private void LateUpdate()
    {
        if (followDisk.DiskList.Count > 0)
        {
            transform.position = followDisk.DiskList[0].transform.position + positionCam;
            return;
        }
        else
        {
            //stransform.position = winDisk.transform.position + positionCam;
        }
    }
}