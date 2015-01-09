using UnityEngine;
using System.Collections;

public class CollectableObject : MonoBehaviour {
    private GameObject player;
    private CharacterMotor motor;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Player") return;
        player = collider.gameObject;
        motor = player.GetComponent<CharacterMotor>();
        StartCoroutine(this.gameObject.name);
        if (this.gameObject.renderer != null)
            this.gameObject.renderer.enabled = false;
        this.gameObject.collider.enabled = false;
        //Destroy(this.gameObject);
    }

    IEnumerator Banane()
    {
        motor.movement.canClimb = true;
        yield return new WaitForSeconds(10);

        motor.movement.canClimb = false;
        this.gameObject.renderer.enabled = true;
        this.gameObject.collider.enabled = true;
        
        //coll.gameObject.GetComponent<CharacterMotor>().jumping.enabled = false;
    }

    IEnumerator ColaDose() {
       motor.movement.maxForwardSpeed = 20.0f;
        yield return new WaitForSeconds(10);

       motor.movement.maxForwardSpeed = 10.0f;
        this.gameObject.renderer.enabled = true;
        this.gameObject.collider.enabled = true;
    }

    IEnumerator Cheeseburger()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.renderer.enabled = false;
        }
       motor.movement.gravity = 51.0f;
        yield return new WaitForSeconds(10);

       motor.movement.gravity = 50.0f;
        this.gameObject.collider.enabled = true;
        foreach (Transform child in this.transform)
        {
            child.gameObject.renderer.enabled = true;
        }
        
    }

}
