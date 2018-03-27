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
    
    //
    bool isTrigger;
    bool isjumpHit;
    bool isSwordHit;
    float speed;
    int dir;
    int time;
    public int type;
    //

    const int MAXTIME = 60;
    // Use this for initialization
    void Start()
    {
        gun.GetComponent<SpriteRenderer>().enabled = false;
        isTrigger = false;
        isjumpHit = false;
        isSwordHit = false;
        dir = 1;
        speed = 1f;
        time = MAXTIME;
        createObj = attackObj.GetComponent<CreateAnim>();
       

    }

    private void OnTriggerStay(Collider other)
    {
        //押されたシャッターボタンの情報
        int shatter = GetButtonID();

        if (other.gameObject.tag == "Skeleton" ||
             other.gameObject.tag == "Goblin")
        {
            gun.GetComponent<Renderer>().material.color = new Color(255, 255, 0);
          
        }
         //カメラのシャッターを押す
        if (other.gameObject.tag == "Goblin")
        {

            isjumpHit = true;
            if (jumpObj.jumpFlag)
            {
                gun.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
              
                //使用回数を+5する
                if(shatter >= 0)
                {
                    Skill.quantity[(int)SkillType.HIGH_JUMP] += 5;
                    this.player.GetComponent<PlayerControler>().SetSkill(shatter, SkillType.HIGH_JUMP);
                    Debug.Log("ジャンプ+5");
                }
            }
        }
        if (other.gameObject.tag == "Skeleton")
        {
            isSwordHit = true;
            if (sword.animState == SkeletonAnim.AnimState.Attack)
            {
           
                gun.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                if (shatter >= 0)
                {
                    Skill.quantity[(int)SkillType.SLASH] += 5;
                    this.player.GetComponent<PlayerControler>().SetSkill(shatter, SkillType.SLASH);
                    Debug.Log("スラッシュ+5");
                }
            }
        }
    }

   void FixedUpdate()
    {
        //このフレームにおける初期値を設定
        //そのあとに判定があったら値を上書きしている
        isSwordHit = false;
        isjumpHit = false;
        gun.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
    }

    //プレイヤーの動きに連動してるので、プレイヤーが動いた後に更新を行うのが好ましい
    void LateUpdate()
    {
        Vector3 pos = player.transform.position;    //プレイヤーの位置を取得

        if (Input.GetButton("Fire3"))
        {
            isTrigger = true;
            gun.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            isTrigger = false;
            gun.GetComponent<SpriteRenderer>().enabled = false;
        }

        //板倉案、カメラは固定
        if (type == 0)
        {
            gun.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
            //カメラ向き
            if (Input.GetAxis("Horizontal") > 0)
            {
                gun.transform.position = new Vector3(pos.x + 10.0f, pos.y + 0.8f, pos.z);

            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                gun.transform.position = new Vector3(pos.x - 10.0f, pos.y + 0.8f, pos.z);
            }
        }

        //佐藤案、カメラをL1で射出できる
        if (type == 1)
        {
            this.transform.parent = null;
            //カメラ向き
            if (!isTrigger)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    dir = 1;
                }
                if (Input.GetAxis("Horizontal") < 0)
                {
                    dir = -1;
                }
                time = MAXTIME;
            }

            if (time >= MAXTIME)
            {
                gun.transform.position = new Vector3(pos.x, pos.y + 0.5f, pos.z);
            }

            if (isTrigger)
            {
                if (isjumpHit)
                {
                    gun.transform.position = jumpObj.transform.position + new Vector3(0.0f, 0.8f, 0.0f);
                }
                if (isSwordHit)
                {
                    gun.transform.position = sword.transform.position + new Vector3(0.0f, 0.8f, 0.0f);
                }
                if (!isSwordHit && !isjumpHit)
                {
                    gun.transform.position += new Vector3(speed * dir * ((float)time / (float)MAXTIME), 0, 0);
                }
                --time;
                if (time <= 0)
                {
                    time = 0;
                    //gun.GetComponent<SpriteRenderer>().enabled = false;
                    //isTrigger = false;
                }
            }
        }

    }


    //ボタン入力に応じて数字を返す。例によって仮の実装
    int GetButtonID()
    {
        if (!this.isTrigger)
        {
            return -1;
        }
        //〇　×　□　△　の順で、押されたらそれぞれに対応したスキルを発動
        string[] button = { "Play1", "Play2", "Play3", "Play4" };
        for (int i = 0; i < 4; ++i)
        {
            if (Input.GetButtonDown(button[i]))
            {
                return i;
            }
        }
        //指定の4ボタンのどれもが押されてない
        return -1;
    }

    //今カメラを構えているか？
    public bool IsCameraUse()
    {
        return this.isTrigger;
    }
}
