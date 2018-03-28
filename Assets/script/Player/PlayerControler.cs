using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {
    const int MAXSKILLNUM = 4;
    CharacterController controler;
    public cameraGun cameraGun;

    float speed;
    float glavity;
    float JumpPower;
    float vertVelosity;
    float minVertVelosity;
    public bool isJumping;

    public PlayerFoot foot;
    public GameObject skillObj;
    private Skill skill;// = new Skill();

    //スキルを入れる配列
    SkillType[] id = new SkillType[MAXSKILLNUM];

    float direction = 90f;
    float summonX = 3.6f;


    // Use this for initialization
    void Start () {
        this.controler = this.gameObject.GetComponent<CharacterController>();
        this.speed = 0.6f;
        this.glavity = 0.05f;
        this.JumpPower = 1.0f;
        this.vertVelosity = 0.0f;
        this.minVertVelosity = -4.0f;
        this.isJumping = false;

        //後々、敵からとったスキルが格納される
        this.id[0] = SkillType.NONE;
        this.id[1] = SkillType.NONE;
        this.id[2] = SkillType.NONE;
        this.id[3] = SkillType.NONE;

        //追加
        skill = skillObj.GetComponent<Skill>();
    }
	int SkillActivate(SkillType skillID)
    {
        int numSkillUseage = 0;
        switch (skillID)
        {
            case SkillType.NONE:
                break;
            case SkillType.HIGH_JUMP:
                numSkillUseage = skill.HighJump(ref JumpPower, this.isJumping);
                if (this.foot.stayGround)
                {
                    //ジャンプ力を挙げたジャンプ処理
                    this.isJumping = true;
                    this.vertVelosity = this.JumpPower;
                }
                this.JumpPower = 1;
                break;
            case SkillType.PUNCH:
                break;
            case SkillType.SLASH:
                Vector3 pos = new Vector3(
                    this.transform.position.x + summonX,
                    this.transform.position.y,
                    this.transform.position.z);
                numSkillUseage = skill.Slash(pos, new Vector3(0, direction, 0));
                break;
        }

        //スキルの残数を返す
        return numSkillUseage;
    }

    
    // Update is called once per frame
    void Update () {
        Vector3 vector = new Vector3(0.0f, 0.0f, 0.0f);

        float axis = Input.GetAxis("Horizontal");


        if (axis < 0) { direction = 180; summonX = -4.6f; }
        if (axis > 0) { direction = 0; summonX = 4.6f; }

        //〇　×　□　△　の順で、押されたらそれぞれに対応したスキルを発動
        string[] button = { "Play1", "Play2", "Play3", "Play4" };
        for(int i = 0; i < 4; ++i)
        {
            if (!Input.GetButtonDown(button[i]) || cameraGun.IsCameraUse())
            {
                continue;
            }
            Debug.Log(button[i]);
            //押してたら
            int numUsage = SkillActivate(this.id[i]);
            if (numUsage <= 0)
            {
                Debug.Log(this.id[i]);
                this.id[i] = SkillType.NONE;
            }
        }

        //ジャンプ
        if (Input.GetButtonDown("Fire1") && this.foot.stayGround)
        {
            this.isJumping = true;
            this.vertVelosity = this.JumpPower;
        }

        if (this.foot.stayGround && this.vertVelosity < -0.1f)
        {
            this.vertVelosity = -0.1f;
            this.isJumping = false;
        }

        //横移動 ＆ 縦移動
        vector += new Vector3(axis * speed, 0.0f, 0.0f);
        vector += new Vector3(0.0f, this.vertVelosity, 0.0f);
        this.vertVelosity -= this.glavity;
        if(this.vertVelosity < this.minVertVelosity)
        {
            this.vertVelosity = this.minVertVelosity;
        }

        this.controler.Move(vector);
    }

    //外からプレイヤーのスキルを変更する関数
    public void SetSkill(int position, SkillType type)
    {
        if(position >= MAXSKILLNUM || position < 0)
        {
            return;
        }

        this.id[position] = type;
    }
    //参照用
    public SkillType GetSkill(int position)
    {
        if (position >= MAXSKILLNUM || position < 0)
        {
            return SkillType.NONE;
        }

        return this.id[position];
    }
}
