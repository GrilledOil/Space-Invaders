using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDrop : MonoBehaviour
{
    public AudioClip speedSoundClip;

    void Start()
    {
        StartCoroutine(Movement());
    }

    IEnumerator Movement()
    {
        while (true)
        {
            yield return new WaitForSeconds(.25f);
            transform.position -= new Vector3(0f, 0.75f / 2, 0f);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject camera = GameObject.Find("cams");
        AudioSource.PlayClipAtPoint(speedSoundClip, camera.transform.position, 1f);
        coll.gameObject.GetComponent<PlayerShooting>().bulletCooldown -= 0.075f;
        Destroy(gameObject);
    }
}
