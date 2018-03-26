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

  
    public int type;

    //private float createObjAngle = 0.0f;  //生成するオブジェクトの向き
    // Use this for initialization
    void Start()
    {
      
        createObj = attackObj.GetComponent<CreateAnim>();

        if (type == 1)
        {
            //カメラ初期位置
            gun.transform.position = new Vector3(-1000f, -1000f, -1000f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //カメラのシャッターを押す
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("J_Hit!!!!!!!!");
            if (jumpObj.jumpFlag)
            {

                //使用回数を+5する
                if (Input.GetButtonDown("Fire3"))
                {
                    Skill.quantity[(int)SkillType.HIGH_JUMP] += 5;
                    Debug.Log("ジャンプ+5");
                }
            }
        }
        if (other.gameObject.tag == "Enemy")
        {
            if (sword.animState == SkeletonAnim.AnimState.Attack)
            {
                if (Input.GetButtonDown("Fire3"))
                {
                    Skill.quantity[(int)SkillType.SLASH] += 5;
                    Debug.Log("スラッシュ+5");
                }
            }
            Debug.Log("S_Hit!!!!!!!!");
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;    //プレイヤーの位置を取得
        
            //カメラ向き
            if (Input.GetAxis("Horizontal") > 0)
            {
                gun.transform.position = new Vector3(pos.x + 3.6f, pos.y, pos.z);

            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                gun.transform.position = new Vector3(pos.x - 3.6f, pos.y, pos.z);

            }

        if (type == 1)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                gun.transform.position = new Vector3(pos.x, pos.y, pos.z);
            }

        }


    }


}
