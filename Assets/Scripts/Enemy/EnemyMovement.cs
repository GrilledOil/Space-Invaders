using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int screenSize = 7;

    private float movementTime;
    private Rigidbody2D rb;

    private float moveDivide = 14;

    private float moveAmount;

    void Start()
    {
        movementTime = GameObject.Find("GameManager").GetComponent<GameManagement>().moveTime;
        moveAmount = GameObject.Find("GameManager").GetComponent<WaveSystem>().enemyPerRow;

        rb = gameObject.GetComponent<Rigidbody2D>();


        StartCoroutine(Movement());
    }

    void Update()
    {

    }

    IEnumerator Movement() 
    {
        while (true) // Enemy moves infinitely until either enemy or player dies
        {
            for (int i = 0; i < moveDivide*2; i++)
            {
                yield return new WaitForSeconds(movementTime); // The amount of time between movements
                transform.position += new Vector3(1f/moveDivide, 0f, 0f);
            }

            yield return new WaitForSeconds(movementTime); // The amount of time between movements
            transform.position -= new Vector3(0f, 1.5f, 0f);

            for (int i = 0; i < moveDivide*2; i++)
            {
                yield return new WaitForSeconds(movementTime); // The amount of time between movements
                transform.position -= new Vector3(1f/moveDivide, 0f, 0f);
            }

            yield return new WaitForSeconds(movementTime); // The amount of time between movements
            transform.position -= new Vector3(0f, 1.5f, 0f);
        }
    }
}
