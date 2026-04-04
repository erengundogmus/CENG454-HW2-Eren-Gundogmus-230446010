using UnityEngine;

public class DangerZoneTrigger : MonoBehaviour
{
    public GameObject warningText;
    public GameObject missilePrefab;
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("plane entered danger zone");

            if (warningText != null)
                warningText.SetActive(true);

            if (missilePrefab != null && spawnPoint != null)
            {
                GameObject newMissile = Instantiate(missilePrefab, spawnPoint.position, spawnPoint.rotation);

                MissileHoming homingScript = newMissile.GetComponent<MissileHoming>();
                if (homingScript != null)
                {
                    homingScript.target = other.transform;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("plane exited danger zone");

            if (warningText != null)
                warningText.SetActive(false);
        }
    }
}