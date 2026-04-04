using UnityEngine;

public class MissileHoming : MonoBehaviour
{
    public Transform target;
    public float speed = 20f;
    public float turnSpeed = 5f;

    void Update()
    {
        if (target == null) return;

        // hedefe doğru yön hesapla
        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        // yumuşak dönüş
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);

        // ileri hareket
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}