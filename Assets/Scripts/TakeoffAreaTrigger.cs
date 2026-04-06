using UnityEngine;

public class TakeoffAreaTrigger : MonoBehaviour
{
    private bool hasTakenOff = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasTakenOff = false;

            if (FlightExamManager.instance != null)
            {
                FlightExamManager.instance.hasTakenOff = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !hasTakenOff)
        {
            hasTakenOff = true;
            Debug.Log("takeoff is successful");

            if (FlightExamManager.instance != null)
            {
                FlightExamManager.instance.hasTakenOff = true;
            }
        }
    }
}