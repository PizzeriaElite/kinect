using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour 
{
	public static Hud instance;
	
	public GUIText[] scoresText;
	
	private void Awake()
	{
		instance = this;
	}
	
	public void AddGoal(int playerId)
	{
		scoresText[playerId].text = (int.Parse(scoresText[playerId].text) + 1).ToString();
		
		if (int.Parse(scoresText[playerId].text) == 2)
		{
			//Application.LoadLevel("Fin" + playerId);
		}
	}
}
