using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour {

    //----------------------------------
    //変数
    bool hitFlag;
    bool exitFlag;
	// Use this for initialization
	void Start () {
        hitFlag = false;
        exitFlag = true;
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            hitFlag = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            hitFlag = true;
            exitFlag = false;
        }
        else
        {
            exitFlag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            hitFlag = false;
            exitFlag = true;
        }
        //hitFlag = false;
    }

    // Update is called once per frame
    void Update () {
	}

    //-------------------------------
    //ヒットしているかの状態を返す
    //-------------------------------
    public bool    HitToPlayer()
    {
        return hitFlag;
    }

    public bool     ExitErea()
    {
        return exitFlag;
    }
}
