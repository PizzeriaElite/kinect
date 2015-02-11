using UnityEngine;
using System.Collections;

public class ButtonSkinSelection: Button
{
	public override void Click()
	{
		Application.LoadLevel("SkinSelection");
	}
}