using System.Collections;
using UnityEngine;

public class MissileHoming : MonoBehaviour
{
    public Transform target;
    public float speed = 20f;
    public float turnSpeed = 5f;
    public AudioClip explosionClip;

    private AudioSource audioSource;
    private bool hasHit = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (target == null || hasHit) return;

        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;

        if (other.CompareTag("Player"))
        {
            hasHit = true;

            if (FlightExamManager.instance != null)
            {
                FlightExamManager.instance.playerWasHit = true;
                FlightExamManager.instance.threatCleared = false;
            }

            Debug.Log("your plane got shot mission failed");

            if (audioSource != null && explosionClip != null)
            {
                audioSource.PlayOneShot(explosionClip);
            }

            StartCoroutine(RespawnPlayerAfterDelay(other));

            Destroy(gameObject, 2f);
        }
    }

    private IEnumerator RespawnPlayerAfterDelay(Collider other)
    {
        yield return new WaitForSeconds(0.8f);

        other.transform.position = new Vector3(500, 15, 100);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}