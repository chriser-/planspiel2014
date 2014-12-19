using UnityEngine;
using System.Collections;

public class Zerstoerbar : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("triggerenter: " + this.gameObject.name);
        
        if (collider.gameObject.GetComponent<CharacterMotor>().movement.gravity > 50.0f)
        {
                Destroy(this.gameObject);
        }
        

        //Destroy(this.gameObject);
    }

}

