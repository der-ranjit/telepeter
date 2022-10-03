using UnityEngine;

public class Spikes : MonoBehaviour
{
    public string[] killComments = { "" };

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            this.killPlayer();
        }
    }

    public void killPlayer() {
        // For now, simply trigger teleportation
        DialogManager.Instance.TriggerDialogWithRandomText(GetType().Name, killComments, gameObject);
        Telepeter.Instance.SetRemainingTime(0.5f);
        // TODO some death animation thing
    }
}
