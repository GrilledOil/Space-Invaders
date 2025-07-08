using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public AudioClip deathSoundClip;
     
    public GameObject speedDropPrefab;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            float dropChance = GameObject.Find("GameManager").GetComponent<GameManagement>().dropChance;
            if (Random.Range(0, 100) <= dropChance)
            {
                Instantiate(speedDropPrefab, transform.position, transform.rotation);
                GameObject.Find("GameManager").GetComponent<GameManagement>().dropChance *= 0.95f;
            }
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        GameObject camera = GameObject.Find("cams");
        AudioSource.PlayClipAtPoint(deathSoundClip, camera.transform.position, 1f);
        
        GameObject.Find("GameManager").GetComponent<GameManagement>().score += 100;

        Destroy(gameObject.GetComponent<BoxCollider2D>());
        Destroy(gameObject.GetComponent<EnemyMovement>());

        gameObject.GetComponent<Animator>().SetTrigger("Impact");

        yield return new WaitForSeconds(0.5f);

        GameObject.Find("GameManager").GetComponent<GameManagement>().moveTime -= 0.03f;

        Destroy(gameObject);
    }
}
