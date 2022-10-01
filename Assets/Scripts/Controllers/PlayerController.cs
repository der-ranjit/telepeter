using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]
public class PlayerController : InputController
{
    public float velocity;
    // public Vector2 maxVelocity;
    public Rigidbody2D playerRigidBody;


    public override bool RetrieveJumpInput()
    {
        return Input.GetButton("Jump");
    }

    public override bool RetrieveJumpInputDown(){
        return Input.GetButtonDown("Jump");
    }

    public override float RetrieveMoveInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    //Fixed Update is called once per frame useful for physics updates
    void FixedUpdate()
    {

        float _horizontalInput = Input.GetAxis("Horizontal");
        float _verticalInput = Input.GetAxis("Vertical");
        
        Vector2 _direction = new Vector2(_horizontalInput,_verticalInput).normalized;
        Debug.Log(_direction);
        playerRigidBody.velocity +=  _direction * velocity * Time.deltaTime;
       
    }
}
