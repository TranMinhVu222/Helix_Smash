using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk: MonoBehaviour
{
    private float time;
    private Vector3 rotation;
    private float RotateAmount = 1;
    private State currenState = State.rotate;

    enum State
    {
        rotate, stop, broken
    }
 
    void Start(){
       // MeshColor();
     
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