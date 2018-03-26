using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zakoScript : MonoBehaviour
{
    float timeCnt = 0.0f;
    bool hitFlag = false;
    public ParticleSystem dethEffect;
    public float dethTime = 90.0f;
    // Use this for initialization
    void Start()
    {
        dethEffect.transform.position = this.gameObject.transform.position;
        dethEffect.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "weaponSword")
        {
            hitFlag = true;
         
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (hitFlag)
        {
            timeCnt++;
        }
        else
        {
            timeCnt = 0;
        }
        if(timeCnt >= 70)
        {
            dethEffect.Play();
        }
        if (timeCnt >= dethTime)
        {
            Debug.Log("雑魚の叫び: うあ～");
            Destroy(this.gameObject);
            ;
        }
    }
}
