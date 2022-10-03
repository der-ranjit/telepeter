using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxSlopeSpeed = 3f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;

    private Vector2 direction;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D body;
    private Ground ground;

    private float maxSpeedChange;
    private float acceleration;
    private bool onGround;

    private float remainingSpeedUpTime = 0;


    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();

    }

    bool IsOnSlope()
    {
        
        bool isGround = ground.GetOnGround();
        float slopeAngle = Vector2.Angle(Vector2.up, ground.GetNormal());
        // Debug.Log(slopeAngle);
        if ( slopeAngle > 10 && slopeAngle <= 45 && isGround){
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = input.RetrieveMoveInput();

        if (direction.x != 0) {
            Vector3 scale = this.gameObject.transform.localScale;
            float sx = Mathf.Abs(scale.x);
            this.gameObject.transform.localScale = new Vector3((direction.x > 0 ? sx : -sx), scale.y, scale.z);
        }

        if (IsOnSlope())
        {
            Vector2 groundNormal = ground.GetNormal();
            Vector2 slopeDirection = Vector2.up - groundNormal * Vector2.Dot(Vector2.up, groundNormal);

            desiredVelocity = new Vector2(direction.x,slopeDirection.y) * Mathf.Max(maxSlopeSpeed - ground.GetFriction(), 0);
            // Debug.Log("DesiredVel"+desiredVelocity);
        } else {
            desiredVelocity = new Vector2(direction.x, 0) * Mathf.Max(maxSpeed - ground.GetFriction(), 0);   
        }

        // Update speed-up-time
        if (this.remainingSpeedUpTime > 0) {
            this.remainingSpeedUpTime = Mathf.MoveTowards(this.remainingSpeedUpTime, 0, Time.deltaTime);
            // Affect desired speed
            desiredVelocity *= 2;
        }
    }


    void FixedUpdate()
    {
        onGround = ground.GetOnGround();
        velocity = body.velocity;

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        
        
        if (IsOnSlope())
        {
            velocity.x = Mathf.MoveTowards(velocity.x,desiredVelocity.x,maxSpeedChange);
            velocity.y = Mathf.MoveTowards(velocity.y,desiredVelocity.y,maxSpeedChange);
            // Debug.Log("SlopeVelocity"+velocity);
        } else {
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
            // Debug.Log("GroundVelocity"+velocity);
        }

        body.velocity = velocity;
    }

    public float GetSpeedUpDuration() {
        return remainingSpeedUpTime;
    }

    public void SetSpeedUpDuration(float duration) {
        this.remainingSpeedUpTime = duration;
    }

}
