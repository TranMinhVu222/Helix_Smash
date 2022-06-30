using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private const float g = 9.8f;
    private const float _v0 = 3.5f;
    private float _v;
    // private float _smax = g * _v0;
    private float _smax = 3f;
    private float _t = 0;
    private float yFall = 0;
    private float yJump =0;
    private float _s0 = 0;
    private float _scaleJump = 0;
    private float _scaleFall = 0;
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
                //LAM CHO QUA BONG NHAY LEN
                yJump = _s0 + _v * _t + 0.5f * g * _t * _t;
                transform.position = new Vector3(0, yJump, 2.5f);
                Debug.Log(("Jumping: " + yJump));
                //LAM CHO QUA BONG BIEN DANG KHI NHAY LEN
                _scaleJump = yJump/(_t*200) ;
                Debug.Log(_scaleJump);
                transform.localScale = new Vector3((float)(1 - 0.5 * _scaleJump), (float)(1 + 0.5 * _scaleJump), (float)(1 - 0.5 * _scaleJump));
                //PHAT HIEN THOI DIEM ROI XUONG
                if(yJump > _smax)
                {
                    ChangeSate(State.Fall);
                    return;
                }
                break;
            case State.Fall:
                //LAM CHO QUA BONG ROI XUONG
                yFall = _s0 + 0.5f * -g * _t * _t;
                transform.position = new Vector3(0, yFall, 2.5f);
                Debug.Log("Falling: " + yFall);
                //LAM CHO QUA BONG BIEN DANG KHI ROI
                _scaleFall = yFall/(_t*300);
                transform.localScale = new Vector3(1f, (float)(1 - 0.5 * _scaleFall), 1);
                Debug.Log(_scaleFall);
                //PHAT HIEN THOI DIEM NHAY LEN
                if (yFall < disks.DiskList[0].transform.position.y + 1f)
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
                _v = _v0;
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
