using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemy : MonoBehaviour {
    CharacterController controller;
    EnemyState state;
    public Animator anim;

    //ここら辺プレイヤーの使いまわし
    float jumpPower;
    float glavity;
    float horiVelosity;
    float vertVelosity;
    float minVertVelosity;
    int timeCnt;

    // Use this for initialization
    void Start () {
        this.state = this.GetComponent<EnemyState>();
        this.controller = this.GetComponent<CharacterController>();
        this.jumpPower = 1.0f;
        this.glavity = 0.05f;
        this.horiVelosity = 0.0f;
        this.vertVelosity = 0.0f;
        this.minVertVelosity = -1.0f;
        this.timeCnt = 0;
    }

    // Update is called once per frame
    void Update () {
        Action();

        //重力移動のみ
        Vector3 vector = new Vector3(0.0f, 0.0f, 0.0f);
        vector += new Vector3(this.horiVelosity, this.vertVelosity, 0.0f);
        this.vertVelosity -= this.glavity;
        if(this.vertVelosity < this.minVertVelosity)
        {
            this.vertVelosity = this.minVertVelosity;
        }

        this.controller.Move(vector);

	}

    //行動パターン
    void Action()
    {
        //ハイジャンプは 90 から ループ後の 30 まで

        //小ジャンプ2回
        if(this.timeCnt == 30)
        {
            //ハイジャンプ終わり
            this.state.SetSkillType(SkillType.NONE);
            anim.SetBool("isJump", true);
            this.horiVelosity = 0.2f;
            this.vertVelosity = this.jumpPower * 0.6f;
        }
        if (this.timeCnt == 60)
        {
            anim.SetBool("isJump", true);
            transform.Rotate(0, 180, 0);
            this.horiVelosity = -0.4f;
            this.vertVelosity = this.jumpPower * 0.6f;
        }

        if (this.timeCnt == 90)
        {
            //ハイジャンプ開始
            anim.SetBool("isJump", true);
            this.state.SetSkillType(SkillType.HIGH_JUMP);
            transform.Rotate(0, 180, 0);
            this.horiVelosity = 0.2f;
            this.vertVelosity = this.jumpPower;
        }
        if(this.timeCnt == 120)
        {
            //ループする
            anim.SetBool("isJump", false);
            this.horiVelosity = 0.0f;
            this.timeCnt = 0;
        }

        //移動に緩急をつける
        this.horiVelosity *= 0.9f;

        ++this.timeCnt;
    }
}
