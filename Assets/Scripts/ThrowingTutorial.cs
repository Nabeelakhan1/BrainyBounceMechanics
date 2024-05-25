using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ThrowingTutorial : MonoBehaviour
{
    [Header("References")]
    public Transform attackPoint;
    public GameObject objectToThrow;
    public float minDistanceToOpaque = 1f;
    private Vector3 initialPosition;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    [Header("UI")]
    public TextMeshProUGUI ballCounterText;

    [Header("Object Size")]
    public Vector3 objectScale = Vector3.one;

    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
        UpdateBallCounter();
        initialPosition = attackPoint.position;
    }

    private void UpdateBallCounter()
    {
        if (ballCounterText != null)
        {
            ballCounterText.text = totalThrows.ToString();
        }
    }

    private void Update()
    {
        if (!readyToThrow || totalThrows <= 0)
            return;

        float distanceTraveled = Vector3.Distance(attackPoint.position, initialPosition);
        float alpha = Mathf.Clamp01(1 - distanceTraveled / minDistanceToOpaque);

        foreach (GameObject projectile in GameObject.FindGameObjectsWithTag("Projectile"))
        {
            Material projectileMaterial = projectile.GetComponent<Renderer>().material;
            Color color = projectileMaterial.color;
            color.a = 1 - alpha;
            projectileMaterial.color = color;
        }

        if (Input.GetKeyDown(throwKey))
        {
            Throw();
        }
    }

    private void Throw()
    {
        readyToThrow = false;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));

        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, Quaternion.identity);

        // Adjust the scale of the instantiated object
        projectile.transform.localScale = objectScale;

        ProjectileAddon projectileAddon = projectile.GetComponent<ProjectileAddon>();
        if (projectileAddon != null)
        {
            projectileAddon.OnProjectileDestroyed += CancelThrowCooldown;
        }

        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        Vector3 throwDirection = (worldMousePosition - attackPoint.position).normalized;
        Vector3 forceToAdd = throwDirection * throwForce + Vector3.up * throwUpwardForce;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;
        UpdateBallCounter();
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

    private void CancelThrowCooldown()
    {
        CancelInvoke(nameof(ResetThrow));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
