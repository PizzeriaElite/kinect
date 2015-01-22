using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour 
{
	public int scoreMaximum = 10;
	public int[] scorePlayer1;
	[HideInInspector] public int lastActivePlayer = 1;
	
	private void Start()
	{
	
		DontDestroyOnLoad(gameObject);
	}
}
