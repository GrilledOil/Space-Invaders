using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f);
    }
    void OnTriggerEnter2D()
    {
        Destroy(gameObject);
    }
}

public class PlayerShooting : MonoBehaviour
{
    public AudioClip shootSoundClip;

    private bool canShoot = true;

    private GameObject camera;

    public GameObject BulletPrefab;
    private float bulletSpeed = 7f;
    public float bulletCooldown = 1.5f;

    void Awake()
    {
        camera = GameObject.Find("cams");
    }

    void Update()
    {
        // X button on Xbox controller is JoystickButton2
        if (Input.anyKeyDown && canShoot == true)
        {
            bulletCooldown = Mathf.Clamp(bulletCooldown, 0.5f, 1.5f);
            StartCoroutine(Shoot(bulletCooldown));
        }
    }

    private IEnumerator Shoot(float waitTime)
    {
        canShoot = false;

        AudioSource.PlayClipAtPoint(shootSoundClip, camera.transform.position, 1f);

        GameObject clone = Instantiate(BulletPrefab, transform.position, transform.rotation);
        Rigidbody2D bulletRB = clone.GetComponent<Rigidbody2D>();
        bulletRB.velocity = transform.TransformDirection(Vector2.up * bulletSpeed);

        Bullet bullet = clone.AddComponent<Bullet>();

        yield return new WaitForSeconds(waitTime);
        canShoot = true;
    } 
}