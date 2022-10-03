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

    public override bool RetrieveJumpInputUp(){
        return Input.GetButtonUp("Jump");
    }

    public override float RetrieveMoveInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public override bool RetrieveUseButton()
    {
        return Input.GetButton("Use");
    }

    public override bool RetrieveUseButtonDown()
    {
        return Input.GetButtonDown("Use");
    }

    public override bool RetrieveUseButtonUp()
    {
        return Input.GetButtonUp("Use");
    }
}
