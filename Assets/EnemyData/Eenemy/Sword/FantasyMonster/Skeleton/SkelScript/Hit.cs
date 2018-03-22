using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hit : MonoBehaviour {
    //-----------------------------------
    //外部変数
    public GameObject sklObj;  
    SkeletonAnim skelton;

	// Use this for initialization
	void Start () {
        skelton = sklObj.GetComponent<SkeletonAnim>();
	}

    public void OnTriggerEnter(Collider other)
    {
        if (skelton.animState == SkeletonAnim.AnimState.Attack)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("切る攻撃に成功しました");
                //ダメージを与える
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
