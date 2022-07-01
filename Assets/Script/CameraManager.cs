using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GamePlay followDisk;
    [SerializeField] private Vector3 positionCam;
    private void LateUpdate()
    {
        Debug.Log( followDisk.DiskList[0].transform.position);
        transform.position = followDisk.DiskList[0].transform.position + positionCam;
    }
}