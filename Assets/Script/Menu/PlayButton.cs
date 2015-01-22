using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour
{
	public GUITexture buttonBackground;

	
	private void Update()
	{
		if (Time.timeSinceLevelLoad > 1 && Input.GetMouseButton(0) && buttonBackground.HitTest(Input.mousePosition))
		{
			Application.LoadLevel("Niveau 1");
		}
	}
}
