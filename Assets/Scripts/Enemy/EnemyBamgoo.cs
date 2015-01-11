using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    [System.Serializable]
    public enum EnemyType
    {
        Idle,
        Patrolling,
        Attacking
    };

    public EnemyType Type;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
