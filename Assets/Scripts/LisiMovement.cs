using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LisiMovement : MonoBehaviour
{

private Rigidbody2D rigidBody;
public GameObject player;
public float MoveSpeed = 5f;
private float vertical;
private float horizontal;
private Vector3 initialPlayerScale;

 void Start ()
 {
   rigidBody = GetComponent<Rigidbody2D>();
   initialPlayerScale = player.transform.localScale;
 }

 void Update ()
 {
 
    horizontal = Input.GetAxisRaw("Horizontal");
    vertical = Input.GetAxisRaw("Vertical"); 
 }

 private void FixedUpdate()
 {  
    if (horizontal < -0.01f) {
      player.transform.localScale = new Vector3(-1 * initialPlayerScale.x, initialPlayerScale.y, initialPlayerScale.z);
    } else if (horizontal > 0.01f)
    {
      player.transform.localScale = initialPlayerScale;
    }
    rigidBody.velocity = new Vector2(horizontal * MoveSpeed, vertical * MoveSpeed);
 }
}
