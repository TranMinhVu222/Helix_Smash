using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private const float A = -9.8f;
    private float _v0;
    private float _s0;
    private float _t = 0;
    private float _v;
    private float yFall = 0;

    [SerializeField] private GamePlay disks;
    // Start is called before the first frame update
    private State _currentState = State.Jump;
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
                //TODO: Tinh toan vi tri dung cong thuc roi tu do
                _v += A * Time.deltaTime;
                yFall = _s0 + _v0 * _t + A * _t * _t * 0.5f;
                transform.position = new Vector3(0, yFall, 2.5f);
                //TODO: Kiem tra xem qua bong da len den dinh hay chua
                if(disks.GetTopLayer().transform.position.y+0.5f <= 2f)
                {  
                    ChangeSate(State.Fall);
                    return;
                }
                break;
            case State.Fall:
                // TODO: Kiem tra xem qua bong co va cham vao layer tren cung hay khong
                _v += A * Time.deltaTime;
                yFall = _s0 + _v0 * _t + A * _t * _t * 0.5f;
                transform.position = new Vector3(0, yFall, 2.5f);
                if (disks.GetTopLayer().transform.position.y - 2f <= yFall)
                {
                    ChangeSate(State.Jump);
                    return;
                }
                // // TODO: Kiem tra xem qua bong co va cham vao layer tren cung hay khong
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
                _s0 = transform.position.y;
                _v0 = _v;
                _t = 0;
                break;
            case State.Fall:
                _s0 = transform.position.y;
                _v0 = 0.0f;
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
