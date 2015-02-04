using UnityEngine;
using System.Collections;
using Kinect;

public class Paddle: MonoBehaviour
{
	public KinectPointController kpc;
	[HideInInspector]
	public GameObject paddleControllerGameObject;
	public const int MAX_BOARD_HEIGHT = 8;
	public const int MIN_BOARD_HEIGHT = -8;

	public int nbShrinkAttack = 1;

	private void FixedUpdate()
	{
		float posZHand = ConvertPositionKinectToGame.ConvertY(kpc, paddleControllerGameObject, MIN_BOARD_HEIGHT, MAX_BOARD_HEIGHT);

		rigidbody.transform.localPosition = new Vector3(rigidbody.transform.localPosition.x, rigidbody.transform.localPosition.y, posZHand);
	}

	public void Shrink()
	{
		transform.localScale = new Vector3(1, 1, 1);
		StartCoroutine(Expand());
	}

	private IEnumerator Expand()
	{
		yield return new WaitForSeconds(5);
		transform.localScale = new Vector3(3, 1, 1);
	}

}