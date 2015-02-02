using UnityEngine;
using System.Collections;

public class Ball: MonoBehaviour
{
	public float startForce = 200;
	public float launchDelay = 2;

	private void Start()
	{
		LaunchWithDelay();
	}

	public void Launch()
	{
		float angleAmplitude = Random.Range(10f, 30f);
		int player = Random.Range(0, 2);
		int direction = Random.Range(0, 2);

		float angle = (angleAmplitude + (player * 180)) + ((180 - angleAmplitude * 2) * direction);

		transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));

		rigidbody.AddForce(transform.forward * startForce, ForceMode.Impulse);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Player")
		{
			rigidbody.AddForce(Vector3.forward * 50, ForceMode.Impulse);
		}
	}

	private void Update()
	{
		if (transform.eulerAngles.y <= 90 && transform.eulerAngles.y > 45)
		{
			transform.eulerAngles = new Vector3(0, 45, 0);
		}
		else if (transform.eulerAngles.y < 135 && transform.eulerAngles.y > 90)
		{
			transform.eulerAngles = new Vector3(0, 135, 0);
		}
		else if (transform.eulerAngles.y <= 270 && transform.eulerAngles.y > 225)
		{
			transform.eulerAngles = new Vector3(0,225, 0);
		}
		else if (transform.eulerAngles.y <= 315 && transform.eulerAngles.y > 270)
		{
			transform.eulerAngles = new Vector3(0, 315, 0);
		}
	}

	public void LaunchWithDelay()
	{
		Invoke("Launch", launchDelay);
	}
}
