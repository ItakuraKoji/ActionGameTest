using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Reroad : MonoBehaviour {
    Scene scene;
	// Use this for initialization
	void Start () {
		scene = SceneManager.GetActiveScene();
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("r") && Input.GetKeyDown("e"))
        SceneManager.LoadScene(scene.name);
    }
}
