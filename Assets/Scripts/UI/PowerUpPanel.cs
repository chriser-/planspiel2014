using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct PowerUpUiConfig
{
    public PowerUp powerUp;
    public Image activeImage;
    public Text text;
};

public class PowerUpPanel : MonoBehaviour {

    private class pUI
    {
        public Image image;
        public Text text;
    };
    private Dictionary<PowerUp, pUI> powerUpUI = new Dictionary<PowerUp, pUI>();

    public PowerUpUiConfig[] PowerUpUiConfig;

    void Start()
    {
        foreach (PowerUpUiConfig c in PowerUpUiConfig)
            powerUpUI.Add(c.powerUp, new pUI { image = c.activeImage, text = c.text });

        PlatformInputController.OnReset += Reset;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //show on UI
        foreach (var powerUp in PowerUpController.powerUps)
        {
            PowerUp p = powerUp.Key;
            float time = powerUp.Value.time;
            float maxTime = powerUp.Value.maxTime;
            if (time <= 0)
            {
                powerUpUI[p].image.fillAmount = 0;
                powerUpUI[p].text.text = "";
            }
            else
            {
                powerUpUI[p].image.fillAmount = time / maxTime;
                powerUpUI[p].text.text = string.Format("{0:0.#}", time);
            }
        }
	}

    void Reset()
    {
        foreach (var powerUp in powerUpUI)
        {
            powerUp.Value.image.fillAmount = 0;
            powerUp.Value.text.text = "";
        }
    }
}
