using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {
    const int MAXSKILLNUM = 4;
    CharacterController controler;


    float speed;
    float glavity;
    float JumpPower;
    float vertVelosity;
    float minVertVelosity;
    public bool isJumping;

    public AudioClip jumpclip;
    private AudioSource jumpSe;
    public PlayerFoot foot;
    public GameObject skillObj;
    private Skill skill;// = new Skill();
    SkillType[] id = new SkillType[MAXSKILLNUM];

    float direction = 90f;
    float summonX = 3.6f;
    // Use this for initialization
    void Start () {
        this.controler = this.gameObject.GetComponent<CharacterController>();
        this.speed = 0.8f;
        this.glavity = 0.05f;
        this.JumpPower = 1.0f;
        this.vertVelosity = 0.0f;
        this.minVertVelosity = -1.0f;
        this.jumpSe = gameObject.AddComponent<AudioSource>();
        this.jumpSe.clip = this.jumpclip;
        this.isJumping = false;
        for(int i = 0; i < MAXSKILLNUM; ++i)
        {
            this.id[i] = SkillType.NONE;
        }
        //仮処理。撮影したらこんな感じになってその動作が使えるようになる
        this.id[1] = SkillType.HIGH_JUMP;
        this.id[2] = SkillType.PUNCH;
        this.id[3] = SkillType.SLASH;

        //追加
        skill = skillObj.GetComponent<Skill>();
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
                    skill.HighJump(ref JumpPower,this.isJumping);
               
                    break;
                case SkillType.PUNCH:
                    break;
                case SkillType.SLASH:
                    Vector3 pos = new Vector3(
                        this.transform.position.x + summonX,
                        this.transform.position.y,
                        this.transform.position.z);
                    skill.Slash(pos,new Vector3(0,direction,0));
                    break;
            }
        }

    }

    
    // Update is called once per frame
    void Update () {
        Vector3 vector = new Vector3(0.0f, 0.0f, 0.0f);

        float axis = Input.GetAxis("Horizontal");
        vector += new Vector3(axis * speed, 0.0f, 0.0f);

       
       if(axis < 0) { direction = -90;  summonX = -4.6f; }
       if(axis > 0) { direction = 90;  summonX = 4.6f; }

        SkillActivate();
        if (Input.GetButtonDown("Fire1") && this.foot.stayGround)
        {

            jumpSe.Play();
            this.isJumping = true;
            this.vertVelosity = this.JumpPower;
        }

        if (this.foot.stayGround && this.vertVelosity < -0.1f)
        {
            this.vertVelosity = -0.1f;
            this.isJumping = false;
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
