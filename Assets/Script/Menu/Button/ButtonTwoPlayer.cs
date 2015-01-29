using UnityEngine;
using System.Collections;

public class ButtonTwoPlayer: Button
{
	public override void Click()
	{
		GameSettings.nbPlayer = 2;
		Application.LoadLevel("HandSelection");
	}
}
