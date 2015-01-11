using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum PowerUp
{
    SugarRush,
    Climb,
    Heavy,
};

[System.Serializable]
public struct PowerUpConfig
{
    public PowerUp powerUp;
    public float time;
};

public class PowerUpController : MonoBehaviour {

    private Dictionary<PowerUp, float> powerUps = new Dictionary<PowerUp, float>();
    private Dictionary<PowerUp, float> powerUpTimes = new Dictionary<PowerUp, float>();
    private Dictionary<PowerUp, Texture> powerUpTextures = new Dictionary<PowerUp,Texture>();
    private Texture barTexture;

	void Start () {
        barTexture = (Texture)Resources.Load("GUI/Bar", typeof(Texture));

        powerUpTextures[PowerUp.Climb] = (Texture)Resources.Load("GUI/Climb", typeof(Texture));
        powerUpTextures[PowerUp.SugarRush] = (Texture)Resources.Load("GUI/Sugarrush", typeof(Texture));
        powerUpTextures[PowerUp.Heavy] = (Texture)Resources.Load("GUI/Heavy", typeof(Texture));

        //Used by GUI to size the bar correctly
        foreach (PowerUp p in (PowerUp[])Enum.GetValues(typeof(PowerUp)))
            powerUpTimes.Add(p, 0);
	}
	
	void FixedUpdate () {

        //Add PowerUps with Keys for debugging purpose
        float pu_time = Input.GetKey(KeyCode.LeftShift) ? -10 : 10;
        if (Input.GetKeyDown(KeyCode.Alpha1)) StartCoroutine(StartPowerUp(new PowerUpConfig { powerUp = PowerUp.SugarRush, time = pu_time }));
        else if (Input.GetKeyDown(KeyCode.Alpha2)) StartCoroutine(StartPowerUp(new PowerUpConfig { powerUp = PowerUp.Climb, time = pu_time }));
        else if (Input.GetKeyDown(KeyCode.Alpha3)) StartCoroutine(StartPowerUp(new PowerUpConfig { powerUp = PowerUp.Heavy, time = pu_time }));

        //decrease the time the powerup is running
        foreach (var powerUp in powerUps.Keys.ToArray())
            powerUps[powerUp] -= Time.fixedDeltaTime;

        //remove from dict where time <= 0
        var itemsToRemove = powerUps.Where(f => f.Value <= 0).ToArray();
        foreach (var item in itemsToRemove)
            powerUps.Remove(item.Key);

	}

    public IEnumerator StartPowerUp(PowerUpConfig c)
    {
        Debug.Log("PowerUp: " + c.powerUp.ToString("G") + ", Time: " + c.time.ToString());
        if (powerUps.ContainsKey(c.powerUp))
            powerUps[c.powerUp] += c.time;
        else
            powerUps.Add(c.powerUp, c.time);
        //remember max time for GUI
        if(c.time > 0)
            powerUpTimes[c.powerUp] = powerUps[c.powerUp];
        yield return null;
    }

    public bool HasPowerUp(PowerUp powerUp)
    {
        if (powerUps.ContainsKey(powerUp) && powerUps[powerUp] > 0) return true;
        return false;
    }

    void OnGUI()
    {
        int offset = 10;
        foreach (var p in powerUps)
        {
            GUI.DrawTexture(new Rect(offset, 10, 60, 60), powerUpTextures[p.Key], ScaleMode.StretchToFill, true, 10.0F);
            GUI.DrawTexture(new Rect(offset, 70, 60 * (p.Value / powerUpTimes[p.Key]), 10), barTexture, ScaleMode.StretchToFill, true, 10.0F);
            offset += 70;
        }
    }
}
