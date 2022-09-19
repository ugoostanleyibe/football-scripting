using UnityEngine;

public class EnemyX : MonoBehaviour
{
	private float speed;

	private Rigidbody enemyRb;
	private GameObject playerGoal;

	// Start is called before the first frame update
	void Start()
	{
		speed = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerX>().enemySpeed;
		playerGoal = GameObject.Find("Player Goal");
		enemyRb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		// Set enemy direction towards player goal and move there
		Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
		enemyRb.AddForce(speed * Time.deltaTime * lookDirection);
	}

	private void OnCollisionEnter(Collision other)
	{
		// If enemy collides with either goal, destroy it
		if (other.gameObject.name == "Enemy Goal" || other.gameObject.name == "Player Goal")
		{
			Destroy(gameObject);
		}
	}
}
