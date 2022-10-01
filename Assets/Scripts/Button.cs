using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable
{
    public delegate void Activate(GameObject obj);
    public event Activate onActivate;
    
    public override void Interact() {
        Debug.Log("Buttoned!");
        if (onActivate != null) {
            Debug.Log("Activated!");
            onActivate(gameObject);
        }
    }
}