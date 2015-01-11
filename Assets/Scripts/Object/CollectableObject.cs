using UnityEngine;
using System.Collections;

public class CollectableObject : MonoBehaviour {
    private GameObject player;
    private CharacterMotor motor;

    public PowerUpConfig[] powerUps;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Player") return;

        //start specific powerups
        foreach(PowerUpConfig p in powerUps)
            collider.gameObject.GetComponent<PowerUpController>().StartCoroutine("StartPowerUp", p);

        //spawn again after delay
        Invoke("Respawn", 10); //TODO: what to use here?

        gameObject.SetActive(false);
    }

    void Respawn()
    {
        gameObject.SetActive(true);
    }

}
