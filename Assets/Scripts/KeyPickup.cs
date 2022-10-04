using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class KeyPickup : Interactable
{
    public GameObject target;
    private GameObject carrier = null;

    private int enableInTicks = 0;

    public void Start() {
    }

    public void Update() {
        if (this.carrier != null) {
            // F to drop
            if (Input.GetKeyDown(KeyCode.F)) {
                this.drop();
            }
            // Close to target -> unlock it!
            if (this.target != null) {
            // Debug.Log("Key pickup");
                Vector2 diff = this.transform.position
                    - this.target.transform.position;
                float dis = diff.magnitude;
                // Debug.Log("distance to target"+dis);
                if (dis < 4) {
                    this.carrier = null;
                    this.target.GetComponentInChildren<InteractionTarget>().ReactToInteraction(this.gameObject);
                    this.disabled = true;
                    this.vanish();
                }
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

    public void vanish() {
        GameObject.Destroy(this.gameObject);
    }

    private void PlaceOnTopOfCarrier(float offset = 0) {
        if (this.carrier == null) {
            return;
        }
        Vector2 trgPos = this.carrier.GetComponent<Rigidbody2D>().position + new Vector2(0, 1 + offset);
        this.GetComponent<Rigidbody2D>().MovePosition(trgPos);
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        OnTriggerEnter2D(collision.collider);
    }

}
