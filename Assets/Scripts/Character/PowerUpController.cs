using UnityEngine;
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

public class pTime
{
    public float time;
    public float maxTime;
};

public class PowerUpController : MonoBehaviour {
    public static Dictionary<PowerUp, pTime> powerUps = new Dictionary<PowerUp, pTime>();
    
	void Start () {
        PlatformInputController.OnReset += Reset;
        powerUps = new Dictionary<PowerUp, pTime>();
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

	}

    void Reset()
    {

        powerUps.Clear();
    }

    public IEnumerator StartPowerUp(PowerUpConfig c)
    {
        //Debug.Log("PowerUp: " + c.powerUp.ToString("G") + ", Time: " + c.time.ToString());
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
