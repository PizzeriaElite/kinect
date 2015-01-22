using UnityEngine;
using System.Collections;

public class End : MonoBehaviour
{
	private void Update () 
	{
		if (Time.timeSinceLevelLoad > 2 && (Input.GetMouseButton(0) || Input.anyKey))
		{
			Application.LoadLevel("Menu");
		}
	}
}
