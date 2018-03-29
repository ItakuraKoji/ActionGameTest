using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zakoScript : MonoBehaviour
{
    bool hitFlag = false;
    public GameObject dethEffect;
    public GameObject damageEffect;
    public int HP;
    public Color color;
    // Use this for initialization
    void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().material.color = this.color;
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
            --this.HP;
            this.hitFlag = false;
            Instantiate(this.damageEffect, this.transform.position + new Vector3(0.0f, 0.0f, -4.0f), this.transform.rotation);
        }
        if (HP <= 0)
        {
            Instantiate(this.dethEffect, this.transform.position, this.transform.rotation);
            Debug.Log("雑魚の叫び: うあ～");
            Destroy(this.gameObject);
        }
    }
}
