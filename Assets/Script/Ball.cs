using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private const float g = -9.8f;
    private float _v0;
    private float _v;
    private float _s0;
    private float _t = 0;
    private float yFall = 0;
    private float yJump=0;
    private State _currentState = State.Jump;
    [SerializeField] private GamePlay disks;
    enum State
    {
        Jump,Fall,Smash,Die
    }
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        switch (_currentState)
        {
            case State.Jump:
                //TODO: tinh do nay cua qua bong
                _v -= g * Time.deltaTime;
                yJump = _s0 + _v *_t + 0.5f*g * _t * _t;
                transform.position = new Vector3(0, yJump, 2.5f);
                // transform.localScale = new Vector3(1, 0.2f, 1);
                //TODO: Kiem tra xem qua bong da len den dinh hay chua
                if(disks.DiskList[0].transform.position.y + yJump > 3)
                {
                    ChangeSate(State.Fall);
                    return;
                }
                break;
            case State.Fall:
                _v += g * Time.deltaTime;
                yFall = _s0 + _v *_t + 0.5f*g * _t * _t;
                transform.position = new Vector3(0, yFall, 2.5f);
                // transform.localScale = new Vector3(0.2f,1,0.2f)*Time.deltaTime;
                if (disks.DiskList[0].transform.position.y + yFall < 1)
                {
                    ChangeSate(State.Jump);
                    return;
                }
                break;
            case State.Smash:
                break;
            case State.Die:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        _t += Time.deltaTime;
    }

    private void ChangeSate(State state)
    {
        if (state == _currentState) return;
        _currentState = state;
        switch (state)
        {
            case State.Jump:
                _s0 = disks.DiskList[0].transform.position.y+1f;
                _v = 5f;
                _t = 0;
                break;
            case State.Fall:
                _s0 = transform.position.y;
                _v = 0;
                _t = 0;
                break;
            case State.Smash:
                break;
            case State.Die:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}  
