using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {
    const int MAXSKILLNUM = 5;
    CharacterController controler;
    float speed;
    float glavity;
    float JumpPower;
    float vertVelosity;
    float minVertVelosity;
    public PlayerFoot foot;
    Skill skill = new Skill();
    SkillType[] id = new SkillType[MAXSKILLNUM];
    // Use this for initialization
    void Start () {
        this.controler = this.gameObject.GetComponent<CharacterController>();
        this.speed = 0.8f;
        this.glavity = 0.05f;
        this.JumpPower = 1.0f;
        this.vertVelosity = 0.0f;
        this.minVertVelosity = -1.0f;
        for(int i = 0; i < MAXSKILLNUM; ++i)
        {
            this.id[i] = SkillType.NONE;
        }
        this.id[1] = SkillType.HIGH_JUMP;
    }
	void SkillActivate()
    {
        for (int i = 0; i < MAXSKILLNUM; ++i)
        {
            switch (this.id[i])
            {
                case SkillType.NONE:
                    break;
                case SkillType.HIGH_JUMP:
                    skill.HighJump(ref JumpPower,this.foot.stayGround);
                    break;
            }
        }

    }
    // Update is called once per frame
    void Update () {
        Vector3 vector = new Vector3(0.0f, 0.0f, 0.0f);

        float axis = Input.GetAxis("Horizontal");
        vector += new Vector3(axis * speed, 0.0f, 0.0f);

        
        
        SkillActivate();

        if (Input.GetButtonDown("Fire1") && this.foot.stayGround)
        {
          
            this.vertVelosity = this.JumpPower;
        }
        if (this.foot.stayGround && this.vertVelosity < -0.1f)
        {
            this.vertVelosity = -0.1f;
        }
        vector += new Vector3(0.0f, this.vertVelosity, 0.0f);
        this.vertVelosity -= this.glavity;
        if(this.vertVelosity < this.minVertVelosity)
        {
            this.vertVelosity = this.minVertVelosity;
        }

        this.controler.Move(vector);
    }
}
