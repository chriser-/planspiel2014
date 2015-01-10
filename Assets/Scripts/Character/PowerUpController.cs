using UnityEngine;
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

    [System.NonSerialized]
    public Dictionary<PowerUp, float> powerUpTimes = new Dictionary<PowerUp,float>();

    private Dictionary<PowerUp, Texture> powerUpTextures = new Dictionary<PowerUp,Texture>();
    private Texture barTexture;

	void Start () {
        barTexture = (Texture)Resources.Load("GUI/Bar", typeof(Texture));

        powerUpTextures[PowerUp.Climb] = (Texture)Resources.Load("GUI/Climb", typeof(Texture));
        powerUpTextures[PowerUp.SugarRush] = (Texture)Resources.Load("GUI/Sugarrush", typeof(Texture));
        powerUpTextures[PowerUp.Heavy] = (Texture)Resources.Load("GUI/Heavy", typeof(Texture));
	}
	
	void FixedUpdate () {

        //decrease the time the powerup is running
        foreach (var powerUp in powerUpTimes.Keys.ToArray())
            powerUpTimes[powerUp] -= Time.fixedDeltaTime;

        //remove from dict where time <= 0
        var itemsToRemove = powerUpTimes.Where(f => f.Value <= 0).ToArray();
        foreach (var item in itemsToRemove)
            powerUpTimes.Remove(item.Key);

	}

    public IEnumerator StartPowerUp(PowerUpConfig c)
    {
        Debug.Log("PowerUp: " + c.powerUp.ToString("G") + ", Time: " + c.time.ToString());
        if (powerUpTimes.ContainsKey(c.powerUp))
            powerUpTimes[c.powerUp] += c.time;
        else
            powerUpTimes.Add(c.powerUp, c.time);
        yield return null;
    }


    /*
    public IEnumerator Banane()
    {
        motor.movement.canClimb = true;
        bananenZeit = 10.0f;
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.1f);
            bananenZeit -= 0.1f;
        }
        bananenZeit = 0.0f;
        motor.movement.canClimb = false;
        this.gameObject.renderer.enabled = true;
        this.gameObject.collider.enabled = true;

        //coll.gameObject.GetComponent<CharacterMotor>().jumping.enabled = false;
    }

    public IEnumerator ColaDose()
    {
        motor.movement.maxForwardSpeed = 20.0f;
        dosenZeit = 10.0f;
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.1f);
            dosenZeit -= 0.1f;
        }
        dosenZeit = 0.0f;

        motor.movement.maxForwardSpeed = 10.0f;
        this.gameObject.renderer.enabled = true;
        this.gameObject.collider.enabled = true;
    }

    public IEnumerator Cheeseburger()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.renderer.enabled = false;
        }
        motor.movement.gravity = 51.0f;
        burgerZeit = 10.0f;
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.1f);
            burgerZeit -= 0.1f;
        }
        burgerZeit = 0.0f;

        motor.movement.gravity = 50.0f;
        this.gameObject.collider.enabled = true;
        foreach (Transform child in this.transform)
        {
            child.gameObject.renderer.enabled = true;
        }
    }
    */

    void OnGUI()
    {
        int offset = 10;
        foreach (var p in powerUpTimes)
        {
            GUI.DrawTexture(new Rect(offset, 10, 60, 60), powerUpTextures[p.Key], ScaleMode.StretchToFill, true, 10.0F);
            GUI.DrawTexture(new Rect(offset, 70, 60 * (p.Value / 10.0f), 10), barTexture, ScaleMode.StretchToFill, true, 10.0F);
            offset += 70;
        }
    }
}
