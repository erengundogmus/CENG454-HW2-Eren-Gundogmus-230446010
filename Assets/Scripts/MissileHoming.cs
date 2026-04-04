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

        if (other.tag == "Player")
        {
            hasHit = true;

            Debug.Log("missile hit the plane");

            if (audioSource != null && explosionClip != null)
            {
                audioSource.PlayOneShot(explosionClip);
            }

            Destroy(gameObject, 2f);
        }
    }
}