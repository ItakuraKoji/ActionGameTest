using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraGun : MonoBehaviour
{

    public Jump jumpObj;
    public SkeletonAnim sword;
    public GameObject gun;
    public GameObject player;
    // Use this for initialization
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = player.transform.position;
        //カメラのシャッターを押す

        if (jumpObj.jumpFlag)
        {

            //ハイジャンプ使用回数を+5する
            if (Input.GetButtonDown("Fire3"))
            {
                Skill.quantity[(int)SkillType.HIGH_JUMP] += 5;
                Debug.Log("ジャンプ+5");
            }
        }
        if (sword.animState == SkeletonAnim.AnimState.Attack)
        {
            if (Input.GetButtonDown("Fire3"))
            {
                Skill.quantity[(int)SkillType.SLASH] += 5;
                Debug.Log("スラッシュ+5");
            }
        }
        if (Input.GetAxis("Horizontal") > 0 )
        {
            gun.transform.position = new Vector3( pos.x + 3.6f, pos.y, pos.z);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            gun.transform.position = new Vector3(pos.x - 3.6f, pos.y, pos.z);
        }
    }

    //敵がジャンプしているか
    bool EnemyJump()
    {
        return jumpObj.jumpFlag;
    }
}
