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
    private int jumpBufferStartFrame;

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
        // int currentBufferCount = Time.frameCount - jumpBufferStartFrame;
        if (onGround)
        {
            jumpPhase = 0;
        }

        if (desiredJump)
        {
            desiredJump = false;
            JumpAction();
        }
        // if (desiredJump){
        //     jumpBufferStartFrame = Time.frameCount;
        //     desiredJump = false;
        //     JumpAction();
        // } else if ( currentBufferCount <= jumpBuffer){
        //     Debug.Log(currentBufferCount);
        //     JumpAction();
        // }
        

        if (isJumpPressed)
        {
            if (body.velocity.y > 0)
            {
                body.gravityScale = upwardMovementMultiplier;
            }
            else if (body.velocity.y < 0)
            {
                body.gravityScale = downwardMovementMultiplier;
            }
            else if (body.velocity.y == 0)
            {
                body.gravityScale = defaultGravityScale;
            }
        } else {
            body.gravityScale = downwardMovementMultiplier;
        }

        body.velocity = velocity;
    }

    private void JumpAction()
    {
        if (onGround || jumpPhase < maxAirJumps)
        {
            jumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
            if (velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            velocity.y += jumpSpeed;
        }
    }
}
