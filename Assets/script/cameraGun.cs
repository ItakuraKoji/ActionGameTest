using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraGun : MonoBehaviour {

    public Jump jumpObj;
  
	// Use this for initialization
	void Start () {
        
        
	}
	
	// Update is called once per frame
	void Update () {
        //カメラのシャッターを押す
      
            if (jumpObj.jumpFlag)
            {
          
            //ハイジャンプ使用回数を+5する
            if (Input.GetButtonDown("Fire3"))
             {
                Skill.quantity[1] += 5;
                Debug.Log("わーい");
             }
            }
            else
            {
            //何も取得しない
           
        }
        
	}

    //敵がジャンプしているか
    bool    EnemyJump()
    {
        return jumpObj.jumpFlag;
    }
}
