using System.Collections;
using UnityEngine;

public class DangerZoneTrigger : MonoBehaviour
{
    public GameObject warningText;
    public GameObject missilePrefab;
    public Transform spawnPoint;
    public float spawnDelay = 5f;

    public AudioSource audioSource;
    public AudioClip warningClip;
    public AudioClip missileClip;

    private Coroutine spawnCoroutine;
    private GameObject currentMissile;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (FlightExamManager.instance != null)
            {
                FlightExamManager.instance.enteredDangerZone = true;
                FlightExamManager.instance.threatCleared = false;
                FlightExamManager.instance.playerWasHit = false;
            }

            Debug.Log("your plane has entered danger zone");

            if (warningText != null)
            {
                warningText.SetActive(true);
            }

            if (audioSource != null && warningClip != null)
            {
                audioSource.PlayOneShot(warningClip);
            }

            if (spawnCoroutine == null)
            {
                spawnCoroutine = StartCoroutine(SpawnMissileWithDelay(other));
            }
        }
    }

    private IEnumerator SpawnMissileWithDelay(Collider other)
    {
        yield return new WaitForSeconds(spawnDelay);

        if (missilePrefab != null && spawnPoint != null)
        {
            currentMissile = Instantiate(missilePrefab, spawnPoint.position, spawnPoint.rotation);

            MissileHoming homingScript = currentMissile.GetComponent<MissileHoming>();
            if (homingScript != null)
            {
                homingScript.target = other.transform;
            }

            if (audioSource != null && missileClip != null)
            {
                audioSource.PlayOneShot(missileClip);
            }
        }

        spawnCoroutine = null;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bool wasHit = false;

            if (FlightExamManager.instance != null)
            {
                wasHit = FlightExamManager.instance.playerWasHit;
                FlightExamManager.instance.threatCleared = true;
            }

            if (!wasHit)
            {
                Debug.Log("your plane has exited danger zone");
            }

            if (warningText != null)
            {
                warningText.SetActive(false);
            }

            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
                spawnCoroutine = null;
            }

            if (currentMissile != null)
            {
                Destroy(currentMissile);
                currentMissile = null;
            }
        }
    }
}