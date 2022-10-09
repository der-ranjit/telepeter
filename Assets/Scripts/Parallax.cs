using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField, Range(0f, 2f)] private float parallaxFactor = 0.5f;
    public GameObject targetCamera;

    private Vector3 initialPos;
    private Vector3 initialCamPos;

    // Start is called before the first frame update
    void Start()
    {
        this.initialPos = this.transform.position;
        this.initialCamPos = Camera.main.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // GameObject target = targetCamera ? targetCamera : GameObject.FindWithTag("Player");
        this.transform.position = this.initialPos + (Camera.main.transform.position - this.initialCamPos) * parallaxFactor;   
    }
}
