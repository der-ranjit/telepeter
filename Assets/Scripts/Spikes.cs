using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            this.killPlayer();
        }
    }

    public void killPlayer() {
        // For now, simply trigger teleportation
        Telepeter.Instance.SetRemainingTime(0.5f);
        // TODO some death animation thing
    }
}
