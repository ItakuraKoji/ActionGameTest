using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraGun : MonoBehaviour
{

    public Jump jumpObj;
    public SkeletonAnim sword;
    public GameObject gun;
    public GameObject player;

    public GameObject attackObj;
    private CreateAnim createObj;
    private float createObjAngle = 0.0f;  //生成するオブジェクトの向き
    // Use this for initialization
    void Start()
    {
        createObj = attackObj.GetComponent<CreateAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        //カメラのシャッターを押す

        if (jumpObj.jumpFlag)
        {

            //ハイジャンプ使用回数を+5する
            if (Input.GetButtonDown("Fire3"))
            {
                Skill.quantity[(int)SkillType.HIGH_JUMP] += 5;
                Debug.Log("ジャンプ+5");
            }
        }
        if (sword.animState == SkeletonAnim.AnimState.Attack)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                Skill.quantity[(int)SkillType.SLASH] += 5;
                Debug.Log("スラッシュ+5");
            }
        }
        if (Input.GetAxis("Horizontal") > 0 )
        {
            gun.transform.position = new Vector3( pos.x + 3.6f, pos.y, pos.z);
            createObjAngle = 90;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            gun.transform.position = new Vector3(pos.x - 3.6f, pos.y, pos.z);
            createObjAngle = -90;
        }

       // PlayBackMotion();
    }

    //敵がジャンプしているか
    bool EnemyJump()
    {
        return jumpObj.jumpFlag;
    }



    
    //--------------------------------------------
    //再生する機能
    //--------------------------------------------
    public void    PlayBackSwordMove()
    {
        //gキーを押せば、swordのオブジェを生成する
        //オブジェクトを生成する
        if (Input.GetKeyDown("g"))
        {
            createObj.CreateEnemy();
        }
    }

    public void    PlayBackMotion()
    {
        //LBボタンを押すと、再生する
        //if(Input.GetButtonDown("Attack") && Skill.quantity[(int)SkillType.SLASH] > 0)
        {
           //Skill.quantity[(int)SkillType.SLASH]--;
           Debug.Log("オブジェクトを生成しました");
           createObj.CreateObj(gun.transform.position, new Vector3(0, createObjAngle, 0));
        }
    }

    //--------------------------------------------
    //撮影する機能(動作を撮影する)
    //--------------------------------------------
    void    TakingAction()
    {
        //撮影したとき
        switch(ReturnEnemyID())
        {
            case 0: //ハイジャンプする機能をゲット(カウントを増やす?)
                break;
            case 1: //攻撃する機能をゲット(カウントを増やす?)
                break;
                           
            case -1:
                //撮影に失敗した
            break;
        }
    }

    //撮影した敵のIDを返す
    int    ReturnEnemyID()
    {
        //まず、撮影ボタンが押されているか
        if(Input.GetButtonDown("Fire3"))
        {
            //そのときに、カメラの当たり判定に入っていれば、
            //撮影ができる
            //if(CameraErea()){ //撮影OK }
        }
        return -1;
    }
}
