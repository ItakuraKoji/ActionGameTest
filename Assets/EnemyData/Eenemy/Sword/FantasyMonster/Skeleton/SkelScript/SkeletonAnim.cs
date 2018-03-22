using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnim : MonoBehaviour {
    //---------------------------
    //定数
    const float FRAME = 60.0f;

    public enum AnimState
    {
        Idle,Walk,Run,Attack,Non
    }
    //---------------------------
    //変数
    float frameCnt;
    float timeCnt;
    Animator animator;

    //---------------------------
    //外部で指定する変数
    public int walkTime;
    public int idleTime;
    public int attackTime;
    public int lostPlayerTime;

    public AnimState animState;     //アニメーションの状態

    //視界
    public GameObject viewErea;
    ViewEreaHit viewHitErea;
    //攻撃範囲
    public GameObject attackErea;
    PlayerHit attackHitErea;

    // Use this for initialization
    void Start () {
        frameCnt = 0.0f;
        timeCnt = 0.0f;
        animState = AnimState.Non;
        animator = GetComponent<Animator>();

        //視界
        viewHitErea = viewErea.GetComponent<ViewEreaHit>();
        //攻撃範囲
        attackHitErea = attackErea.GetComponent<PlayerHit>();
    }
	
	// Update is called once per frame
	void Update () {
        AnimTransition();
	}


    //-----------------------------------------
    //時間をリセットする
    //-----------------------------------------
    void    TimeReset()
    {
        frameCnt = 0.0f;
        timeCnt = 0.0f;
    }

    //-----------------------------------------
    //時間(秒)を数える
    //-----------------------------------------
    void    TimeCnt()
    {
        frameCnt++;
        if(frameCnt >= FRAME)
        {
            frameCnt = 0.0f;
            timeCnt++;
        }
    }

    //--------------------------------
    //アニメーションさせる時間が来たら、true
    //引数1:アニメーションの時間(秒)
    //戻り値:指定時間(秒)になったら、true
    //--------------------------------
    bool    AnimTimeCnt(int animTimeCnt)
    {
        if(timeCnt >= (float)animTimeCnt)
        {
            timeCnt = 0.0f;
            return true;
        }
        return false;
    }


    //---------------------------------
    //アニメーション遷移
    //---------------------------------
    void       AnimTransition()
    {
        switch (animState)
        {
            case AnimState.Idle:    //待機//
                TimeCnt();
                if(AnimTimeCnt(idleTime))
                {
                    animator.SetBool("ToWalk", true);
                    animator.SetBool("ToIdle", false);
                    TimeReset();
                    animState = AnimState.Walk;
                    Debug.Log("歩くモードへ");
                }
                if(RunAnim())
                {
                    animator.SetBool("ToRun", true);
                    animator.SetBool("ToIdle", false);
                    TimeReset();
                    animState = AnimState.Run;
                    Debug.Log("走るモードへ");
                }
                break;
            case AnimState.Walk:    //歩く//
                TimeCnt();
                if(AnimTimeCnt(walkTime))
                {
                    animator.SetBool("ToIdle", true);
                    animator.SetBool("ToWalk", false);
                    TimeReset();
                    animState = AnimState.Idle;
                    Debug.Log("待機モードへ");
                }
                if(RunAnim())
                {
                    animator.SetBool("ToRun", true);
                    animator.SetBool("ToWalk", false);
                    TimeReset();
                    animState = AnimState.Run;
                    Debug.Log("走るモードへ");
                }
                break;
            case AnimState.Run:     //走る//
                if (ExitRunErea())
                {
                    animator.SetBool("ToWalk", true);
                    animator.SetBool("ToRun", false);
                    animState = AnimState.Walk;
                    Debug.Log("視界から外れました");
                }
                if (AttackAnim())
                {
                    animator.SetBool("ToAttack", true);
                    animator.SetBool("ToRun", false);
                    TimeReset();
                    animState = AnimState.Attack;
                    Debug.Log("攻撃モードへ");
                }
                break;
            case AnimState.Attack:  //攻撃//
                TimeCnt();
                if (AnimTimeCnt(attackTime))
                {
                    animator.SetBool("ToIdle", true);
                    animator.SetBool("ToAttack", false);
                    TimeReset();
                    animState = AnimState.Idle;
                    Debug.Log("待機状態へ");
                }
                break;
            case AnimState.Non:     //何もしない//
                animState = AnimState.Idle;
                Debug.Log("最初は待機状態へ");
                break;
        }
    }


    //---------------------------------------
    //アニメーションの状態を取得する
    public AnimState   GetAnimState()
    {
        return animState;
    }


    //---------------------------------------
    //走るアニメーションに入る判定
    //---------------------------------------
    bool RunAnim()
    {
        //待機状態 or 歩く状態の時
        if (animState == AnimState.Idle ||
            animState == AnimState.Walk )
        {
            if (viewHitErea.ViewErea())
            {
                return true;
            }
        }
        return false;
    }

    bool ExitRunErea()
    {
        if(viewHitErea.ExitViewErea())
        {
            return true;
        }
        return false;
    }
    //---------------------------------------
    //攻撃アニメーションに入る判定
    //---------------------------------------
    bool   AttackAnim()
    {
        //走っている状態の時
        if(animState == AnimState.Idle || 
            animState == AnimState.Walk || animState == AnimState.Run)
        {
            if(attackHitErea.HitToPlayer())
            {
                Debug.Log("攻撃範囲に入りました");
                return true;
            }
        }
        return false;
    }
}
