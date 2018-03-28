using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour {
    int timeCnt;

	// Use this for initialization
	void Start () {
        this.timeCnt = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(this.timeCnt == 60)
        {
            Destroy(this.gameObject);
        }
        ++this.timeCnt;
	}
}
