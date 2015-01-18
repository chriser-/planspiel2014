using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIHandler : MonoBehaviour {

	[SerializeField]
	private GameObject loseScreen;
	[SerializeField]
	private GameObject winScreen;
	[SerializeField]
	private GameObject highscore; 
	[SerializeField]
	private GameObject indicator;
	[SerializeField]
	private GameObject info;

	// Für die Punktevergabe durch gebrauchte Zeit
	public float ExpectedDurationMin;
	public float ExpectedDurationMax;
	public int MinPoints;
	public int MaxPoints;
	// Wird von den Punkten abgezogen, z.B. durch FastFood
	private int malusResult;


	private int gotPoints;
	private float timeNeeded;
	private bool highscoreInserted;


	// Use this for initialization
	void Start () {

		gotPoints = 0;
		timeNeeded = 0f; 
		highscoreInserted = false;
		malusResult = 0;

	
	}

	void Update(){
		//if(Input.GetKeyDown("mouse 1"))
			//YouWin();
	}
	


	public void YouDie()
	{
		Time.timeScale = 0f;
		loseScreen.SetActive(true);


	}

	public void ShowInfo()
	{
		Time.timeScale = 0f;
		info.SetActive(true);
	}

	public void YouWin()
	{
		Time.timeScale = 0f;

		Stopuhr stopuhrScript = indicator.GetComponentInChildren<Stopuhr>();
		winScreen.SetActive(true);
		//Zeit setzten
		RectTransform zeitValue = (RectTransform) winScreen.transform.FindChild("ZeitValue");
		Text zeitValueTextScript = zeitValue.GetComponent<Text>();
		zeitValueTextScript.text = stopuhrScript.ZeitAsString;

		//Nutrition
		RectTransform nutritionValue = (RectTransform) winScreen.transform.FindChild("NutritionValues");
		Text nutritionTextScript = nutritionValue.GetComponent<Text>();

		RectTransform nutritionPanelText = (RectTransform) this.transform.FindChild("NutritionPanel").FindChild("Values");
		Text nutritionPanelTextScript = nutritionPanelText.GetComponent<Text>();

		nutritionTextScript.text = nutritionPanelTextScript.text;

		//Malus

		string mali = nutritionPanelTextScript.text;
		string[] maliSplit = mali.Split('\n');

		malusResult = 0;
		int n;

		foreach(string m in maliSplit)
		{
			//Extract numbers from String
			string resultString = System.Text.RegularExpressions.Regex.Match(m, @"\d+").Value;
			int.TryParse(resultString, out n);
			malusResult += n;
		}

		// Make Malus smaller and negativ, Malus will be applied in DistributePoints()
		malusResult /= -6; // TO DO: Balancing
		RectTransform malusValue = (RectTransform) winScreen.transform.FindChild("MalusValue");
		Text malusValueTextScript = malusValue.GetComponent<Text>();
		malusValueTextScript.text = malusResult.ToString();


		//Punkte setzten
		timeNeeded = stopuhrScript.ZeitAsNumber;
		gotPoints = DistributePoints(timeNeeded);
		RectTransform punkteValue = (RectTransform) winScreen.transform.FindChild("PunkteValue");
		Text punkteValueTextScript = punkteValue.GetComponent<Text>();
		punkteValueTextScript.text = gotPoints.ToString();

	}

	private int DistributePoints(float time)
	{
		int tempPoints = MaxPoints;
		float tempTime = ExpectedDurationMin;

		while(tempTime < time)
		{
			tempTime += (ExpectedDurationMax - ExpectedDurationMin) / 100;
			tempPoints -= (int) (MaxPoints - MinPoints) / 100;
		}

		return tempPoints + malusResult;
	}

	public void CloseAllScreens()
	{
		Time.timeScale = 1f;
		loseScreen.SetActive(false);
		winScreen.SetActive(false);
		highscore.SetActive(false);
		info.SetActive(false);
	}

	public void ShowHighscore()
	{
		Time.timeScale = 0f;
		highscore.SetActive(true);

		RectTransform punkte = (RectTransform) highscore.transform.FindChild("Punkte");
		Text punkteTextScript =  punkte.GetComponent<Text>();
		punkteTextScript.text = gotPoints.ToString() + " Punkte";

		string placeholder = "-";

		// Show Highscore top 4 entries

		for(int i = 1; i < 5; i++)
		{
			RectTransform text1 = (RectTransform) highscore.transform.FindChild(i.ToString() + "Text");
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

			RectTransform value1 = (RectTransform) highscore.transform.FindChild(i.ToString() + "Value");
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

	public void AddHighscoreEntry()
	{
		RectTransform submit = (RectTransform) highscore.transform.FindChild("InputField").FindChild("Submit");
		Text submitTextScript =  submit.GetComponent<Text>();
		string playerName = submitTextScript.text;

		if (playerName == "" || highscoreInserted)
			return;
		/*
		PlayerPrefs.SetString("Highscore1Name", playerName);
		PlayerPrefs.SetInt("Highscore1Value", gotPoints);

		CloseAllScreens();
		ShowHighscore();
		*/

		for(int i = 1; i < 5; i++)
		{
			// Find position in highscore
			if (gotPoints >= PlayerPrefs.GetInt("Highscore" + i + "Value"))
			{
				highscoreInserted = true;

				// Write to position and push everyone below us one position back
				string newName = playerName;
				int newValue = gotPoints;

				string oldName = "";
				int oldValue = -1;

				for(int j = i; j < 5; j++)
				{

					oldName = PlayerPrefs.GetString("Highscore" + j + "Name");
					oldValue = PlayerPrefs.GetInt("Highscore" + j + "Value");

					if(oldName =="")
						oldName = "-";	// For not yet used positions
					
					
					PlayerPrefs.SetString("Highscore" + j + "Name", newName);
					PlayerPrefs.SetInt ("Highscore" + j + "Value", newValue);

					newName = oldName;
					newValue = oldValue;

				}
				// when we inserted our value we no longer want to keep searching
				break;

			}

		}

		// Make changes in Highscore visible
		CloseAllScreens();
		ShowHighscore();
		
	}

	public void ResetHighscoreData()
	{
		PlayerPrefs.DeleteAll();
		Debug.Log ("HighscoreData deleted");
	}

	public void ResetGUI()
	{

		Stopuhr stopuhrScript = indicator.GetComponentInChildren<Stopuhr>();
		stopuhrScript.ResetStopuhr();

		Start ();

		CloseAllScreens();

	}

	public void ReloadLevel()
	{

		Application.LoadLevel(Application.loadedLevelName);

	}

	public void LoadNextLevel()
	{
		int nextLevelID = Application.loadedLevel + 1;
		Application.LoadLevel(nextLevelID);
	}
}
