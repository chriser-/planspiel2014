using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlatformInputController>().spawnPoint = transform;
            HealthController.lastNutrition = HealthController.currentNutrition;
        }
    }
}
