using System.Collections;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
	private readonly float boost = 500.0f;
	private readonly float speed = 750.0f;
	
	private Rigidbody playerRb;
	private GameObject focalPoint;

	public ParticleSystem smokeParticle;
	public GameObject powerupIndicator;
	public int powerupDuration = 5;
	
	public bool hasPowerup;

	private readonly float powerupStrength = 25.0f; // how hard to hit enemy with powerup
	private readonly float normalStrength = 10.0f; // how hard to hit enemy normally

	void Start()
	{
		focalPoint = GameObject.Find("Focal Point");
		playerRb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		// Add force to player in direction of the focal point (and camera)
		float verticalInput = Input.GetAxis("Vertical");
		playerRb.AddForce(speed * Time.deltaTime * verticalInput * focalPoint.transform.forward);

		// Set powerup indicator position to beneath player
		powerupIndicator.transform.position = transform.position + new Vector3(0.0f, -0.6f, 0.0f);

		// Add impulse force if space bar is pressed
		if (Input.GetKeyDown(KeyCode.Space))
		{
			playerRb.AddForce(boost * Time.deltaTime * focalPoint.transform.forward, ForceMode.Impulse);
			smokeParticle.Play();
		}
	}

	// Coroutine to count down powerup duration
	private IEnumerator PowerupCooldown()
	{
		yield return new WaitForSeconds(powerupDuration);
		powerupIndicator.SetActive(false);
		hasPowerup = false;
	}

	// If Player collides with powerup, activate powerup
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Powerup"))
		{
			StartCoroutine(PowerupCooldown());
			powerupIndicator.SetActive(true);
			Destroy(other.gameObject);
			hasPowerup = true;
		}
	}

	// If Player collides with enemy
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
			Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;

			if (hasPowerup) // if have powerup hit enemy with powerup force
			{
				enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
			}
			else // if no powerup, hit enemy with normal strength 
			{
				enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
			}
		}
	}
}
