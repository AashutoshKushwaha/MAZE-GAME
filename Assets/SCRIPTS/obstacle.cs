using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int damage = 20; // Damage dealt by the obstacle
    private PlayerHealth playerHealth; // Reference to the PlayerHealth script

    private void Start()
    {
        // Find the PlayerHealth script on the Slider GameObject
        GameObject healthSlider = GameObject.FindWithTag("HealthSlider");
        if (healthSlider != null)
        {
            playerHealth = healthSlider.GetComponent<PlayerHealth>();
        }
        else
        {
            Debug.LogError("Health Slider with PlayerHealth script not found!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("PlayerHealth reference is null!");
            }
        }
    }
}
