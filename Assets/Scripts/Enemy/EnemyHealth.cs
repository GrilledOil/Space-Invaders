using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Animator animator;

    public int dropChance;

    public GameObject speedDropPrefab;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            if (Random.Range(0, 100) <= dropChance)
            {
                Instantiate(speedDropPrefab, transform.position, transform.rotation);
            }
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        GameObject.Find("GameManager").GetComponent<GameManagement>().score += 100;

        Destroy(gameObject.GetComponent<BoxCollider2D>());
        Destroy(gameObject.GetComponent<EnemyMovement>());

        animator = gameObject.GetComponent<Animator>();

        animator.SetTrigger("Impact");

        yield return new WaitForSeconds(0.5f);
        
        Destroy(gameObject);
    }
}
