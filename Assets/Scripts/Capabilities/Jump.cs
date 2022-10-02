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
            if (isJetpack && onGround){
                // Debug.Log("Jetpack Thrust");
                body.gravityScale = -2.0f;
            }
            jumpBufferFramesLeft = jumpBuffer;
            JumpAction();
        }
        if (jumpBufferFramesLeft > 0)
        {
            // Debug.Log("Jump Buffer:" + jumpBufferFramesLeft);
            jumpBufferFramesLeft -= 1;
            JumpAction();
        }

        // Variable jump height implementation
        if (isJumpPressed)
        {
            if (body.velocity.y > 0)
            {
                body.gravityScale = !isJetpack? upwardMovementMultiplier: upwardJetpackMultiplier;
            }
            else if (body.velocity.y < 0)
            {
                body.gravityScale = !isJetpack? downwardMovementMultiplier: upwardJetpackMultiplier;
            }
            else if (body.velocity.y == 0)
            {
                body.gravityScale = defaultGravityScale;
            }
        }
        else
        {
            body.gravityScale = downwardMovementMultiplier;
        }

        body.velocity = velocity;
    }

    private void JumpAction()
    {
        // Debug.Log("Jump Called");
        if (onGround || jumpPhase < maxAirJumps || isJetpack )
        {
            // Debug.Log("Activated");
            jumpBufferFramesLeft = 0;
            coyoteTimeFramesLeft = 0;
            jumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
            if (velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            velocity.y += jumpSpeed;
        }
        // else
        // {
        //     Debug.Log("Not Activated");
        // }
    }
}
