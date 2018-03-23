using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zakoScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "weaponSword")
        {
            Debug.Log("雑魚の叫び: うあ～");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
