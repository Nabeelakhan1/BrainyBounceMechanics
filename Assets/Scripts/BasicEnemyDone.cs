using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyDone : MonoBehaviour
{
    public GameObject smallPiecePrefab;
    [Header("Stats")]
    public int health;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            DestroyEnemy();
    }

public void DestroyEnemy()
{
    // Number of small pieces to instantiate
    int numPieces = 40;

    // Instantiate small pieces around the position of the enemy
    for (int i = 0; i < numPieces; i++)
    {
        Vector3 randomOffset = Random.insideUnitSphere * 2f; // Adjust the radius as needed
        GameObject smallPiece = Instantiate(smallPiecePrefab, transform.position + randomOffset, Quaternion.identity);

        // Add a Rigidbody component to the small piece if it doesn't have one already
        Rigidbody rb = smallPiece.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = smallPiece.AddComponent<Rigidbody>();
        }

        // Apply a random force to the small piece to make it explode
        rb.AddForce(Random.insideUnitSphere * 10f, ForceMode.Impulse);
    }

    // Destroy the enemy
    Destroy(gameObject);
}


}