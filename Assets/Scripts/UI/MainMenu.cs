using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	private GameObject HighscorePanel;

	// Use this for initialization
	void Start () {

		HighscorePanel = transform.FindChild("HighscorePanel").gameObject;

	}

	public void ResetHighscoreData()
	{
		PlayerPrefs.DeleteAll();
		Debug.Log ("HighscoreData deleted");
	}

	public void LoadFirstLevel()
	{
		Application.LoadLevel(1); // 0 is MainMenu
	}
	


	public void ShowHighscore()
	{

		HighscorePanel.SetActive(true);

		
		string placeholder = "-";
		
		// Show Highscore top 4 entries
		
		for(int i = 1; i < 5; i++)
		{
			RectTransform text1 = (RectTransform)HighscorePanel.transform.FindChild(i.ToString() + "Text");
			Text text1Script =  text1.GetComponent<Text>();
			if(PlayerPrefs.HasKey("Highscore" + i + "Name"))
			{
				placeholder = PlayerPrefs.GetString("Highscore" + i + "Name");
			}
			else
			{
				placeholder = "-";
			}
			text1Script.text = placeholder;
			
			RectTransform value1 = (RectTransform)HighscorePanel.transform.FindChild(i.ToString() + "Value");
			Text value1Script =  value1.GetComponent<Text>();
			if(PlayerPrefs.HasKey("Highscore" + i + "Value"))
			{
				placeholder = PlayerPrefs.GetInt("Highscore" + i + "Value").ToString();
			}
			else
			{
				placeholder = "0";
			}
			value1Script.text = placeholder;
			
		}
		
		
	}
}
