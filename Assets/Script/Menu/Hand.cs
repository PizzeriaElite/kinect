using UnityEngine;
using System.Collections;

public class Hand: MonoBehaviour
{
	private const float CLICKING_TIME = 2.5f;
	private const int MAX_BOARD_HEIGHT = 6;
	private const int MIN_BOARD_HEIGHT = -4;
	private const int MAX_BOARD_WIDTH = 9;
	private const int MIN_BOARD_WIDTH = -9;

	public KinectPointController KPC;
	public GameObject paddleControllerGameObject;

	private bool clickingButton = false;
	private float timer = 0;
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

	private void Update()
	{
		if (clickingButton)
		{
			timer -= Time.deltaTime;

			if (timer <= 0)
			{
				clickButton = true;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Button")
		{
			clickingButton = true;
			timer = CLICKING_TIME;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Button" && clickButton)
		{
			clickButton = false;
			other.GetComponent<Button>().Click();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Button")
		{
			clickingButton = false;
		}
	}
}
