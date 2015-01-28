using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class test: MonoBehaviour
{
	public Text textTest;
	public GameObject bodyPart;


	void Update()
	{
		textTest.text = bodyPart.transform.position.x.ToString();
	}
}
