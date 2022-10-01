using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : Interactable
{
    // public GameObject target;
    
    public override void Interact() {
        GameObject.Destroy(gameObject);
        // if (this.target) {
        //     Door door = this.target.GetComponent<Door>();
        //     if (door != null) {
        //         Debug.Log("Door!!!");
        //         door.ReactToInteraction();
        //     }
        // }
    }

}
