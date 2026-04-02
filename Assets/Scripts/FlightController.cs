using UnityEngine;

public class FlightController : MonoBehaviour
{
    [SerializeField] private float pitchSpeed = 45f; // degrees/second
    [SerializeField] private float yawSpeed = 45f;   // degrees/second
    [SerializeField] private float rollSpeed = 45f;  // degrees/second
    [SerializeField] private float thrustSpeed = 45f; // units/second

    // TODO (Task 3-A): Declare a private Rigidbody field named 'rb'
    private Rigidbody rb;

    void Start()
    {
        // TODO (Task 3-B): Cache GetComponent<Rigidbody>() into 'rb'.
        rb = GetComponent<Rigidbody>();
        
        // Then set rb.freezeRotation = true.
        rb.freezeRotation = true;
    }

    void Update() // Input is polled in Update()
    {
        HandleRotation();
        HandleThrust();
    }

    private void HandleRotation()
    {
        // Pitch (aşağı ve yukarı yön tuşları -> Vector3.right ekseni)
        float pitchInput= Input.GetAxis("Vertical");
        transform.Rotate(Vector3.right*pitchInput*pitchSpeed*Time.deltaTime);

        // Yaw (sağ ve sol ok tuşları -> Vector3.up ekseni)
        float yawInput= Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up*yawInput*yawSpeed*Time.deltaTime);

        // Roll (q ve e tuşları -> Vector3.forward ekseni)
        float rollInput= 0f;
        if (Input.GetKey(KeyCode.Q)) rollInput= 1f;
        else if (Input.GetKey(KeyCode.E)) rollInput= -1f;
        transform.Rotate(Vector3.forward*rollInput*rollSpeed*Time.deltaTime);
    }

    private void HandleThrust()
    {
        // Thrust (boşluk tuşu -> Vector3.forward ekseninde ilerleme)
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.forward * thrustSpeed * Time.deltaTime);
        }
    }
}