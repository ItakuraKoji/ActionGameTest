using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    //------------------------------------
    //定数
    public enum State
    {
        Non,Idle,Jump
    };

    //-------------------------------------
    //変数
    State state;
    int frameTime;
    int timeCnt;
    public bool jumpFlag = false;
    public int idleTime;
    public int jumpTime;
    public Animator anim;
    public float jumpPow;
    public int jumpTimePunctuation;

    CharacterController charaCon;
    public float gravity = 20.0f;
    Vector3 moveDir;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        frameTime = 0;
        timeCnt = 0;
        state = State.Idle;
        charaCon = GetComponent<CharacterController>();
        moveDir = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        charaCon.Move(moveDir * Time.deltaTime);
        if (AnimTimeCnt(jumpTimePunctuation))
        {
            MoveJump();
            TimeReset();
            if (!jumpFlag)
            {
                anim.SetBool("jump", false);
            }
            else
            {
                anim.SetBool("jump", true);
            }
        }
        else
        {
            jumpFlag = false;
            TimeCnt();
            anim.SetBool("idle", true);
            moveDir.y -= gravity * Time.deltaTime;
        }
        // MoveAnim();
        if (charaCon.isGrounded)
        {

            jumpFlag = false;

        }
        else
        {
            jumpFlag = true;

        }
    }

    //----------------------------------
    //秒をカウントする
    //----------------------------------
    void    TimeCnt()
    {
        frameTime++;
        if(frameTime >= 60)
        {
            timeCnt++;
            frameTime = 0;
        }
    }

    //----------------------------------
    //アニメーションの時間
    //----------------------------------
    bool    AnimTimeCnt(int time)
    {
        if(timeCnt >= time)
        {
            timeCnt = 0;
            return true;
        }
        return false;
    }

    //----------------------------------
    //時間のリセット
    //----------------------------------
    void    TimeReset()
    {
        timeCnt = 0;
        frameTime = 0;
    }

    //----------------------------------
    //ジャンプ処理
    //----------------------------------
    void    MoveAnim()
    {
        switch(state)
        {
            case State.Idle:    //待機
                TimeCnt();
                if (AnimTimeCnt(idleTime))
                {
                    anim.SetBool("ToJump", true);
                    anim.SetBool("ToIdle", false);
                    state = State.Jump;
                    TimeReset();
                    MoveJump();
                }
                break;
            case State.Jump:    //ジャンプ
                TimeCnt();
                //MoveJump();
                if (AnimTimeCnt(jumpTime))
                {
                    anim.SetBool("ToIdle", true);
                    anim.SetBool("ToJump", false);
                    state = State.Idle;
                    TimeReset();
                }
                break;
            case State.Non:     //何もしない
                break;
        }
    }

    //-----------------------------------
    //ジャンプ処理
    //-----------------------------------
    void    MoveJump()
    {
        float power = jumpPow;
        if (charaCon.isGrounded)
        {
             
             moveDir.y = power;
          
        }
        else
        {
            power = 0;
           
        }
    }

    //------------------------------------
    //ジャンプしているかを返す
    //------------------------------------
    bool    NowJumpDuring()
    {
        return jumpFlag;
    }
}
