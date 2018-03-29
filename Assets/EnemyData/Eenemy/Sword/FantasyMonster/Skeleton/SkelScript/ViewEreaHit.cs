using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewEreaHit : MonoBehaviour {

    //------------------------------------
    //変数
    bool hitFlag;
    bool exitFlag;

	// Use this for initialization
	void Start () {
        hitFlag = false;
        exitFlag = true;
	}

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        { 
            hitFlag = true;
            exitFlag = false;
        }
        else
        {
            //hitFlag = false;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            exitFlag = true;
        }
        hitFlag = false;
    }

    // Update is called once per frame
    void Update () {
		
	}


    //-------------------------------
    //視界内にいるかを返す
    //-------------------------------
    public bool    ViewErea()
    {
        return hitFlag;
    }

    public bool     ExitViewErea()
    {
        return exitFlag;
    }
}
