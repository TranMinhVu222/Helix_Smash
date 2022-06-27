using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk: MonoBehaviour
{
    private float time;
    private Vector3 rotation;
    private float RotateAmount = 1;
    // Start is called before the first frame update

    private State currenState = State.rotate;
    enum State
    {
        rotate, stop, broken
    }
    void Start(){
        
    }

    // Update is called once per frame
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
    
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "Ball")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Do something here"); 
            //collision.gameObject.transform.position =
                
            
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "MyGameObjectTag")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
        }
    }
}