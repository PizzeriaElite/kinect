using UnityEngine;
using System.Collections;

public class Game: MonoBehaviour
{
	[SerializeField]
	private GameObject leftHandPlayer1 = null;
	[SerializeField]
	private GameObject rightHandPlayer1 = null;
	[SerializeField]
	private GameObject leftHandPlayer2 = null;
	[SerializeField]
	private GameObject rightHandPlayer2 = null;
	[SerializeField]
	private Paddle player1 = null;
	[SerializeField]
	private Paddle player2 = null;

	private FrontClap frontClapPlayer1;
	private FrontClap frontClapPlayer2;

	private void OnEnable()
	{
		frontClapPlayer1 = new FrontClap(player1.kpc);
		frontClapPlayer2 = new FrontClap(player2.kpc);

		if (GameSettings.nbPlayer == 1)
		{
			player2.kpc = player1.kpc;
			player1.paddleControllerGameObject = leftHandPlayer1;
			player2.paddleControllerGameObject = rightHandPlayer1;
		}
		else if (GameSettings.nbPlayer == 2)
		{
			if (GameSettings.playerOne == Side.Right)
			{
				player1.paddleControllerGameObject = rightHandPlayer1;
			}
			else
			{
				player1.paddleControllerGameObject = leftHandPlayer1;
			}

			if (GameSettings.playerTwo == Side.Right)
			{
				player2.paddleControllerGameObject = rightHandPlayer2;
			}
			else
			{
				player2.paddleControllerGameObject = leftHandPlayer2;
			}
		}
	}

	private void FixedUpdate()
	{
		if (player1.nbShrinkAttack > 0 && frontClapPlayer1.Check())
		{
			player1.nbShrinkAttack--;
			player2.Shrink();
		}

		if (player2.nbShrinkAttack > 0 && frontClapPlayer2.Check())
		{
			player2.nbShrinkAttack--;
			player1.Shrink();
		}
	}
}
