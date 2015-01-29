using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;


public class StopWatch : MonoBehaviour {

	public float TimeAsNumber { get; private set; }
	public bool Pause { get; set; }
	private Text textScript;
	public string TimeAsString { get; private set;}

	// Use this for initialization
	void Start () {
		TimeAsNumber = 0;
		Pause = false;
		textScript = this.GetComponent<Text>();
		TimeAsString = "00:00";

	}
	
	// Update is called once per frame
	void Update () {

		if(!Pause){

			TimeAsNumber += Time.deltaTime;
            TimeSpan t = TimeSpan.FromSeconds(TimeAsNumber);

			TimeAsString = string.Format("{0:00}:{1:00}.{2:000}", t.Minutes, t.Seconds, t.Milliseconds);
			textScript.text = TimeAsString;
			

		}
	
	}

	public void ResetStopuhr()
	{

		Start ();
		textScript.text = TimeAsString;

	}
}
