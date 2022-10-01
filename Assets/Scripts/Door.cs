using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractionTarget
{
    public bool isOpen = false;
    public GameObject target;

    private void OnEnable() {
        target.GetComponent<Button>().onActivate += ReactToInteraction;
    }

    private void OnDisable() {
        target.GetComponent<Button>().onActivate -= ReactToInteraction;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DoSomething() {
        Debug.Log("Hello");
    }

    public override void ReactToInteraction(GameObject obj) {
        Debug.Log(obj);
        if (obj == target) {
            if (this.isOpen) {
                this.Close();
            } else {
                this.Open();
            }
        }
    }

    public void Open() {
        this.isOpen = true;
        Debug.Log("Opening");
    }

    public void Close() {
        this.isOpen = false;
        Debug.Log("Closing");
    }
}
