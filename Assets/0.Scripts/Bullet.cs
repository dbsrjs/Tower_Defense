using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;

    private Transform target;

    private float time = 0f;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= 3f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        Destroy(gameObject);
    }
}