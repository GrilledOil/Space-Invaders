using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    
    public Vector2 moveDir;
    private Rigidbody2D rb;
    
    public bool isMoving;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        isMoving = false;
    }

    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        moveDir.Normalize();

        if (moveDir.x == 0 && moveDir.y == 0)
        {
            isMoving = false;
        }   else
        {
            isMoving = true;
        }

        rb.velocity = moveDir * moveSpeed;
    }
}
