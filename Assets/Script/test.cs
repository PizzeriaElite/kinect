using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class test: MonoBehaviour
{
	FrontClap clap;

	private void OnEnable()
	{
		clap = new FrontClap(GetComponent<KinectPointController>());
	}
	void Update()
	{
		if (clap.Check())
		{
			Debug.Log("Clap");
		}
	}
}
