using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill : MonoBehaviour {
    public Animator anim;
    public GameObject attackEffect;
    int timeCnt;

	// Use this for initialization
	void Start () {
        timeCnt = 0;
        anim.SetBool("isAttack", true);
        Instantiate(this.attackEffect, this.transform);
        this.attackEffect.transform.position = new Vector3(5.0f, 2.0f, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        this.anim.gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f - (float)this.timeCnt / 30 + 0.5f);
		if(this.timeCnt == 60)
        {
            Destroy(this.gameObject);
        }
        ++this.timeCnt;
    }
}
