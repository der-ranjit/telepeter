using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{

    private Cinemachine.CinemachineVirtualCamera followCamera;

    // Start is called before the first frame update
    void Start()
    {
        followCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (followCamera != null && player != null) {
            followCamera.Follow = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
