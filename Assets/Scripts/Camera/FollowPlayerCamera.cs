using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{

    private Cinemachine.CinemachineVirtualCamera followCamera;

    public string tagToFollow = "Player";

    private bool updateRequired = false;
    // Start is called before the first frame update
    void Start()
    {
        followCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        GameObject player = GameObject.FindGameObjectWithTag(tagToFollow);
        if (followCamera != null && player != null) {
            followCamera.Follow = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (followCamera != null && followCamera.Follow == null || updateRequired) {
            followCamera.Follow = GameObject.FindGameObjectWithTag(tagToFollow)?.transform;
            updateRequired = false;
        }
    }

    public void SetTagToFollow(string tag) {
        tagToFollow = tag;
        updateRequired = true;
    }
}
