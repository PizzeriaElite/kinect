using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour
{
	public enum InputAxis {VerticalPlayer1 = 0, VerticalPlayer2 = 1};
	
	[SerializeField] private float speed = 25;
	public InputAxis inputAxis = InputAxis.VerticalPlayer1;
	
	private void FixedUpdate()
	{
		rigidbody.velocity = transform.right * Input.GetAxis("Vertical Player " + (int)inputAxis) * speed;
	}
}
