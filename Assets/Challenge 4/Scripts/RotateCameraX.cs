using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
	private readonly float speed = 200.0f;
	public GameObject player;

	// Update is called once per frame
	void Update()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		transform.Rotate(Vector3.up, horizontalInput * speed * Time.deltaTime);
		transform.position = player.transform.position; // Move focal point with player
	}
}
