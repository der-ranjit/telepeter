using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] private float jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int maxAirJumps = 0;

    // [SerializeField, Range(0, 8)] private int jumpBuffer = 3;
    // [SerializeField, Range(0, 8)] private int coyoteTime = 3;

    [SerializeField, Range(0f, 5f)] private float downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;

    [SerializeField] private bool isVariableHeight = true;

    private Rigidbody2D body;
    private Ground ground;
    private Vector2 velocity;

    private int jumpPhase;
    private float defaultGravityScale;

    private bool desiredJump;
    private bool onGround;

    private bool isJumpPressed;
    private bool isJumpUp;
    private bool isJetpacking = false;
    private GameObject jetpackObject;
    // private int jumpBufferFramesLeft;
    // private int coyoteTimeFramesLeft;


    public bool isJetpack = false;
    [SerializeField, Range(0f, -1f)] private float upwardJetpackMultiplier = -0.5f;



    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();

        defaultGravityScale = 1f;

        jetpackObject = this.transform.Find("jetpack_for_player").gameObject;
        jetpackObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        desiredJump |= input.RetrieveJumpInputDown();
        isJumpPressed = input.RetrieveJumpInput();
        isJumpUp = input.RetrieveJumpInputUp();


    }

    void FixedUpdate()
    {
        onGround = ground.GetOnGround();
        velocity = body.velocity;

        if (onGround)
        {
            jumpPhase = 0;
        }

        // jump action
        if (desiredJump)
        {
            desiredJump = false;
            JumpAction();
        } 
        if (!isJumpPressed) {
            isJetpacking = false;
            this.transform.Find("jetpack_for_player")
                .GetComponent<Animator>().SetBool("isThrustActive", false);
        }

        // Variable Jump Height
        if ( !isVariableHeight || (!isJumpUp && isJumpPressed)){
            if (body.velocity.y > 0)
            {
                body.gravityScale = !isJetpack ? upwardMovementMultiplier : upwardJetpackMultiplier;
            }
            else if (body.velocity.y < 0)
            {
                body.gravityScale = !isJetpack ? downwardMovementMultiplier : upwardJetpackMultiplier;
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

        // Cat animation update
        Animator catAnimator = this.transform.Find("space_cat_perspective")
            .GetComponent<Animator>();
        catAnimator.SetBool("onGround", onGround);
        catAnimator.SetBool("isJetpacking", isJetpacking);
        catAnimator.SetFloat("horizontalSpeed", Mathf.Abs(this.GetComponent<Rigidbody2D>().velocity.x));
    }

    private void JumpAction()
    {
        // Debug.Log("Jump Called");
        if (onGround || jumpPhase < maxAirJumps || isJetpack)
        {
            jumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f* Physics2D.gravity.y*jumpHeight);
            if (velocity.y > 0f){
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            velocity.y += jumpSpeed;

            if (isJetpack) {
                Debug.Log("Jetpack active");
                isJetpacking = true;
                this.transform.Find("jetpack_for_player")
                    .GetComponent<Animator>().SetBool("isThrustActive", true);
            }
        }
    }

    public void enableJetpack() {
        this.isJetpack = true;
        jetpackObject.SetActive(true);
    }
}
