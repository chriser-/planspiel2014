using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stopuhr : MonoBehaviour {

	public float ZeitAsNumber { get; private set; }
	public bool Pause { get; set; }
	private Text textScript;
	public string ZeitAsString { get; private set;}

	// Use this for initialization
	void Start () {
		ZeitAsNumber = 0;
		Pause = false;
		textScript = this.GetComponent<Text>();
		ZeitAsString = "00:00";

	}
	
	// Update is called once per frame
	void Update () {

		if(!Pause){

			ZeitAsNumber += Time.deltaTime;
			int seconds = (int) ZeitAsNumber % 60;
			int minutes = (int) ZeitAsNumber / 60;

			ZeitAsString = string.Format("{0:00}:{1:00}", minutes, seconds);
			textScript.text = ZeitAsString;
			

		}
	
	}

	public void ResetStopuhr()
	{

		Start ();
		textScript.text = ZeitAsString;

	}
}
