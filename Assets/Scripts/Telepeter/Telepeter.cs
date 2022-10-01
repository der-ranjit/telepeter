using UnityEngine;

// Teleports all "Player" tagged objects to this transforms location 
public class Telepeter : MonoBehaviour
{

    public static Telepeter Instance;
    public float timer = 10;
    public string tagToTeleport = "Player";
    public float remainingTime;

    public float currentIteration = 1;

    private float timePassed = 0;
    
    void Awake() {
        Instance = this;
    }

    void Start() {
        remainingTime = timer - timePassed;
    }

    void Update() {
        timePassed += Time.deltaTime;
        remainingTime = timer - timePassed;
        if (timePassed >= timer) {
            timePassed = 0;
            currentIteration++;
            GameObject[] players = GameObject.FindGameObjectsWithTag(tagToTeleport);
            foreach (GameObject player in players) {
                player.transform.position = transform.position;
            }
        }
    }
}
