using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Info : MonoBehaviour {

	private List<string> infos;

	// Use this for initialization
	void Awake () {
		infos = new List<string>();
		infos.Add("Wenn Sie 1.0 Stk. Cheeseburger zu sich nehmen, müssen Sie 39,5 Minuten laufen.");
		infos.Add("Wenn Sie 1.0 l Cola zu sich nehmen, müssen Sie 51,6 Minuten laufen.");
		infos.Add("Wenn Sie 1.0 Stk. Banane, frisch zu sich nehmen, müssen Sie 11,5 Minuten laufen.");
		infos.Add("Wenn Sie 100.0 g Kuchen allgemein zu sich nehmen, müssen Sie 46,4 Minuten laufen.");
		infos.Add("Wenn Sie 1.0 Stk. Chicken Nuggets zu sich nehmen, müssen Sie 4,6 Minuten laufen.");
		infos.Add("Wenn Sie 100.0 g Pommes Frites mittel zu sich nehmen, müssen Sie 36,6 Minuten laufen.");
		infos.Add("Wenn Sie 1.0 Port. Pizza zu sich nehmen, müssen Sie 80,7 Minuten laufen.");
		infos.Add("Wenn Sie 100.0 g Rahmspinat zu sich nehmen, müssen Sie 6,5 Minuten laufen.");



		ChangeInfo();
		Time.timeScale = 0f;
	}

	void OnEnable(){
		ChangeInfo();
		Time.timeScale = 0f;
	}

	public void ChangeInfo(){

		// Display random messages regarding health
		int rnd = Random.Range(0, infos.Count-1);


		RectTransform info = (RectTransform) this.transform.FindChild("Info");
		Text infoTextScript = info.GetComponent<Text>();
		infoTextScript.text = infos[rnd];

	}


	

}
