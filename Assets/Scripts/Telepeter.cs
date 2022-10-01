using UnityEngine;

// Teleports all "Player" tagged objects to this transforms location 
public class Telepeter : MonoBehaviour
{
    public float timeUntilTeleport = 10;
    public string usedTag = "Player";
    private float timePassed = 0;
    

    void Start() {
    }

    void Update() {
        timePassed += Time.deltaTime;
        if (timePassed >= timeUntilTeleport) {
            timePassed = 0;
            GameObject[] players = GameObject.FindGameObjectsWithTag(usedTag);
            foreach (GameObject player in players) {
                player.transform.position = transform.position;
            }
        }
    }
}
