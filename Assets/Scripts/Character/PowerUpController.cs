using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum PowerUp
{
    Climb,
    Heavy,
    Rush,
    Strong,
};

[System.Serializable]
public struct PowerUpConfig
{
    public PowerUp powerUp;
    public float time;
};

[System.Serializable]
public struct PowerUpUiConfig
{
    public PowerUp powerUp;
    public Image activeImage;
    public Text text;
};

public class PowerUpController : MonoBehaviour {

    private class pTime
    {
        public float time;
        public float maxTime;
    };
    private class pUI
    {
        public Image image;
        public Text text;
    };

    private Dictionary<PowerUp, pTime> powerUps = new Dictionary<PowerUp, pTime>();
    private Dictionary<PowerUp, pUI> powerUpUI = new Dictionary<PowerUp, pUI>();

    public PowerUpUiConfig[] PowerUpUiConfig;

	void Start () {
        foreach (PowerUpUiConfig c in PowerUpUiConfig)
            powerUpUI.Add(c.powerUp, new pUI { image = c.activeImage, text = c.text });

        EventController.OnReset += Reset;
	}
	
	void FixedUpdate () {

        //Add PowerUps with Keys for debugging purpose
        float pu_time = Input.GetKey(KeyCode.LeftShift) ? -10 : 10;
        if (Input.GetKeyDown(KeyCode.Alpha1)) StartCoroutine(StartPowerUp(new PowerUpConfig { powerUp = PowerUp.Rush, time = pu_time }));
        else if (Input.GetKeyDown(KeyCode.Alpha2)) StartCoroutine(StartPowerUp(new PowerUpConfig { powerUp = PowerUp.Climb, time = pu_time }));
        else if (Input.GetKeyDown(KeyCode.Alpha3)) StartCoroutine(StartPowerUp(new PowerUpConfig { powerUp = PowerUp.Heavy, time = pu_time }));
        else if (Input.GetKeyDown(KeyCode.Alpha4)) StartCoroutine(StartPowerUp(new PowerUpConfig { powerUp = PowerUp.Strong, time = pu_time }));

        //decrease the time the powerup is running
        foreach (var p in powerUps.Keys.ToArray())
            powerUps[p].time -= Time.fixedDeltaTime;

        //show on UI
        foreach (var powerUp in powerUps)
        {
            PowerUp p = powerUp.Key;
            float time = powerUp.Value.time;
            if (time <= 0)
            {
                powerUpUI[p].image.fillAmount = 0;
                powerUpUI[p].text.text = "";
            }
            else
            {
                powerUpUI[p].image.fillAmount = time / powerUps[p].maxTime;
                powerUpUI[p].text.text = string.Format("{0:0.#}",time);
            }
        }

        //remove from dict where time <= 0
        var itemsToRemove = powerUps.Where(f => f.Value.time <= 0).ToArray();
        foreach (var item in itemsToRemove)
            powerUps.Remove(item.Key);
	}

    void Reset()
    {
        foreach (var powerUp in powerUps)
        {
            PowerUp p = powerUp.Key;
            powerUpUI[p].image.fillAmount = 0;
            powerUpUI[p].text.text = "";
        }
        powerUps.Clear();
    }

    public IEnumerator StartPowerUp(PowerUpConfig c)
    {
        Debug.Log("PowerUp: " + c.powerUp.ToString("G") + ", Time: " + c.time.ToString());
        if (powerUps.ContainsKey(c.powerUp))
        {
            powerUps[c.powerUp].time += c.time;
            powerUps[c.powerUp].maxTime = powerUps[c.powerUp].time;
        }
        else
            powerUps.Add(c.powerUp, new pTime { time = c.time, maxTime = c.time });
        //remember max time for GUI
        yield return null;
    }

    public bool HasPowerUp(PowerUp powerUp)
    {
        if (powerUps.ContainsKey(powerUp) && powerUps[powerUp].time > 0) return true;
        return false;
    }
}
