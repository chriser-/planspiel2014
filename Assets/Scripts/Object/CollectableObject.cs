﻿using UnityEngine;
using System.Collections;

public class CollectableObject : MonoBehaviour {
    private GameObject player;
    private CharacterMotor motor;

    public PowerUpConfig[] powerUps;
    public Nutrition nutritionValue;

    public AudioClip nom;

    void Start()
    {
        PlatformInputController.OnReset += Reset;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Player") return;

        
        if (powerUps.Length > 0)
        {
            
            float maxTime = 0;
            //start specific powerups
            collider.gameObject.audio.PlayOneShot(nom,1.5F);
            foreach (PowerUpConfig p in powerUps)
            {
                collider.gameObject.GetComponent<PowerUpController>().StartCoroutine("StartPowerUp", p);
                
                if (p.time > maxTime) maxTime = p.time;
            }
            //add nutrition value to controller
            HealthController.addNutrition(nutritionValue);
            //spawn again after the longest powerUp is over
            Invoke("Respawn", maxTime);
        }
        gameObject.SetActive(false);
    }

    void Reset()
    {
        CancelInvoke("Respawn");
        gameObject.SetActive(true);
    }

    void Respawn()
    {
        gameObject.SetActive(true);
    }

}
