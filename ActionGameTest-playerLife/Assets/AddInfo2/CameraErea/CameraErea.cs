using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraErea : MonoBehaviour {

    bool hitFlag;
	// Use this for initialization
	void Start () {
        hitFlag = false;
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            hitFlag = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            hitFlag = false;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
   

    //カメラの撮影範囲にいるかを返す
    public bool    HitErea()
    {
        return hitFlag;
    }
}
