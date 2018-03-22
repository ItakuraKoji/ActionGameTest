using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gobJump : MonoBehaviour {

    CharacterController charaCon;
    Animator anim;
    public float jumpPow = 7.0f;
    public float gravity = 20.0f;
    Vector3 moveVec = Vector3.zero;
    float timeCnt = 0;
	// Use this for initialization
	void Start () {
        charaCon = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        timeCnt = Time.deltaTime;
        if(timeCnt >= 3)
        {
            timeCnt -= timeCnt;
            Jumping();
        }
	}

    //ジャンプ処理
    void    Jumping()
    {
        if(charaCon.isGrounded)
        {
            moveVec.y = jumpPow;
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }
        moveVec.y -= gravity * Time.deltaTime;
    }
}
