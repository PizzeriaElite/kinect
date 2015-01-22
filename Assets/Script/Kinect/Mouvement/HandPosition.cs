using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandPosition: MonoBehaviour
{
	[SerializeField]
	private Text handPosition = null;
	[SerializeField]
	private Text handcoor = null;

	[SerializeField]
	private KinectPointController kPC = null;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		handcoor.text = kPC.Hand_Right.transform.position.x.ToString();

		if (kPC.Hand_Right.transform.position.y > kPC.Head.transform.position.y)
		{
			handPosition.text = "Haut";
		}
		else if (kPC.Hand_Right.transform.position.y < kPC.Hip_Center.transform.position.y)
		{
			handPosition.text = "Bas";
		}
		else
		{
			handPosition.text = "milieu";
		}
	}
}
