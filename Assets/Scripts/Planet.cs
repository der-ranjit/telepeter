using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float angle = 15f * Time.time;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
