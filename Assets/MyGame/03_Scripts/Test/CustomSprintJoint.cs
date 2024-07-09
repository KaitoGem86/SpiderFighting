using UnityEngine;

public class CustomSpringJoint : MonoBehaviour
{
    public Transform connectedBody;   // The Rigidbody to which the spring is connected
    public float springForce = 50.0f; // The spring constant (k)
    public float damperForce = 5.0f;  // The damping coefficient (c)
    public float restLength = 1.0f;   // The rest length of the spring
    public float speed = 10.0f;       // Speed for manual control

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (connectedBody == null)
        {
            Debug.LogError("No connected body assigned for the custom spring joint.");
        }
    }

    void FixedUpdate()
    {
        if (connectedBody == null) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }

        // Manual control with WASD keys
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.velocity += input * speed * Time.fixedDeltaTime;

        // Calculate the vector from this object to the connected object
        Vector3 displacement = rb.position - connectedBody.position;
        float currentLength = displacement.magnitude;

        // Calculate the direction of the force
        Vector3 direction = displacement.normalized;

        // Calculate the spring force magnitude
        float springMagnitude = springForce * (currentLength - restLength);

        // Calculate the damping force magnitude
        Vector3 relativeVelocity = rb.velocity;
        float dampingMagnitude = damperForce * Vector3.Dot(relativeVelocity, direction);

        // Calculate the total force
        Vector3 force = (springMagnitude + dampingMagnitude) * direction;

        // Apply the forces to both rigidbodies
        rb.AddForce(-force);
        //connectedBody.AddForce(force);
    }
}
