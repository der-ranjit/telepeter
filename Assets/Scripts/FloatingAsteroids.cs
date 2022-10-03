using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingAsteroids : MonoBehaviour
{
    // Just add this script to the asteroid sprites it will handle behaviour accordingly. 
    // distance that the float will "scale
    [SerializeField, Range(-1f, 1f)] private float scalingDistance = 1;
    [SerializeField, Range(-1f, 1f)] private float rotationSpeed = 1;
    // distance that the float will "travel"
    [SerializeField, Range(0f, 6f)] private float distance = 1;

    // time it takes in seconds to complete one circle
    [SerializeField, Range(0f, 10f)] private float speed = 6;

    private Vector2 iPosition;
    private Vector2 iScale;
    private SpriteRenderer spriteRenderer ;
    private string spriteName = "";
    void Awake(){
        spriteRenderer = GetComponent <SpriteRenderer>();
        spriteName = spriteRenderer.sprite.name; 
    }
    void Start() {
        iPosition = transform.position;
        iScale = transform.localScale;
    }

    void Update()
    {
        if (spriteRenderer != null){
            
            if ( spriteName == "balzing_asteroid"){
                transform.Translate(new Vector2(-1,-1)*speed*Time.deltaTime);
            } else if (spriteName == "asteroid"){
                Vector2 nScale= transform.localScale;
                Vector2 nPos = transform.position;
                nScale.x = iScale.x + scalingDistance*Mathf.Cos( speed * Time.time);
                nScale.y = iScale.y + scalingDistance*Mathf.Cos( speed * Time.time);
                nPos.y = iPosition.y + distance* Mathf.Sin(speed * Time.time);
                transform.position = nPos;                
                transform.localScale = nScale;
                transform.Rotate(0f,0f,Mathf.Sin(rotationSpeed*Time.time),Space.Self);
            } else if (spriteName == "asteroid2"){
                Vector2 nScale= transform.localScale;
                Vector2 nPos = transform.position;

                nScale.x = iScale.x + scalingDistance*Mathf.Cos( speed * Time.time);
                nScale.y = iScale.y + scalingDistance*Mathf.Cos( speed * Time.time);
                nPos.y = iPosition.y + distance* Mathf.Sin(speed * Time.time);
                transform.position = nPos;                
                transform.localScale = nScale;
                transform.Rotate(0f,0f,Mathf.Sin(rotationSpeed*Time.time),Space.Self);
            }
        }        
    }
}
