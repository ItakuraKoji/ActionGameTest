using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMove : MonoBehaviour {

    //--------------------------------
    //定数

    //--------------------------------
    //変数
    public float speed;
    Vector3 moveVec;
    SkeletonAnim skeltonAnim;

    //--------------------------------
    //外部変数
    public Transform target;    //追いかける対象
    public float rotMax;        //回転速度
    public float runSpeed;      //速度

	// Use this for initialization
	void Start () {
        moveVec = Vector3.zero;
        skeltonAnim = GetComponent<SkeletonAnim>();
	}
	
	// Update is called once per frame
	void Update () {
        AnimMove();
	}

    //---------------------------------
    //アニメーション時のそれぞれの動き
    //---------------------------------
    void AnimMove()
    {
        switch(skeltonAnim.animState)
        {
            case SkeletonAnim.AnimState.Idle:
                moveVec = new Vector3(0, 0, 0);
                break;
            case SkeletonAnim.AnimState.Walk:
                moveVec = new Vector3(speed, 0, 0);
                break;
            case SkeletonAnim.AnimState.Run:
                //視界に入っている間、走る
                moveVec = new Vector3(speed * 2, 0, 0);
                // NaviChase();
                break;
            case SkeletonAnim.AnimState.Attack:
                moveVec = new Vector3(0, 0, 0);
                //アタックエリアに入っている間、攻撃モーション

                break;
            case SkeletonAnim.AnimState.Non:
                break;
        }
        transform.position += moveVec;
    }


    //--------------------------------
    //追尾処理
    //--------------------------------
    void    Chase()
    {
        float vx = target.position.x - this.transform.position.x;
        float vy = target.position.z - this.transform.position.z;
        float dx, dz;
        float radian;

        //ターゲットとの角度
        radian = Mathf.Atan2(vy, vx);
        dx = Mathf.Cos(radian) * 0.01f;
        dz = Mathf.Sin(radian) * 0.01f;

        //移動制御
        if(Mathf.Abs(vx) < 0.1f)
        {
            dx = 0.0f;
        }
        if(Mathf.Abs(vy) < 0.1f)
        {
            dz = 0.0f;
        }

        //移動を反映
        target.position += new Vector3(dx, 0, dz);
        this.gameObject.transform.position = target.position;
    }

    void NaviChase()
    {
        
    }
}
