using UnityEngine;

public class LandingAreaTrigger : MonoBehaviour
{
    public GameObject missionText;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject missile = GameObject.FindWithTag("Missile");

            if (missile == null)
            {
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