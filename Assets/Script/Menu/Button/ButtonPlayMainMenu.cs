using UnityEngine;
using System.Collections;

public class ButtonPlayMainMenu: Button
{
	public override void Click()
	{
		Application.LoadLevel("SkinSelection");
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			Application.LoadLevel("Niveau1");
		}
	}
}
