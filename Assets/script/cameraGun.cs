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
    GameObject rockonTarget;
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
        this.rockonTarget = null;
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
        //とにかくカーソルが出現してないときは判定しない
        //敵キャラ以外には判定を行わない
        if (!this.isTrigger || other.gameObject.tag != "Enemy")
        {
            return;
        }
        this.rockonTarget = other.gameObject;


        //押されたシャッターボタンの情報
        int shatter = GetButtonID();

        EnemyState state = other.GetComponent<EnemyState>();
        if(state.GetSkillType() == SkillType.NONE)
        {
            gun.GetComponent<Renderer>().material.color = new Color(255, 255, 0);
        }
        else
        {
            gun.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            SkillType skill = state.GetSkillType();

            if (shatter >= 0)
            {
                Skill.quantity[(int)skill] += 5;
                if (type == 2)
                {
                    //操作タイプ2だけ特別、割り当て場所は固定
                    this.player.GetComponent<PlayerControler>().SetSkill(1, skill);
                }
                else
                {
                    this.player.GetComponent<PlayerControler>().SetSkill(shatter, skill);
                }
            }
        }

        //こっから下は古い処理、そのうち消す
        return;


        if (other.gameObject.tag == "Skeleton" ||
             other.gameObject.tag == "Goblin")
        {
            gun.GetComponent<Renderer>().material.color = new Color(255, 255, 0);
          
        }
         //カメラのシャッターを押す
        if (other.gameObject.tag == "Goblin")
        {
            this.rockonTarget = other.gameObject;

            isjumpHit = true;
            if (jumpObj.jumpFlag)
            {
                gun.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
              
                //使用回数を+5する
                if(shatter >= 0)
                {
                    Skill.quantity[(int)SkillType.HIGH_JUMP] += 5;
                    if (type == 2)
                    {
                        //操作タイプ2だけ特別、割り当て場所は固定
                        this.player.GetComponent<PlayerControler>().SetSkill(1, SkillType.HIGH_JUMP);
                    }
                    else
                    {
                        this.player.GetComponent<PlayerControler>().SetSkill(shatter, SkillType.HIGH_JUMP);
                    }
                    Debug.Log("ジャンプ+5");
                }
            }
        }
        if (other.gameObject.tag == "Skeleton")
        {
            this.rockonTarget = other.gameObject;

            isSwordHit = true;
            if (sword.animState == SkeletonAnim.AnimState.Attack)
            {
           
                gun.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                if (shatter >= 0)
                {
                    Skill.quantity[(int)SkillType.SLASH] += 5;
                    if (type == 2)
                    {
                        //操作タイプ2だけ特別、割り当て場所は固定
                        this.player.GetComponent<PlayerControler>().SetSkill(3, SkillType.SLASH);
                    }
                    else
                    {
                        this.player.GetComponent<PlayerControler>().SetSkill(shatter, SkillType.SLASH);
                    }
                    Debug.Log("スラッシュ+5");
                }
            }
        }
    }

   void FixedUpdate()
    {
        //このフレームにおける初期値を設定
        //そのあとに判定があったら値を上書きしている
        this.rockonTarget = null;
        isSwordHit = false;
        isjumpHit = false;
        gun.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
    }

    //プレイヤーの動きに連動してるので、プレイヤーが動いた後に更新を行うのが好ましい
    void LateUpdate()
    {

        if (type == 0 || type == 1)
        {
            if (Input.GetButton("Fire3"))
            {
                this.isTrigger = true;
                gun.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                this.isTrigger = false;
                gun.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        //板倉案、カメラは固定
        if (type == 0)
        {
            Type0Update();
        }

        //佐藤案亜種、カメラをL1押しっぱなしで射出できる
        if (type == 1)
        {
            Type1Update();
        }

        //佐藤案、L1でカメラ射出し、L1で撮影
        if(type == 2)
        {
            Type2Update();
        }

    }

    void Type0Update()
    {
        Vector3 pos = player.transform.position;    //プレイヤーの位置を取得
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
    void Type1Update()
    {
        Vector3 pos = player.transform.position;    //プレイヤーの位置を取得
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
            if (this.rockonTarget != null)
            {
                gun.transform.position = this.rockonTarget.transform.position + new Vector3(0.0f, 0.8f, 0.0f);
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
    void Type2Update()
    {
        Vector3 pos = player.transform.position;    //プレイヤーの位置を取得
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

        if (Input.GetButtonDown("Fire3"))
        {
            this.isTrigger = true;
            gun.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (isTrigger)
        {
            if (this.rockonTarget != null)
            {
                gun.transform.position = this.rockonTarget.transform.position + new Vector3(0.0f, 0.8f, 0.0f);
            }
            if (!isSwordHit && !isjumpHit)
            {
                gun.transform.position += new Vector3(speed * dir, 0, 0);
            }
            --time;
            if (time <= 0)
            {
                time = 0;
                gun.GetComponent<SpriteRenderer>().enabled = false;
                isTrigger = false;
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
        if (type == 0 || type == 1)
        {
            //〇　×　□　△　の順で、押されたらそれぞれに対応したスキルを発動
            string[] button = { "Play1", "Play2", "Play3", "Play4" };
            for (int i = 0; i < 4; ++i)
            {
                if (Input.GetButtonDown(button[i]))
                {
                    return i;
                }
            }
        }else
        {
            //操作タイプ2の場合
            if (Input.GetButtonDown("Fire3"))
            {
                //負の数じゃないならぶっちゃけなんでもいい
                return 100;
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
