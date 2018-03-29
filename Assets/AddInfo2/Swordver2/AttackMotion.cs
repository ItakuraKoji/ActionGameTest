using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMotion : MonoBehaviour {

    private Animator anim;
    public float timeCnt = 3.0f;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.Play("SwordAttack");
        Destroy(this.gameObject,timeCnt);
    }
}
