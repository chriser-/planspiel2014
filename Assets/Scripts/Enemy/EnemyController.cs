using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    private bool jumpedOn = false;
    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
        EventController.OnReset += Reset;
    }

    void Update()
    {

    }

    void Reset()
    {
        jumpedOn = false;
        transform.localScale = initialScale;
        gameObject.rigidbody.WakeUp();
    }

    public void JumpedOn()
    {
        if (!jumpedOn)
        {
            jumpedOn = true;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);
        }
    }
}
