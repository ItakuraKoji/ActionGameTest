using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoot : MonoBehaviour {
    public bool stayGround;

	// Use this for initialization
	void Start () {
        stayGround = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Ground")
        {
            stayGround = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        stayGround = false;
    }
}
