using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {
    public AudioClip sound;
    private AudioSource source;
    // Use this for initialization
    void Start () {
        source = gameObject.GetComponent<AudioSource>();

        source.Play();

    }
	
	// Update is called once per frame
	void Update () {
     
    }
}
