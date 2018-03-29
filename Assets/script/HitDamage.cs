using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDamage : MonoBehaviour {

    public int giveDamage = 1;      //与えるダメージ

	// Use this for initialization
	void Start () {
	}

    //衝突したときにダメージを与える
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerControler playerCon;
            playerCon = other.gameObject.GetComponent<PlayerControler>();
            playerCon.DecrementLife(giveDamage);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
