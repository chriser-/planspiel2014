using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {


	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			// Notify GUI that Player won
			GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
			GUIHandler guiHandlerScript = canvas.GetComponent<GUIHandler>();
			guiHandlerScript.YouWin();
		}
	}
	


}
