using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
	public GameObject enemyPrefab;
	public GameObject powerupPrefab;

	private readonly float spawnRangeX = 10;
	private readonly float spawnZMin = 15; // set min spawn Z
	private readonly float spawnZMax = 25; // set max spawn Z

	public float enemySpeed;
	
	public int waveCount = 1;

	public GameObject player;

	// Update is called once per frame
	void Update()
	{
		if (GameObject.FindGameObjectsWithTag("Enemy").Length < 1)
		{
			SpawnEnemyWave(waveCount);
		}
	}

	// Generate random spawn position for powerups and enemy balls
	Vector3 GenerateSpawnPosition()
	{
		float xPos = Random.Range(-spawnRangeX, spawnRangeX);
		float zPos = Random.Range(spawnZMin, spawnZMax);
		return new Vector3(xPos, 0.0f, zPos);
	}


	void SpawnEnemyWave(int enemiesToSpawn)
	{
		Vector3 powerupSpawnOffset = new(0.0f, 0.0f, -15.0f); // make powerups spawn at player end

		// If no powerups remain, spawn a powerup
		if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) // check that there are zero powerups
		{
			Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
		}

		// Spawn number of enemy balls based on wave number
		for (int i = 0; i < enemiesToSpawn; i++)
		{
			Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
		}

		ResetPlayerPosition(); // put player back at start
		enemySpeed += 20f; // increase enemy speed
		waveCount++;
	}

	// Move player back to position in front of own goal
	void ResetPlayerPosition()
	{
		player.transform.position = new(0.0f, 1.0f, -7.0f);
		player.GetComponent<Rigidbody>().velocity = Vector3.zero;
		player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
	}
}
