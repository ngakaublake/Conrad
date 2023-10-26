using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private void Start()
    {
        //Sets a Bullet Lifetime of 3 seconds.
        StartCoroutine(DestroyAfterDelay(3f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        //Deletes object after 'delay' seconds.
        //Used to delete BulletMask as it doesn't collide
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroys bullet on collision/
        Destroy(gameObject);
    }
}
