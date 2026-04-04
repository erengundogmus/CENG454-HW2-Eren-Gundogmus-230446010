using UnityEngine;

public class LandingAreaTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("landed successfully");
        }
    }
}