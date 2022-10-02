using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : Interactable
{

    public override void Interact(GameObject other) {
        this.disabled = true;
        this.GetComponent<BoxCollider2D>().enabled = false;
        GameObject.Destroy(this.gameObject);
        Debug.Log("Jetpacking!");
        GameObject.FindGameObjectWithTag("Player").GetComponent<Jump>().isJetpack = true;
    }
}
