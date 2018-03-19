using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {
    CharacterController controler;
    float speed;
    float glavity;
    float JumpPower;
    float vertVelosity;
    float minVertVelosity;
    public PlayerFoot foot;
    SkillFunctions skill = new SkillFunctions();

    // Use this for initialization
    void Start () {
        this.controler = this.gameObject.GetComponent<CharacterController>();
        this.speed = 0.8f;
        this.glavity = 0.05f;
        this.JumpPower = 1.0f;
        this.vertVelosity = 0.0f;
        this.minVertVelosity = -1.0f;
        skill.HighJump(ref this.JumpPower);
  
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 vector = new Vector3(0.0f, 0.0f, 0.0f);

        float axis = Input.GetAxis("Horizontal");
        vector += new Vector3(axis * speed, 0.0f, 0.0f);
       
        if (Input.GetButtonDown("Fire1") && this.foot.stayGround)
        {
            this.vertVelosity = this.JumpPower;
        }
        if (this.foot.stayGround && this.vertVelosity < -0.1f)
        {
            this.vertVelosity = -0.1f;
        }
        vector += new Vector3(0.0f, this.vertVelosity, 0.0f);
        this.vertVelosity -= this.glavity;
        if(this.vertVelosity < this.minVertVelosity)
        {
            this.vertVelosity = this.minVertVelosity;
        }

        this.controler.Move(vector);
    }
}
