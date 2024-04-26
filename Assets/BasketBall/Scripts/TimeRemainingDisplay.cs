using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeRemainingDisplay : MonoBehaviour {
    Text text;
    LevelManager levelManager;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
        levelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Time Remaining: " + Convert.ToInt32(levelManager.timeTillNextLevel);
	}
}
