using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour {

    private Animator animator;
    public GameObject obj;
    private PlayerControler player;
    float y;
    // Use this for initialization
        void Start()
    {
        animator = GetComponent<Animator>();
        player = obj.GetComponent<PlayerControler>();
        y = 90;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (Input.GetButtonDown("Fire1") && player.isJumping)
        {
            animator.SetBool("isjump", true);
        }
      else if(!player.isJumping)
        {
            animator.SetBool("isjump", false);
        }
      if(Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
        {
            animator.SetBool("isrun", true);
        }
      else
        {
            animator.SetBool("isrun", false);
        }
        if(Input.GetAxis("Horizontal") > 0)
        {
            if(y == -90)
            {
                transform.Rotate(0, 180, 0);
                y = 90;
            }
        }
        if(Input.GetAxis("Horizontal") < 0)
        {
            if(y == 90)
            {
                transform.Rotate(0, -180, 0);
                y = -90;
            }
           
        }
    }
}
