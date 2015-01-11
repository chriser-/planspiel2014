using UnityEngine;
using System.Collections;

public class CollectableObject : MonoBehaviour {
    private GameObject player;
    private CharacterMotor motor;

    public PowerUpConfig[] powerUps;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Player") return;

        if (powerUps.Length > 0)
        {
            float maxTime = 0;
            //start specific powerups
            foreach (PowerUpConfig p in powerUps)
            {
                collider.gameObject.GetComponent<PowerUpController>().StartCoroutine("StartPowerUp", p);
                if (p.time > maxTime) maxTime = p.time;
            }

            //spawn again after the longest powerUp is over
            Invoke("Respawn", maxTime);
        }
        gameObject.SetActive(false);
    }

    void Respawn()
    {
        gameObject.SetActive(true);
    }

}
