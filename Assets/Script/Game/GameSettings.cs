using UnityEngine;
using System.Collections;

public class GameSettings: MonoBehaviour
{
	public static int nbPlayer;

	private void OnEnable()
	{
		DontDestroyOnLoad(this);
	}
}
