using UnityEngine;
using System.Collections;

public class CollectableObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("triggerenter: " + this.gameObject.name);
        Destroy(this.gameObject);
    }
}
