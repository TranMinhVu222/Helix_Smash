using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SocialPlatforms;
using  Funzilla;
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
    private int dem = 0;
    private bool furing = false;
    private bool checkFurry = false;
    public State _currentState = State.Jump;
    private int _undestroyable = 1;
    private float speedDestroy;
    private GameObject destroyDisk;
    [SerializeField] private Gameplay disks;
    [SerializeField] private Image whiteCircle;
    [SerializeField] private GameObject FireFury;
    public enum State
    {
        Jump,Fall,Smash,Die
    }

    void Start()
    {
        FireFury.SetActive(false);
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
                if (Input.GetMouseButtonDown(0) && disks.DiskList.Count > 2)
                {
                    ChangeSate(State.Smash);
                    return;
                }
                
                break;
            case State.Fall:
                //LAM CHO QUA BONG ROI XUONG
                yFall = _s0 + 0.5f * -g * _t * _t;
                transform.position = new Vector3(0, yFall, 2.5f);
                //LAM CHO QUA BONG BIEN DANG KHI ROI
                transform.localScale = new Vector3(1f + 0.6f * _t,1f - 0.2f * _t,1f);
                //PHAT HIEN THOI DIEM NHAY LEN
                if (yFall < disks.DiskList[0].transform.position.y + 1f)
                {
                    ChangeSate(State.Jump);
                    return;
                }

                if (Input.GetMouseButton(0) && disks.DiskList.Count > 2)
                {
                    ChangeSate(State.Smash);
                    return;
                }
                
                break;
            case State.Smash:
                if (Input.GetMouseButton(0))
                {
                    float countdownFurry = 5.5f * Time.deltaTime;
                    int count = 0;
                    transform.position -= new Vector3(0, 0.5f, 0)*0.5f;
                    if (transform.position.y < disks.DiskList[0].transform.position.y+0.5f && disks.DiskList.Count > 2)
                    {
                        theDestroy();
                        Destroy(disks.DiskList[0]);
                        disks.DiskList.Remove(disks.DiskList[0]);
                        count++;
                        if (furing == false)
                        {
                            whiteCircle.fillAmount += countdownFurry;
                        }
                        _undestroyable = 1;
                    }
                    _smax -= count * 1f;
                }
                if (whiteCircle.fillAmount == 1f)
                {
                    furing = true;
                    FireFury.SetActive(true);
                    checkFurry = true;
                }

                if (whiteCircle.fillAmount <= 0)
                {
                    furing = false;
                    FireFury.SetActive(false);
                    checkFurry = false;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    ChangeSate(State.Fall);
                    return;
                }
                break;
            case State.Die:
                break;
            default:
                return;
        }
        if (whiteCircle.fillAmount <= 0)
        {
            furing = false;
            FireFury.SetActive(false);
            checkFurry = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            ChangeSate(State.Fall);
            return;
        }
        _t += Time.deltaTime;
        whiteCircle.fillAmount -= 0.5f * Time.deltaTime;
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
                // gameObject.SetActive(false);
                disks.ChangeState(Gameplay.State.Lose);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

       private void theDestroy()
        {
            List<Transform> parent = new List<Transform>();
            for (int i = 0; i < disks.DiskList[0].transform.childCount; i++)
            {
                parent.Add(disks.DiskList[0].transform.GetChild(i));
            }
            List<Transform> childList = new List<Transform>();
            foreach (Transform child in parent)
            {
                Transform childDisk = Instantiate(child,disks.DiskList[0].transform.position + Vector3.up*1.5f, child.transform.rotation);
                childList.Add(childDisk);
                Rigidbody rb = childDisk.gameObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.AddForce(0,2f,- 10f,ForceMode.Impulse);
                Debug.Log(child.position);
            }
    
            for (int i = 0; i < 4; i++)
            {
                int temp = UnityEngine.Random.Range(0, childList.Count - 1);
                childList[temp].gameObject.GetComponent<Rigidbody>().AddForce(temp,5f,- 10f - temp,ForceMode.Impulse);
                childList[temp+1].gameObject.GetComponent<Rigidbody>().AddForce(temp - temp,1f+temp,- 10f - temp,ForceMode.Impulse);
            }
    }

    private void OnTriggerStay(Collider other)
    {
        if(_currentState == State.Smash && other.gameObject.CompareTag("Black_Piece") && checkFurry == false)
        {
            if (_undestroyable == 1)
            {
                var scaleSequence = DOTween.Sequence();
                scaleSequence.Append(gameObject.transform.DOScaleZ(2f, 3f))
                    .Append(gameObject.transform.DOScaleZ(2f, 5f));
                ChangeSate(State.Fall);
                _undestroyable--;
            }
            else
            {
                ChangeSate(State.Die);
            }
        }
        if (other.gameObject.CompareTag("Win_Piece")&& disks.DiskList.Count == 2f)
        {
            disks.ChangeState(Gameplay.State.Win);
        }
    }
} 