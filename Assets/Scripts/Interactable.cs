using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    bool disabled = false;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public virtual void Interact() {
        Debug.Log("Unspecified interaction with player!");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!this.disabled) {
            if (other.tag != "player") {
                this.disabled = true;
                Interact();
            }
        }
    }
}
