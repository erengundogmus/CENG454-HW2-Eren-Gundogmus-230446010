using UnityEngine;

public class DangerZoneTrigger : MonoBehaviour
{
    public GameObject warningText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("plane entered danger zone");

            if (warningText != null)
            {
                warningText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("plane exited danger zone");

            if (warningText != null)
            {
                warningText.SetActive(false);
            }
        }
    }
}