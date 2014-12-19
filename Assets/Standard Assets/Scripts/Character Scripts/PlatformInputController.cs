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

    private CharacterMotor motor;
    private Animator anim;
    private Transform spawnPoint, lowestPoint;

    // Use this for initialization
    void Awake()
    {
        motor = GetComponent<CharacterMotor>();
        anim = GetComponent<Animator>();
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        lowestPoint = GameObject.Find("LowestPoint").transform;
        transform.position = spawnPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the input vector from kayboard or analog stick
        Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        Vector3 upDownVector = new Vector3(0, Mathf.Max(0,Input.GetAxis("Vertical")), 0);
        if (upDownVector.sqrMagnitude > 0.01)
            directionVector = upDownVector;

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
        motor.inputMoveDirection = directionVector;
        motor.inputJump = Input.GetButton("Jump");

        Vector3 newForward = ConstantSlerp(transform.forward, directionVector, maxRotationSpeed * Time.deltaTime);
        newForward = ProjectOntoPlane(newForward, transform.up);
        transform.rotation = Quaternion.LookRotation(newForward, transform.up);

        // Reset to SpawnPoint if too low.
        if (motor.transform.position.y < lowestPoint.position.y)
        {
            motor.transform.position = spawnPoint.position;
            motor.SetVelocity(Vector3.zero);
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
        anim.SetFloat("Speed", Mathf.Abs(motor.movement.velocity.x));
        anim.SetFloat("Jump", motor.movement.velocity.y);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //pass through branches
        if (hit.gameObject.tag == "Falltrough" && hit.normal == Vector3.up && Input.GetAxis("Vertical") < 0)
            hit.gameObject.collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Falltrough":
                other.gameObject.collider.isTrigger = false;
                break;
        }
    }
    void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Climb":
                if (motor.inputMoveDirection.z > 0 && motor.movement.canClimb)
                    motor.movement.isClimbing = true;
                else
                    motor.movement.isClimbing = false;
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