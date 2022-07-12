using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk: MonoBehaviour
{
    private float time;
    private Vector3 rotation;
    private float RotateAmount = 1;
    private State currenState = State.rotate;
    private float WinDiskPosition;
    [SerializeField] private GamePlay gamePlay;
    [SerializeField] private GameObject diskWin;

    enum State
    {
        rotate, stop, broken
    }
 
    void Start(){
       // MeshColor();
       Win();
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
    
    void Win()
    {
        diskWin.transform.position = new Vector3(0,-48,0);
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