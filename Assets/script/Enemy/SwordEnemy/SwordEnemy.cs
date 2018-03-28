using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemy : MonoBehaviour {
    CharacterController controller;
    EnemyState state;
    public Animator anim;
    public GameObject attackEffect;

    //ここら辺プレイヤーの使いまわし
    float glavity;
    float horiVelosity;
    float vertVelosity;
    float minVertVelosity;
    int timeCnt;

    // Use this for initialization
    void Start()
    {
        this.state = this.GetComponent<EnemyState>();
        this.controller = this.GetComponent<CharacterController>();
        this.glavity = 0.05f;
        this.horiVelosity = 0.0f;
        this.vertVelosity = 0.0f;
        this.minVertVelosity = -1.0f;
        this.timeCnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Action();

        //重力移動のみ
        Vector3 vector = new Vector3(0.0f, 0.0f, 0.0f);
        vector += new Vector3(this.horiVelosity, this.vertVelosity, 0.0f);
        this.vertVelosity -= this.glavity;
        if (this.vertVelosity < this.minVertVelosity)
        {
            this.vertVelosity = this.minVertVelosity;
        }

        this.controller.Move(vector);

    }

    //行動パターン
    void Action()
    {
        //プレイヤーの方向を見ながら、剣を振る
        if(this.timeCnt == 30)
        {
            this.anim.SetBool("preAttack", true);
        }
        if(this.timeCnt == 60)
        {
            this.state.SetSkillType(SkillType.SLASH);
            Instantiate(this.attackEffect, this.transform);
            this.attackEffect.transform.position = new Vector3(6.0f, 2.0f, 0.0f);
            this.anim.SetBool("isAttack", true);
        }
        if (this.timeCnt == 120)
        {
            this.state.SetSkillType(SkillType.NONE);
            this.anim.SetBool("preAttack", false);
            this.anim.SetBool("isAttack", false);
        }
        if (this.timeCnt == 150)
        {
            this.timeCnt = 0;
        }




        ++this.timeCnt;
    }
}
