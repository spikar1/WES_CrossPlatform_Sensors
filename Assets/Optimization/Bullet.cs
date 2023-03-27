using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject impactParticles;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * 25;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.transform.GetComponent<Player>();
        if (player != null)
            player.TakeDamager();

        Instantiate(impactParticles, transform.position, Quaternion.LookRotation(Vector3.forward, collision.contacts[0].normal));
        if(collision.contacts[0].collider.GetComponent<Bullet>() == null)
            Destroy(gameObject);
    }
}
