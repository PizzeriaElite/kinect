using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HandSelection: MonoBehaviour
{
	private const float TIME_READY_SECONDE = 6;

	[SerializeField]
	private KinectPointController playerOne = null;
	[SerializeField]
	private KinectPointController playerTwo = null;
	[SerializeField]
	private Text countDown = null;
	[SerializeField]
	private Text oneText = null;
	[SerializeField]
	private Text twoText = null;

	private bool oneReady = false;
	private bool twoReady = false;

	private bool starting = false;
	private float timer;

	private void Update()
	{
		HandSelectionUpdate();

		if (!starting)
		{
			if (oneReady && twoReady)
			{
				starting = true;
				timer = TIME_READY_SECONDE;
			}
		}
		else
		{
			if (!oneReady || !twoReady)
			{
				starting = false;
				countDown.text = "";
			}
			else
			{
				timer -= Time.deltaTime;
				countDown.text = ((int)timer).ToString();

				if (timer <= 0)
				{
					Application.LoadLevel("Niveau1");
				}
			}
		}




	}

	private IEnumerator Starting()
	{
		starting = true;
		countDown.text = "5";
		yield return new WaitForSeconds(1);
		countDown.text = "4";
		yield return new WaitForSeconds(1);
		countDown.text = "3";
		yield return new WaitForSeconds(1);
		countDown.text = "2";
		yield return new WaitForSeconds(1);
		countDown.text = "1";
		yield return new WaitForSeconds(1);

	}

	private void HandSelectionUpdate()
	{
		if (playerOne.Hand_Right.transform.position.y > playerOne.Elbow_Right.transform.position.y && playerOne.Hand_Left.transform.position.y < playerOne.Elbow_Left.transform.position.y)
		{
			GameSettings.playerOne = Side.Right;
			oneText.text = "Prêt";
			oneReady = true;
		}
		else if (playerOne.Hand_Left.transform.position.y > playerOne.Elbow_Left.transform.position.y && playerOne.Hand_Right.transform.position.y < playerOne.Elbow_Right.transform.position.y)
		{
			GameSettings.playerOne = Side.Left;
			oneText.text = "Prêt";
			oneReady = true;
		}
		else
		{
			oneText.text = "En attente";
			oneReady = false;
		}

		if (playerTwo.Hand_Right.transform.position.y > playerTwo.Elbow_Right.transform.position.y && playerTwo.Hand_Left.transform.position.y < playerTwo.Elbow_Left.transform.position.y)
		{
			GameSettings.playerTwo = Side.Right;
			twoText.text = "Prêt";
			twoReady = true;
		}
		else if (playerTwo.Hand_Left.transform.position.y > playerTwo.Elbow_Left.transform.position.y && playerTwo.Hand_Right.transform.position.y < playerTwo.Elbow_Right.transform.position.y)
		{
			GameSettings.playerTwo = Side.Left;
			twoText.text = "Prêt";
			twoReady = true;
		}
		else
		{
			twoText.text = "En attente";
			twoReady = false;
		}
	}
}
