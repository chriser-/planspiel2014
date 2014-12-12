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
        this.gameObject.renderer.enabled = false;
        this.gameObject.collider.enabled = false;
        //Destroy(this.gameObject);
    }

    void Banane() {
        Debug.Log("Bananenfunktion");
        //coll.gameObject.GetComponent<CharacterMotor>().jumping.enabled = false;
    }

    IEnumerator ColaDose() {
        Debug.Log("Colafunktion");
        coll.gameObject.GetComponent<CharacterMotor>().movement.maxForwardSpeed = 20.0f;
        yield return new WaitForSeconds(10);

        coll.gameObject.GetComponent<CharacterMotor>().movement.maxForwardSpeed = 10.0f;
        Destroy(this.gameObject);
    }

}
