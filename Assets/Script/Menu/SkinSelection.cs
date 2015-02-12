using UnityEngine;
using System.Collections;

public class SkinSelection: MonoBehaviour
{
	[SerializeField]
	public GameObject[] skins;

	private GameObject[] skins1;
	private GameObject[] skins2;

	private Vector3 posDefault = new Vector3(99, 99, 99);
	private Vector3 pos1 = new Vector3(-35, -15, 50);
	private Vector3 pos2 = new Vector3(35, -15, 50);

	public int player1Skin = 0;
	public int player2Skin = 0;

	private void Start()
	{
		skins1 = new GameObject[skins.Length];
		skins2 = new GameObject[skins.Length];

		for (int i = 0; i < skins.Length; i++)
		{
			skins1[i] = (GameObject)Instantiate(skins[i], posDefault, Quaternion.identity);
			skins2[i] = (GameObject)Instantiate(skins[i], posDefault, Quaternion.identity);
		}

		skins1[0].transform.position = pos1;
		skins2[0].transform.position = pos2;
	}

	private void Update()
	{
		//if (true)
		//{
		//	Next(ref player1Skin, skins1, pos1);
		//}

		//if (true)
		//{
		//	Next(ref player2Skin, skins2, pos2);
		//}
	}

	private void Next(ref int index, GameObject[] liste, Vector3 pos)
	{
		liste[index].transform.position = posDefault;

		if (index >= liste.Length)
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
