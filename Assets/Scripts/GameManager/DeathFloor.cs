using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            GameObject.Find("GameManager").GetComponent<GameManagement>().PermaDeath(); // Instakills player if the enemy reaches the bottom of the screen
        }
    }
}