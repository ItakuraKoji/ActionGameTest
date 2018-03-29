using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    const int MAXSKILLNUM = 4;
    CharacterController controler;
    public cameraGun cameraGun;

    float speed;
    float glavity;
    float JumpPower;
    float vertVelosity;
    float minVertVelosity;
    public bool isJumping;
    public int life = 20;

    public AudioClip jumpclip;
    private AudioSource jumpSe;
    public PlayerFoot foot;
    public GameObject skillObj;
    private Skill skill;

    //スキルを入れる配列
    SkillInfo[] id = new SkillInfo[MAXSKILLNUM];

    float direction = 90f;
    float summonX = 3.6f;


    // Use this for initialization
    void Start()
    {
        this.controler = this.gameObject.GetComponent<CharacterController>();
        this.speed = 0.6f;
        this.glavity = 0.05f;
        this.JumpPower = 1.0f;
        this.vertVelosity = 0.0f;
        this.minVertVelosity = -4.0f;
        this.jumpSe = gameObject.AddComponent<AudioSource>();
        this.jumpSe.clip = this.jumpclip;
        this.isJumping = false;

        //後々、敵からとったスキルが格納される
        this.id[0].type = SkillType.NONE;
        this.id[1].type = SkillType.NONE;
        this.id[2].type = SkillType.NONE;
        this.id[3].type = SkillType.NONE;

        //追加
        skill = skillObj.GetComponent<Skill>();
    }
<<<<<<< HEAD
	bool SkillActivate(SkillType skillID)
=======
    int SkillActivate(SkillType skillID)
>>>>>>> master
    {
        bool isUsed = false;
        switch (skillID)
        {
            case SkillType.NONE:
                break;
            case SkillType.HIGH_JUMP:
                if (this.foot.stayGround)
                {
                    //ジャンプ力を上げたジャンプ処理
                    skill.HighJump(ref JumpPower, this.isJumping);
                    this.isJumping = true;
                    this.vertVelosity = this.JumpPower;
                    this.JumpPower = 1;
                    isUsed = true;
                }
                break;
            case SkillType.PUNCH:
                break;
            case SkillType.SLASH:
                Vector3 pos = new Vector3(
                    this.transform.position.x + summonX,
                    this.transform.position.y,
                    this.transform.position.z);
                skill.Slash(pos, new Vector3(0, direction, 0));
                isUsed = true;
                break;
        }

        //スキルの残数を返す
        return isUsed;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 vector = new Vector3(0.0f, 0.0f, 0.0f);

        float axis = Input.GetAxis("Horizontal");


        if (axis < 0) { direction = 180; summonX = -4.6f; }
        if (axis > 0) { direction = 0; summonX = 4.6f; }

        //〇　×　□　△　の順で、押されたらそれぞれに対応したスキルを発動
        string[] button = { "Play1", "Play2", "Play3", "Play4" };
        for (int i = 0; i < 4; ++i)
        {
            if (!Input.GetButtonDown(button[i]) || cameraGun.IsCameraUse())
            {
                continue;
            }
            Debug.Log(button[i]);
            //押してたら
            if (SkillActivate(this.id[i].type))
            {
                //スキルを実際に使ったら消費
                --this.id[i].numUsage;
            }
            if (this.id[i].numUsage <= 0)
            {
                Debug.Log(this.id[i]);
                this.id[i].type = SkillType.NONE;
            }
        }

        //ジャンプ
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

        //横移動 ＆ 縦移動
        vector += new Vector3(axis * speed, 0.0f, 0.0f);
        vector += new Vector3(0.0f, this.vertVelosity, 0.0f);
        this.vertVelosity -= this.glavity;
        if (this.vertVelosity < this.minVertVelosity)
        {
            this.vertVelosity = this.minVertVelosity;
        }

        this.controler.Move(vector);
    }

    //外からプレイヤーのスキルを変更する関数
    public void SetSkill(int position, SkillType type, int numUsage)
    {
        if (position >= MAXSKILLNUM || position < 0)
        {
            return;
        }

        this.id[position].type = type;
        this.id[position].numUsage = numUsage;
    }
    //参照用
    public SkillInfo GetSkill(int position)
    {
        if (position >= MAXSKILLNUM || position < 0)
        {
            SkillInfo info;
            info.numUsage = 0;
            info.type = SkillType.NONE;
            return info;
        }

        return this.id[position];
    }

    //プレイヤーの体力を減らす処理
    public void DecrementLife(int damage)
    {
        if (Active())
        {
            for (int i = 0; i < damage; ++i)
            {
                if (Active())
                {
                    life--;
                }
                else
                {
                    break;
                }
            }
            Died();
        }
    }

    //プレイヤーの体力が0になった
    bool Died()
    {
        if (life <= 0)
        {
            Debug.Log("プレイヤーが死亡します");
            return true;
        }
        return false;
    }

    bool Active()
    {
        if (life > 0)
        {
            return true;
        }
        return false;
    }
}
