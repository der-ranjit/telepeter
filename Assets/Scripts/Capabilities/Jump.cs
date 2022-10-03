using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 10f)] private float jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int maxAirJumps = 0;

    [SerializeField, Range(0, 8)] private int jumpBuffer = 3;
    [SerializeField, Range(0, 8)] private int coyoteTime = 3;

    [SerializeField, Range(0f, 5f)] private float downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;

    private Rigidbody2D body;
    private Ground ground;
    private Vector2 velocity;

    private int jumpPhase;
    private float defaultGravityScale;

    private bool desiredJump;
    private bool onGround;
    private int jumpBufferFramesLeft;
    private int coyoteTimeFramesLeft;

    private bool isJumping = false;

    
    public bool isJetpack = false;
    [SerializeField, Range(0f, -1f)] private float upwardJetpackMultiplier = -0.5f;


    
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();

        defaultGravityScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        desiredJump |= input.RetrieveJumpInputDown();


    }

    void FixedUpdate()
    {
        onGround = ground.GetOnGround();
        velocity = body.velocity;
        bool isJumpPressed = input.RetrieveJumpInput();
        
        
        // Coyote Time 
        if (onGround)
        {
            jumpPhase = 0;
            coyoteTimeFramesLeft = coyoteTime;
            isJumping = false;
        }

        if(!onGround && coyoteTimeFramesLeft > 0){
            // Debug.Log("coyoteTime:"+coyoteTimeFramesLeft);
            // Debug.Log("Ground:"+onGround);
            coyoteTimeFramesLeft -= 1;
            onGround = true;
        }


        // Jump Buffer and jump action
        if (desiredJump)
        {
            desiredJump = false;
            // if (isJetpack && onGround){
            //     // Debug.Log("Jetpack Thrust");
            //     body.gravityScale = -2.0f;
            // }
            jumpBufferFramesLeft = jumpBuffer;
            JumpAction();
        } else if (jumpBufferFramesLeft > 0)
        {
            // Debug.Log("Jump Buffer:" + jumpBufferFramesLeft);
            jumpBufferFramesLeft -= 1;
            JumpAction();
        }

        // Variable jump height implementation
        if (isJumpPressed && isJumping)
        {
            // if (body.velocity.y > 0)
            // {
            //     body.gravityScale = upwardMovementMultiplier;
            // }
            // else
            // {
            //     body.gravityScale = downwardMovementMultiplier;
            // }
        }
        else
        {
            body.gravityScale = downwardMovementMultiplier;
        }

        if (isJumpPressed && isJetpack) {
            this.PerformJetpack();
        }

        body.velocity = velocity;
    }

    private void JumpAction()
    {
        if (onGround || jumpPhase < maxAirJumps)
        {
            Debug.Log("Jump Performed");
            // Jump
            jumpBufferFramesLeft = 0;
            coyoteTimeFramesLeft = 0;
            jumpPhase += 1;
            velocity.y = jumpHeight * 3;
            isJumping = true;
            Debug.Log(velocity.y + "," + body.gravityScale);
        }
    }

    private void PerformJetpack() {
        // velocity.y += 0.5f;
    }
}
