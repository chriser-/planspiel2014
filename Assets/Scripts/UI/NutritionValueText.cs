using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NutritionValueText : MonoBehaviour {

    Text t;

	void Start () {
        t = GetComponent<Text>();
        //Nutrition.OnNutritionChange += UpdateText;
	}

    void Update()
    {
        if(HealthController.changed)
            t.text = HealthController.currentNutrition.ToGUIText();
    }
}
