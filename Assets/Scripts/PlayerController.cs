using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity;
    public Vector2 maxVelocity;
    public Rigidbody2D playerRigidBody;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if ( Mathf.Abs( playerRigidBody.velocity.x) < maxVelocity.x)
            {
                playerRigidBody.velocity += Vector2.right * velocity;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if ( Mathf.Abs( playerRigidBody.velocity.x) < maxVelocity.x)
            {
                playerRigidBody.velocity += -Vector2.right * velocity;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if ( Mathf.Abs( playerRigidBody.velocity.y) < maxVelocity.y)
            {
                playerRigidBody.velocity += 10*Vector2.up * velocity;
            }
        }
    }

    void FixedUpdate()
    {

    }
}
