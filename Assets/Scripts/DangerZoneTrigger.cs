using System.Collections;
using UnityEngine;

public class DangerZoneTrigger : MonoBehaviour
{
    public GameObject warningText;
    public GameObject missilePrefab;
    public Transform spawnPoint;
    public float spawnDelay =5f;

    private Coroutine spawnCoroutine;
    private GameObject currentMissile;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("your plane has entered danger zone");

            if (warningText != null)
            {
                warningText.SetActive(true);
            }

            if (spawnCoroutine == null)
            {
                spawnCoroutine =StartCoroutine(SpawnMissileWithDelay(other));
            }
        }
    }

    private IEnumerator SpawnMissileWithDelay(Collider other)
    {
        yield return new WaitForSeconds(spawnDelay);

        if (missilePrefab != null && spawnPoint != null)
        {
            currentMissile = Instantiate(missilePrefab, spawnPoint.position, spawnPoint.rotation);

            MissileHoming homingScript =currentMissile.GetComponent<MissileHoming>();
            if (homingScript != null)
            {
                homingScript.target =other.transform;
            }
        }

        spawnCoroutine = null;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("your plane has exited danger zone");

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