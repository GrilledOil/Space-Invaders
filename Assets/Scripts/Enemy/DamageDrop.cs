using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDrop : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Movement());
    }

    IEnumerator Movement()
    {
        while (true)
        {
            yield return new WaitForSeconds(.25f);
            transform.position -= new Vector3(0f, 0.75f/2, 0f);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        coll.gameObject.GetComponent<PlayerShooting>().bulletCooldown -= 0.25f;
        Destroy(gameObject);
    }
}
