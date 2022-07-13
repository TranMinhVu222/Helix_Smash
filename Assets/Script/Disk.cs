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
    [SerializeField] private Material colorPiece;

    enum State
    {
        rotate, stop, broken
    }
 
    void Start(){
       //  if (!gameObject.GetComponentInChildren<MeshRenderer>().CompareTag("Black_Piece"))
       // {
       //     colorPiece.color = Random.ColorHSV(0.5f, 1f, 1f, 1f, 0.5f, 1f);    
       // }
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
        // int WinDiskPosition = gamePlay.DiskList.Count - 2;
        // diskWin.transform.position = new Vector3(0,-WinDiskPosition - 0.25f,0);
        diskWin.transform.position = new Vector3(0,-48.25f,0);
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