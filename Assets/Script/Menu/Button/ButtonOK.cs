using UnityEngine;
using System.Collections;

public class ButtonOK: Button
{
	[SerializeField]
	private SkinSelection skinSelection = null;

	public override void Click()
	{
		GameSettings.instance.skin1 = skinSelection.skins[skinSelection.player1Skin];
		GameSettings.instance.skin2 = skinSelection.skins[skinSelection.player2Skin];
		Application.LoadLevel("NbPlayer");
	}
}