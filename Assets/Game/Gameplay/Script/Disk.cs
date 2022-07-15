using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk: MonoBehaviour
{
    private float time;
    private Vector3 rotation;
    private float RotateAmount = 1;
    private State currenState = State.rotate;
    private float vitri;
    [SerializeField] private GameObject diskWin;
    [SerializeField] private GamePlay gamePlay;
    

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
        transform.Rotate(Vector3.up*RotateAmount);
    }
    
    void DiskWin()
    {
        int WinDiskPosition = gamePlay.DiskList.Count - 2;
        diskWin.transform.position = new Vector3(0,-WinDiskPosition - 0.25f,0);

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