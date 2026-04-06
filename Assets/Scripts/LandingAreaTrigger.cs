using UnityEngine;

public class LandingAreaTrigger : MonoBehaviour
{
    public GameObject missionText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (FlightExamManager.instance == null)
            {
                return;
            }

            if (!FlightExamManager.instance.enteredDangerZone)
            {
                Debug.Log("enter the danger zone first");
                return;
            }

            if (!FlightExamManager.instance.threatCleared)
            {
                Debug.Log("cannot land, threat still active");
                return;
            }

            GameObject missile = GameObject.FindWithTag("Missile");

            if (missile == null)
            {
                FlightExamManager.instance.missionComplete = true;
                Debug.Log("landed successfully, mission complete congratulations");

                if (missionText != null)
                {
                    missionText.SetActive(true);
                }
            }
            else
            {
                Debug.Log("cannot land, threat still active");
            }
        }
    }
}