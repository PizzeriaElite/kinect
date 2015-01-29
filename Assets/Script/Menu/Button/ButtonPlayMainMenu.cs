using UnityEngine;
using System.Collections;

public class ButtonPlayMainMenu: Button
{
	public override void Click()
	{
		Application.LoadLevel("NbPlayer");
	}
}
