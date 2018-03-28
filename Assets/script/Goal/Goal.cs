using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Goal : MonoBehaviour {
    public GameObject goal;
    public GUIStyle goalText;
    public bool isGoal;
	// Use this for initialization
	void Start () {
        goalText = new GUIStyle();
        goalText.fontSize = 100;
        isGoal = false;
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isGoal = true;
            Debug.Log("Goal!!!!!!");
        }
    }
    private void OnGUI()
    {
        if (isGoal)
        {
            
            goalText.normal.textColor = new Color(255f, 0, 255f);
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "続きは製品版で", goalText);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
