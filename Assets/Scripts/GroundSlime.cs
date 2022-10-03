using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Check rarely to not hurt performance unnecessarily
        if (Random.Range(0f, 1f) < 0.1) {
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            if (target) { // TODO: check if target is currently not sped up
                // TODO: speed up player for 0.5 seconds or so
            }
        }
    }
}
