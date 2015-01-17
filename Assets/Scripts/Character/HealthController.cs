using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

    public static Nutrition currentNutrition = new Nutrition();
    public static bool changed = false;

	// Use this for initialization
	void Start () {
        PlatformInputController.OnReset += Reset;
	}
	
	// Update is called once per frame
	void Update () {
        if (changed)
            changed = false;
	}

    void Reset()
    {
        currentNutrition = new Nutrition();
    }

    public static void addNutrition(Nutrition n)
    {
        currentNutrition += n;
        changed = true;
    }
}
