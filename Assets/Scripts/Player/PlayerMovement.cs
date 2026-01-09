using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float speed; //please_speed_i_need_this.png
    public float input;

    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        playerRb.linearVelocity = new Vector2(input * speed, playerRb.linearVelocity.y);
    }

    
}
