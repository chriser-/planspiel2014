using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public Vector3 startPoint;
    public Vector3 endPoint;
    public float platformSpeed;

    private Vector3 destination;
    private Vector3 direction;
    private Transform platform;

	// Use this for initialization
	void Start () {
        platform = gameObject.transform;
        SetDestination(startPoint);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(direction * platformSpeed * Time.fixedDeltaTime, Space.World);

        if (Vector3.Distance(platform.position, destination) < platformSpeed * Time.fixedDeltaTime)
        {
            SetDestination(destination == startPoint ? endPoint : startPoint);
        }
	}

    void SetDestination(Vector3 dest)
    {
        destination = dest;
        direction = (dest - platform.position).normalized;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(startPoint, gameObject.transform.localScale);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(endPoint, gameObject.transform.localScale);
    }
}
