using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

    public static Nutrition currentNutrition = new Nutrition();
    public static bool changed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (changed)
        {
            changed = false;
            Debug.Log(currentNutrition);
        }
	}
}
