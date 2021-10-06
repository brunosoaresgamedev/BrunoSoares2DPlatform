using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDagger : MonoBehaviour
{
    public GameObject bulletHitEffect;
    public float speed = 25f;
    Rigidbody2D rb;
    bool isHitting;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        StartCoroutine(selfDestroy());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        StartCoroutine(BulletHitEffect());

    }

    IEnumerator BulletHitEffect()
    {
        rb.velocity = transform.right * 0;
        rb.gravityScale = 0;
        isHitting = true;
        //  bulletHitEffect.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        isHitting = false;
        //   bulletHitEffect.SetActive(false);
        Destroy(gameObject);


    }
    IEnumerator selfDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
