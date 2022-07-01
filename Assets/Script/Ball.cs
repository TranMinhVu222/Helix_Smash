using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Ball : MonoBehaviour
{
    private const float g = 39.8f;
    private const float _v0 = 3.5f;
    private float _v;
    // private float _smax = g * _v0;
    private float _smax = 3f;
    private float _t = 0;
    private float yFall = 0;
    private float yJump =0;
    private float _s0 = 0;
    private State _currentState = State.Jump;
    private bool click;
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
                //LAM CHO QUA BONG BIEN DANG KHI NHAY LEN
                transform.localScale = new Vector3((float)(1 - 0.6 * _t), (float)(1 + 0.2 * _t),(float)(1 - 0.6 * _v0));
                //PHAT HIEN THOI DIEM ROI XUONG
                if(yJump > _smax)
                {
                    ChangeSate(State.Fall);
                    return;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    ChangeSate(State.Smash);
                }
                break;
            case State.Fall:
                //LAM CHO QUA BONG ROI XUONG
                yFall = _s0 + 0.5f * -g * _t * _t;
                transform.position = new Vector3(0, yFall, 2.5f);
                //LAM CHO QUA BONG BIEN DANG KHI ROI
                transform.localScale = new Vector3(1f + 0.6f * _t,1f - 0.2f * _t,1f + 0.6f * _t);
                //PHAT HIEN THOI DIEM NHAY LEN
                if (yFall < disks.DiskList[0].transform.position.y + 1f)
                {
                    ChangeSate(State.Jump);
                    return;
                }

                if (Input.GetMouseButton(0))
                {
                    ChangeSate(State.Smash);
                }
                break;
            case State.Smash:
                if (Input.GetMouseButtonUp(0))
                {
                    click = false;
                }
                if (Input.GetMouseButton(0))
                {
                    _smax -= 1.5f;
                    int temp = 0;
                    transform.position -= new Vector3(0, 1.5f, 0) * Time.deltaTime;
                    if (transform.position.y > disks.DiskList[0].transform.position.y)
                    {
                        Destroy(disks.DiskList[0]);
                        disks.DiskList.Remove(disks.DiskList[0]);
                    }
                }
                if (click == false)
                {
                    ChangeSate(State.Jump);
                }
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
