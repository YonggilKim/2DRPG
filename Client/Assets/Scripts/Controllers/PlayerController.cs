﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Define;

public class PlayerController : MonoBehaviour
{
    
    float _speed = 5.0f;
    public Grid _grid;
    Vector3Int _celPos = Vector3Int.zero;
    bool _isMoving = false;
    Animator _animator;
    MoveDir _dir = MoveDir.Down;
    public MoveDir Dir
    {
        get { return _dir; }
        set
        {
            if (_dir == value)
                return;
            switch (value)
            {
                case MoveDir.Up:
                    _animator.Play("WALK_BACK");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.Down:
                    _animator.Play("WALK_FRONT");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.Left:
                    _animator.Play("WALK_RIGHT");
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.Right:
                    _animator.Play("WALK_RIGHT");
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    break;
                case MoveDir.None:
                    if (_dir == MoveDir.Up)//위로가는방향이였으면,
                    {
                        _animator.Play("IDLE_BACK");
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    else if (_dir == MoveDir.Down)
                    {
                        _animator.Play("IDLE_FRONT");
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    else if (_dir == MoveDir.Left)
                    {
                        _animator.Play("IDLE_RIGHT");
                        transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    }
                    else
                    {
                        _animator.Play("IDLE_RIGHT");
                        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                    break;
            }
            _dir = value;
        }
    }

    #region LifeCycle
    void Start()
    {
        _animator = GetComponent<Animator>();
        Vector3 pos = _grid.CellToWorld(_celPos) + new Vector3(0.5f, 0.64f);
        transform.position = pos;
    }

    void Update()
    {
        GetDirInput();
        UpdateIsMoving();
        UpdatePosition();
    }
    #endregion

    #region Player Move Functions
    //키보드 입력받아 방향설정
    void GetDirInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Dir = MoveDir.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Dir = MoveDir.Down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Dir = MoveDir.Left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Dir = MoveDir.Right;
        }
        else
        {
            Dir = MoveDir.None;
        }
    }
    //셀단위로 스르르움직이는거 구현
    void UpdatePosition()
    {
        if (_isMoving == false) return;

        Vector3 destPos = _grid.CellToWorld(_celPos) + new Vector3(0.5f, 0.64f);
        Vector3 moveDir = destPos - transform.position;

        //도착 여부 체크
        float dist = moveDir.magnitude;
        if (dist < _speed * Time.deltaTime)
        {
            transform.position = destPos;
            _isMoving = false;
        }
        else
        {
            transform.position += moveDir.normalized * _speed * Time.deltaTime;// 스르르움직임
            _isMoving = true;
        }

    }
    //이동가능 상태일때 실제좌표로 이동
    void UpdateIsMoving()
    {
        if (_isMoving == false)
        {
            switch (Dir)
            {
                case MoveDir.Up:
                    _celPos += Vector3Int.up;
                    _isMoving = true;
                    break;
                case MoveDir.Down:
                    _celPos += Vector3Int.down;
                    _isMoving = true;
                    break;
                case MoveDir.Left:
                    _celPos += Vector3Int.left;
                    _isMoving = true;
                    break;
                case MoveDir.Right:
                    _celPos += Vector3Int.right;
                    _isMoving = true;
                    break;
            }
        }
    }
    #endregion
}
