using UnityEngine;
using System.Collections;

public class ButtonOnePlayer: Button
{
	public override void Click()
	{
		GameSettings.nbPlayer = 1;
		Application.LoadLevel("Niveau1");
	}
}
