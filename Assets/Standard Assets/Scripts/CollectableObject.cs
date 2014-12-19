using UnityEngine;
using System.Collections;

public class CollectableObject : MonoBehaviour {
    private Collider coll;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        coll = collider;
        Debug.Log("triggerenter: " + this.gameObject.name);
        StartCoroutine(this.gameObject.name);
        if (this.gameObject.renderer != null)
            this.gameObject.renderer.enabled = false;
        this.gameObject.collider.enabled = false;
        //Destroy(this.gameObject);
    }

    IEnumerator Banane()
    {
        Debug.Log("Bananenfunktion");
        coll.gameObject.GetComponent<CharacterMotor>().movement.canClimb = true;
        yield return new WaitForSeconds(10);

        coll.gameObject.GetComponent<CharacterMotor>().movement.canClimb = false;
        this.gameObject.renderer.enabled = true;
        this.gameObject.collider.enabled = true;
        
        //coll.gameObject.GetComponent<CharacterMotor>().jumping.enabled = false;
    }

    IEnumerator ColaDose() {
        Debug.Log("Colafunktion");
        coll.gameObject.GetComponent<CharacterMotor>().movement.maxForwardSpeed = 20.0f;
        yield return new WaitForSeconds(10);

        coll.gameObject.GetComponent<CharacterMotor>().movement.maxForwardSpeed = 10.0f;
        this.gameObject.renderer.enabled = true;
        this.gameObject.collider.enabled = true;
    }

    IEnumerator Cheeseburger()
    {
        Debug.Log("Cheeseburgerfuntkion");
        foreach (Transform child in this.transform)
        {
            child.gameObject.renderer.enabled = false;
        }
        coll.gameObject.GetComponent<CharacterMotor>().movement.gravity = 51.0f;
        yield return new WaitForSeconds(10);

        coll.gameObject.GetComponent<CharacterMotor>().movement.gravity = 50.0f;
        this.gameObject.collider.enabled = true;
        foreach (Transform child in this.transform)
        {
            child.gameObject.renderer.enabled = true;
        }
        
    }

}
