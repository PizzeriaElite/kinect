using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
	public int opponentId = 1;

	private void OnTriggerEnter(Collider other)
	{
		other.gameObject.SetActive(false);
		other.transform.position = Vector3.zero;
		other.rigidbody.velocity = Vector3.zero;
		other.gameObject.SetActive(true);
		
		Ball ball = other.GetComponent<Ball>();
		ball.LaunchWithDelay();
		
		Hud.instance.AddGoal(opponentId);
	}
}
