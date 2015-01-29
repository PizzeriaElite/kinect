using UnityEngine;
using System.Collections;

public class Hand: MonoBehaviour
{
	public const int MAX_BOARD_HEIGHT = 6;
	public const int MIN_BOARD_HEIGHT = -4;
	public const int MAX_BOARD_WIDTH = 9;
	public const int MIN_BOARD_WIDTH = -9;

	public KinectPointController KPC;
	public GameObject paddleControllerGameObject;

	private bool clickButton = false;

	private void OnEnable()
	{
		clickButton = false;
	}

	private void FixedUpdate()
	{

		float posXHand = ConvertPositionKinectToGame.ConvertX(KPC, paddleControllerGameObject, MIN_BOARD_WIDTH, MAX_BOARD_WIDTH);
		float posYHand = ConvertPositionKinectToGame.ConvertY(KPC, paddleControllerGameObject, MIN_BOARD_HEIGHT, MAX_BOARD_HEIGHT);

		transform.localPosition = new Vector3(posXHand, posYHand, transform.localPosition.z);
	}

	private void OnTriggerEnter(Collider other)
	{
		StartCoroutine(DelayClickButton());
	}

	private IEnumerator DelayClickButton()
	{
		yield return new WaitForSeconds(2.5f);
		clickButton = true;
	}

	private void OnTriggerStay(Collider other)
	{
		if (clickButton)
		{
			clickButton = false;
			other.GetComponent<Button>().Click();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		StopCoroutine(DelayClickButton());
	}
}
