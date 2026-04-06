using UnityEngine;

public class FlightExamManager : MonoBehaviour
{
    public static FlightExamManager instance;

    public bool hasTakenOff = false;
    public bool enteredDangerZone = false;
    public bool threatCleared = false;
    public bool missionComplete = false;
    public bool playerWasHit = false;

    private void Awake()
    {
        instance = this;
    }
}