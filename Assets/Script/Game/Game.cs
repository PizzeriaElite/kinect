using UnityEngine;
using System.Collections;

public class Game: MonoBehaviour
{
	[SerializeField]
	private GameObject leftHandPlayerOne = null;
	[SerializeField]
	private GameObject rightHandPlayerOne = null;
	[SerializeField]
	private GameObject leftHandPlayerTwo = null;
	[SerializeField]
	private GameObject rightHandPlayerTwo = null;
	[SerializeField]
	private Paddle playerOne = null;
	[SerializeField]
	private Paddle playerTwo = null;

	private void OnEnable()
	{
		if (GameSettings.nbPlayer == 1)
		{
			playerOne.paddleControllerGameObject = leftHandPlayerOne;
			playerTwo.paddleControllerGameObject = rightHandPlayerOne;
		}
		else if (GameSettings.nbPlayer == 2)
		{
			if (GameSettings.playerOne == Side.Right)
			{
				playerOne.paddleControllerGameObject = rightHandPlayerOne;
			}
			else
			{
				playerOne.paddleControllerGameObject = leftHandPlayerOne;
			}

			if (GameSettings.playerTwo == Side.Right)
			{
				playerTwo.paddleControllerGameObject = rightHandPlayerTwo;
			}
			else
			{
				playerTwo.paddleControllerGameObject = leftHandPlayerTwo;
			}
		}
	}
}
