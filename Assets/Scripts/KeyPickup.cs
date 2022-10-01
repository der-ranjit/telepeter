using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : Interactable
{
    // public GameObject target;
    private GameObject carrier = null;

    private int enableInTicks = 0;

    public void Start() {
    }

    public void Update() {
        if (this.carrier != null) {
            if (Input.GetKeyDown(KeyCode.F)) {
                this.drop();
            }
        }
    }

    public void FixedUpdate() {
        if (this.carrier != null) {
            this.PlaceOnTopOfCarrier();
        }
        if (this.enableInTicks > 0) {
            this.enableInTicks--;
            if (this.enableInTicks <= 0) {
                this.disabled = false;
            }
        }
    }
    
    public override void Interact(GameObject other) {
        this.disabled = true;
        this.carrier = other;
        this.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void drop() {
        if (this.carrier == null) {
            return;
        }
        this.enableInTicks = 30;
        this.GetComponent<BoxCollider2D>().enabled = true;
        this.PlaceOnTopOfCarrier(0.2f);
        this.GetComponent<Rigidbody2D>().velocity = this.carrier.GetComponent<Rigidbody2D>().velocity + new Vector2(0, 5);
        this.carrier = null;
    }

    private void PlaceOnTopOfCarrier(float offset = 0) {
        if (this.carrier == null) {
            return;
        }
        Vector2 trgPos = this.carrier.GetComponent<Rigidbody2D>().position + new Vector2(0, 1 + offset);
        this.GetComponent<Rigidbody2D>().MovePosition(trgPos);
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        if (!this.disabled) {
            if (collision.gameObject.tag == "Player") {
                Interact(collision.gameObject);
            }
        }
    }

}
