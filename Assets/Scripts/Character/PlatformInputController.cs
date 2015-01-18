using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Require a character controller to be attached to the same game object
[RequireComponent(typeof(CharacterMotor))]
[AddComponentMenu("Character/Platform Input Controller")]


// This makes the character turn to face the current movement speed per default.
public class PlatformInputController : MonoBehaviour
{
    public bool autoRotate = true;
    public float maxRotationSpeed = 360.0f;
    public Transform spawnPoint;

    private CharacterMotor motor;
    private Animator anim;
    private Transform lowestPoint;
    private PowerUpController powerUps;

    public delegate void ResetLevel();
    public static event ResetLevel OnReset;

    // Use this for initialization
    void Awake()
    {
        motor = GetComponent<CharacterMotor>();
        anim = GetComponent<Animator>();
        powerUps = GetComponent<PowerUpController>();
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        lowestPoint = GameObject.Find("LowestPoint").transform;
        transform.position = spawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the input vector from keyboard or analog stick
        Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), Mathf.Abs(Input.GetAxis("Vertical")), 0);
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        if (powerUps.HasPowerUp(PowerUp.Rush))
            motor.movement.maxForwardSpeed = 20;
        else
            motor.movement.maxForwardSpeed = 10; //TODO: use value from inspector instead of hardcoded

        motor.movement.canClimb = powerUps.HasPowerUp(PowerUp.Climb);


        if (directionVector != Vector3.zero)
        {
            // Get the length of the directon vector and then normalize it
            // Dividing by the length is cheaper than normalizing when we already have the length anyway
            var directionLength = directionVector.magnitude;
            directionVector = directionVector / directionLength;

            // Make sure the length is no bigger than 1
            directionLength = Mathf.Min(1, directionLength);

            // Make the input vector more sensitive towards the extremes and less sensitive in the middle
            // This makes it easier to control slow speeds when using analog sticks
            directionLength = directionLength * directionLength;

            // Multiply the normalized direction vector by the modified length
            directionVector = directionVector * directionLength;
        }

        // Rotate the input vector into camera space so up is camera's up and right is camera's right
        directionVector = Camera.main.transform.rotation * directionVector;

        // Rotate input vector to be perpendicular to character's up vector
        Quaternion camToCharacterSpace = Quaternion.FromToRotation(-Camera.main.transform.forward, transform.up);
        directionVector = (camToCharacterSpace * directionVector);

        // Apply the direction to the CharacterMotor
        motor.inputMoveDirection = inputVector;
        motor.inputJump = Input.GetButton("Jump");

        Vector3 newForward = ConstantSlerp(transform.forward, directionVector, maxRotationSpeed * Time.deltaTime);
        newForward = ProjectOntoPlane(newForward, transform.up);
        transform.rotation = Quaternion.LookRotation(newForward, transform.up);

        // Reset to SpawnPoint if too low.
        if (motor.transform.position.y < lowestPoint.position.y)
        {
			// Notify GUI that Player failed
			GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
			GUIHandler guiHandlerScript = canvas.GetComponent<GUIHandler>();
			guiHandlerScript.YouDie();

			//No longer needed Level will be reloaded
            //Respawn();

        }
        // Set Animation Variables
        SetAnimationVars();
    }

    Vector3 ProjectOntoPlane(Vector3 v, Vector3 normal)
    {
        return v - Vector3.Project(v, normal);
    }

    Vector3 ConstantSlerp(Vector3 from, Vector3 to, float angle)
    {
        float value = Mathf.Min(1, angle / Vector3.Angle(from, to));
        return Vector3.Slerp(from, to, value);
    }

    void SetAnimationVars()
    {
        anim.SetBool("Grounded", motor.IsGrounded());
        anim.SetFloat("VSpeed", Mathf.Abs(motor.movement.velocity.x));
        anim.SetFloat("HSpeed", motor.movement.velocity.y);
        anim.SetBool("Climbing", motor.movement.isClimbing);
        anim.SetBool("Jumping", motor.IsJumping());
        if (motor.movement.isClimbing)
            anim.speed = Mathf.Abs(motor.movement.velocity.y) / 2f;
        else
            anim.speed = 1;
    }

    public void Respawn()
    {
        motor.transform.position = spawnPoint.position;
        motor.SetVelocity(Vector3.zero);
        OnReset();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //pass through branches
        if (hit.gameObject.tag == "Falltrough" && hit.normal.y > 0 && Input.GetAxis("Vertical") < 0)
            hit.gameObject.collider.isTrigger = true;

        /*
        Rigidbody body = hit.collider.attachedRigidbody;
		// no rigidbody
        if (body == null || body.isKinematic)
            return;
			
		// We dont want to push objects below us
		//if (hit.moveDirection.y < -0.3) 
		//	return;
		
		// Calculate push direction from move direction, 
		// we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, hit.moveDirection.y, hit.moveDirection.z);
		// If you know how fast your character is trying to move,
		// then you can also multiply the push velocity by that.
		
		// Apply the push
		body.velocity = pushDir * 2;
        */
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Falltrough":
                other.gameObject.collider.isTrigger = false;
                break;
            case "Destroyable":
                if (powerUps.HasPowerUp(PowerUp.Heavy)) other.gameObject.GetComponent<DestroyableObject>().Destroy(transform.position);
                break;
            case "Enemy":
                other.gameObject.GetComponent<EnemyController>().JumpedOn();
                break;
        }
    }
    void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Climb":
                    motor.movement.isClimbing = motor.movement.canClimb && (motor.movement.isClimbing || motor.inputMoveDirection.y != 0 || !motor.grounded);
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Climb":
                motor.movement.isClimbing = false;
                break;
        }
    }
}