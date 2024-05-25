using UnityEngine;
using System;

public class ProjectileAddon : MonoBehaviour
{
    public int damage;

    private Rigidbody rb;

    private bool targetHit;

    public event Action OnProjectileDestroyed; // Add this line

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // make sure only to stick to the first target you hit
        if (targetHit)
            return;
        else
            targetHit = true;

        // check if you hit an enemy
        BasicEnemyDone enemy = collision.gameObject.GetComponent<BasicEnemyDone>();
        if (enemy != null && enemy.health > 0)
        {
            enemy.TakeDamage(damage);

            // destroy projectile
            Destroy(gameObject);

            // Trigger the event when the projectile is destroyed
            OnProjectileDestroyed?.Invoke();
        }

        // make sure projectile sticks to surface
        // rb.isKinematic = true;

        // make sure projectile moves with target
        transform.SetParent(collision.transform);
    }
}
