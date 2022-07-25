using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Funzilla;

public class CameraManager : MonoBehaviour

{
    // Start is called before the first frame update
    [SerializeField] private Gameplay followDisk;
    [SerializeField] private Vector3 positionCam;
    private void Update()
    {
        Debug.Log(followDisk.DiskList.Count);
        if (followDisk.DiskList.Count > 0)
        {
            transform.position = followDisk.DiskList[0].transform.position + positionCam;
        }
    }
}