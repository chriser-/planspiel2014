using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyableObject : MonoBehaviour
{
    public bool destroyed = false;
    GameObject copy;

    void Start()
    {
        //the copied gameobject is the gameobject we see ingame. the actual one is the dummy.
        //dont execute start when run with cloned object, or we run into a loop
        if (gameObject.name.Contains("Clone")) return;
        gameObject.SetActive(false);
        copy = (GameObject)Instantiate(gameObject);
        copy.SetActive(true);
        EventController.OnReset += Reset;
    }

    void Reset()
    {
        if (copy == null)
        {
            copy = (GameObject)Instantiate(gameObject);
            copy.SetActive(true);
        }
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
        foreach (Transform child in gameObject.transform)
        {
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

