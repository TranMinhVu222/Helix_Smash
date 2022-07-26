using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Funzilla;

public class Disk: MonoBehaviour
{
    private float time;
    private Vector3 rotation;
    private float RotateAmount = 2f;
    private State currenState = State.rotate;
    private float vitri;
    [SerializeField] private GameObject diskWin;
    [SerializeField] private Gameplay gamePlay;
    

    enum State
    {
        rotate, stop, broken
    }
 
    void Start(){
       DiskWin();
    }
    void Update()
    {
        if (currenState == State.rotate)
        {
            RotateOb();
        }
    }

    void RotateOb()
    {
        transform.Rotate(0, 90f * Time.deltaTime, 0);
    }
    
    void DiskWin()
    {
        int WinDiskPosition = gamePlay.DiskList.Count - 2;
        diskWin.transform.position = new Vector3(0,-WinDiskPosition*1.5f - 0.3f,0);
        diskWin.transform.localScale = new Vector3(14f, 1.2f, 14f);
    }

    State changeState(State state)
    {
        if (state == State.rotate)
        {
            currenState = State.rotate;
        }

        if (state ==  State.broken)
        {
            currenState = State.broken;
        }

        if (state == State.stop)
        {
            currenState = State.stop;
        }
        
        return currenState;
    }
}