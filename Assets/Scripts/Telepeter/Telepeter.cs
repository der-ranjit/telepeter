using UnityEngine;

// Teleports all "Player" tagged objects to this transforms location 
public class Telepeter : MonoBehaviour
{

    public static Telepeter Instance;
    public string[] respawnComments = { "" };
    public float respawnDuration = 10;
    public string tagToTeleport = "Player";
    public float remainingTime;

    public float currentIteration = 1;

    private float timePassed = 0;
    
    void Awake() {
        Instance = this;
    }

    void Start() {
        remainingTime = respawnDuration - timePassed;
    }

    public void SetRemainingTime(float remainingTime) {
        timePassed = respawnDuration - remainingTime;
        this.remainingTime = remainingTime;
    }

    void Update() {
        timePassed += Time.deltaTime;
        remainingTime = respawnDuration - timePassed;
        if (timePassed >= respawnDuration) {
            timePassed = 0;
            currentIteration++;
            this.TeleportPlayer();
        }
    }

    void TeleportPlayer() {
        GameObject[] players = GameObject.FindGameObjectsWithTag(tagToTeleport);
        Vector2 targetPos = transform.position + new Vector3(0, 2f, 0);
        DialogManager.Instance.TriggerDialogWithRandomText(GetType().Name, respawnComments, gameObject);
        foreach (GameObject player in players) {
            player.transform.position = targetPos;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.5f);
        }
    }
}
