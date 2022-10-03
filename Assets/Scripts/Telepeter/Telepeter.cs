using UnityEngine;

// Teleports all "Player" tagged objects to this transforms location 
public class Telepeter : MonoBehaviour
{

    [SerializeField] private InputController input = null;
    public static Telepeter Instance;
    public string[] respawnComments = { "" };
    public float respawnDuration = 10;
    public string tagToTeleport = "Player";
    public float remainingTime;

    public float currentIteration = 1;

    private float timePassed = 0;

    public bool isPicked = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Debug.Log("Telepeter Enter "+ collider.gameObject.name);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        Debug.Log("Telepeter Stay " + collider.gameObject.name);
        Interact(collider);

    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        // Debug.Log("Telepeter Exit "+ collider.gameObject.name);
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        remainingTime = respawnDuration - timePassed;
    }

    public void SetRemainingTime(float remainingTime)
    {
        timePassed = respawnDuration - remainingTime;
        this.remainingTime = remainingTime;
    }

    void Update()
    {
        if (GameStateManager.Instance.currentGameLifeState != GameLifeStates.Running)
        {
            return;
        }

        timePassed += Time.deltaTime;
        remainingTime = respawnDuration - timePassed;
        if (timePassed >= respawnDuration)
        {
            timePassed = 0;
            currentIteration++;
            this.TeleportPlayer();
        }

        if (isPicked)
        {
            transform.position = transform.parent.position;
            // transform.position.x += 5;
        }
    }

    void TeleportPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(tagToTeleport);
        Vector2 targetPos = transform.position + new Vector3(0, 2f, 0);
        DialogManager.Instance.TriggerDialogWithRandomText(GetType().Name, respawnComments, gameObject);
        foreach (GameObject player in players)
        {
            player.transform.position = targetPos;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.5f);
        }
    }

    void Interact(Collider2D coll)
    {
        // input = coll.gameObject.GetComponent<PlayerController>();
        bool use = input.RetrieveUseButtonDown(); 
        // bool useUp = input.RetrieveJumpInputUp();
        if ( use && isPicked)
        {

            isPicked = false;
            transform.parent = null;
        }
        
        if (use && !isPicked)
        {
            isPicked = true;
            transform.parent = coll.transform;
        }

    }
}
