using UnityEngine;
using System.Collections;

public class SkinSelection: MonoBehaviour
{
	public KinectPointController menuPointManControllerPlayer1;
	public KinectPointController menuPointManControllerPlayer2;

	private SwipeGesture swipePlayer1;
	private SwipeGesture swipePlayer2;

	[SerializeField]
	public GameObject[] skins;

	private GameObject[] skins1;
	private GameObject[] skins2;

	private Vector3 posDefault = new Vector3(99, 99, 99);
	private Vector3 pos1 = new Vector3(-4.5f, -1, 0);
	private Vector3 pos2 = new Vector3(4.5f, -1, 0);

	public int player1Skin = 0;
	public int player2Skin = 0;

	private void Start()
	{
		swipePlayer1 = new SwipeGesture(menuPointManControllerPlayer1);
		swipePlayer2 = new SwipeGesture(menuPointManControllerPlayer2);

		skins1 = new GameObject[skins.Length];
		skins2 = new GameObject[skins.Length];

		for (int i = 0; i < skins.Length; i++)
		{
			skins1[i] = (GameObject)Instantiate(skins[i], posDefault, Quaternion.Euler (new Vector3(0, 180,0)));
			skins2[i] = (GameObject)Instantiate(skins[i], posDefault, Quaternion.Euler (new Vector3(0, 180,0)));
		}

		skins1[0].transform.position = pos1;
		skins2[0].transform.position = pos2;
	}

	private void Update()
	{
		if (swipePlayer1.Check())
		{
			Next(ref player1Skin, skins1, pos1);
		}

		if (swipePlayer2.Check())
		{
			Next(ref player2Skin, skins2, pos2);
		}
	}

	private void Next(ref int index, GameObject[] liste, Vector3 pos)
	{
		liste[index].transform.position = posDefault;

		if (index >= (liste.Length - 1))
		{
			index = 0;
		}
		else
		{
			index++;
		}

		liste[index].transform.position = pos;
		
	}
}
