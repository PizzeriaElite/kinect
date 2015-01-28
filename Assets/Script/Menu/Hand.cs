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
	private bool wasOnScreen = false;

	private void FixedUpdate()
	{

		float posXHand = ConvertPositionKinectToGame.ConvertX(KPC, paddleControllerGameObject, MIN_BOARD_WIDTH, MAX_BOARD_WIDTH);
		float posYHand = ConvertPositionKinectToGame.ConvertY(KPC, paddleControllerGameObject, MIN_BOARD_HEIGHT, MAX_BOARD_HEIGHT);

		transform.localPosition = new Vector3(posXHand, posYHand, transform.localPosition.z);
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Enter");
		StartCoroutine(DelayClickButton());
	}

	private IEnumerator DelayClickButton()
	{
		Debug.Log("Coroutine");
		yield return new WaitForSeconds(2.5f);
		clickButton = true;
	}

	private void OnTriggerStay(Collider other)
	{
		if (clickButton)
		{
			other.GetComponent<Button>().Click();
			clickButton = false;
		}

	}

	private void OnTriggerExit(Collider other)
	{
		StopCoroutine(DelayClickButton());
	}
}
