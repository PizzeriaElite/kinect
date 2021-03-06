﻿using UnityEngine;
using System.Collections;

public class GameSettings: MonoBehaviour
{
	public static int nbPlayer = 1;
	public static Side playerOne;
	public static Side playerTwo;
	public GameObject skin1;
	public GameObject skin2;
	public SkeletonWrapper sw;

	public static GameSettings instance = null;

	private void OnEnable()
	{
		DontDestroyOnLoad(this);
		if (instance == null)
		{
			instance = this;
		}
	}
}

public enum Side
{
	Left,
	Right
}
