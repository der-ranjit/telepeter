using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected bool disabled = false;

    public string[] interactionComments = { "" };

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public virtual void Interact(GameObject other) {
        Debug.Log("Unspecified interaction with player!");
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        if (!disabled) {
            if (other.tag == "Player") {
                disabled = true;
                DialogManager.Instance.TriggerDialogWithRandomText(GetType().Name, interactionComments, gameObject);
                Interact(other.gameObject);
            }
        }
    }
}
