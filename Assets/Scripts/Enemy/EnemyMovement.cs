using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float movementTime;
    private int direction = 1;
    private const float moveDivide = 10;

    void Start()
    {
        StartCoroutine(Movement());
    }

    void Update()
    {
        movementTime = GameObject.Find("GameManager").GetComponent<GameManagement>().moveTime;
    }

    IEnumerator Movement() 
    {
        var gm = GameObject.Find("GameManager").GetComponent<GameManagement>();
        while (true) // Enemy moves infinitely until either enemy or player dies
        {
            while (!gm.canSwitch)
            {
                yield return new WaitForSeconds(movementTime);
                transform.position += new Vector3(direction * (1f / moveDivide), 0f, 0f);
            }

            yield return new WaitForSeconds(movementTime);
            transform.position -= new Vector3(0f, .75f, 0f);
            direction *= -1;

            while (gm.canSwitch)
            {
                yield return null;
            }
        }
    }
}
