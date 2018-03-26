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

    private bool isTrigger;
    public bool isjumpHit;
    public bool isSwordHit;
    int time;
    public int type;

    const int MAXTIME = 60;
    // Use this for initialization
    void Start()
    {
        isTrigger = false;
        isjumpHit = false;
        isSwordHit = false;
    time = MAXTIME;
        createObj = attackObj.GetComponent<CreateAnim>();
       

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Skeleton" ||
             other.gameObject.tag == "Goblin")
        {
            gun.GetComponent<Renderer>().material.color = new Color(255, 255, 0);
          
        }
         //カメラのシャッターを押す
        if (other.gameObject.tag == "Goblin")
        {
            isjumpHit = true;
            Debug.Log("J_Hit!!!!!!!!");
           
            if (jumpObj.jumpFlag)
            {
                gun.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
              
                //使用回数を+5する
                if (Input.GetButtonDown("Fire3"))
                {
                    Skill.quantity[(int)SkillType.HIGH_JUMP] += 5;
                    Debug.Log("ジャンプ+5");
                }
            }
            else
            {
                isjumpHit = false;
            }
          
        }
        if (other.gameObject.tag == "Skeleton")
        {
            isSwordHit = true;
            if (sword.animState == SkeletonAnim.AnimState.Attack)
            {
           
                gun.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
                if (Input.GetButtonDown("Fire3"))
                {
                    Skill.quantity[(int)SkillType.SLASH] += 5;
                    Debug.Log("スラッシュ+5");
                }
            }
            Debug.Log("S_Hit!!!!!!!!");
        }
        else
        {
            isSwordHit = false;
        }
      

    }

    //色が変わらなくなるのを防ぐ
   void FixedUpdate()
    {
        Vector3 pos = player.transform.position;    //プレイヤーの位置を取得
        if (type == 1)
        {

            if (time >= MAXTIME)
            {
                gun.transform.position = new Vector3(pos.x, pos.y, pos.z);
            }
            if (Input.GetButtonDown("Fire3"))
            {
                isTrigger = true;
            }
            if (isTrigger)
            {
                if (isjumpHit)
                {
                    gun.transform.position = jumpObj.transform.position;
                }
                if (isSwordHit)
                {
                    gun.transform.position = sword.transform.position;
                }
                if (!isSwordHit && !isjumpHit)
                {
                    gun.transform.position += new Vector3(1f, 0, 0);
                }

                --time;
                if (time <= 0)
                {
                    isTrigger = false;
                    time = MAXTIME;
                }
            }
        }
        gun.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;    //プレイヤーの位置を取得
        if (type == 0)
        {
            //カメラ向き
            if (Input.GetAxis("Horizontal") > 0)
            {
                gun.transform.position = new Vector3(pos.x + 3.6f, pos.y, pos.z);

            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                gun.transform.position = new Vector3(pos.x - 3.6f, pos.y, pos.z);

            }
        }
      

    }

    private void LateUpdate()
    {
       
    }
}
