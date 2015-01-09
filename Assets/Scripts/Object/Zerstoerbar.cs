using UnityEngine;
using System.Collections;

public class Zerstoerbar : MonoBehaviour
{
    private bool destroyed = false;
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
        if (collider.gameObject.tag == "Player" && !destroyed)
        {
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.rigidbody.constraints = RigidbodyConstraints.None;
                child.gameObject.rigidbody.AddExplosionForce(20, collider.gameObject.transform.position, 0, -1, ForceMode.VelocityChange);
            }
            destroyed = true;
        }
    }

}

