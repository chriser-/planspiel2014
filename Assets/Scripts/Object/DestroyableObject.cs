using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyableObject : MonoBehaviour
{
    private bool destroyed = false;

    void Start()
    {

    }

    void Reset()
    {

    }

    public void Destroy(Vector3 position)
    {
        if (!destroyed)
        {
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.rigidbody.constraints = RigidbodyConstraints.None;
                child.gameObject.rigidbody.AddExplosionForce(20, position, 0, -1, ForceMode.VelocityChange);
            }
            destroyed = true;
            StartCoroutine(MakeTransparent());
        }
    }

    IEnumerator MakeTransparent()
    {
        yield return new WaitForSeconds(2f);
        foreach (Transform child in gameObject.transform) {
            child.gameObject.renderer.material.shader = Shader.Find("Transparent/Diffuse");
            Color c = child.gameObject.renderer.material.color;
            child.gameObject.renderer.material.color = new Color(c.r, c.g, c.b, 1f);
        }
        for (int i = 0; i < 20; i++)
        {
            foreach (Transform child in gameObject.transform)
            {
                Color c = child.gameObject.renderer.material.color;
                child.gameObject.renderer.material.color = new Color(c.r, c.b, c.g, c.a - 0.05f);
            }
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }

}

