using UnityEngine;
using System.Collections;

public class DestroyableObject : MonoBehaviour
{
    private bool destroyed = false;

    void Destroy(Vector3 position)
    {
        if (!destroyed)
        {
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.rigidbody.constraints = RigidbodyConstraints.None;
                child.gameObject.rigidbody.AddExplosionForce(20, position, 0, -1, ForceMode.VelocityChange);
            }
            destroyed = true;
        }
    }

}

