using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Kinect;

public class Test: MonoBehaviour
{
	[SerializeField]
	private Text test = null;

	[SerializeField]
	private GameObject box = null;

	[SerializeField]
	private KinectPointController KPC = null;

	private float maxYGame = 32;
	private float minYGame = -32;
	private float ratioGame = 64;

	private float maxYKinect;
	private float minYKinect;
	private float ratioKinect;

	private bool isCalibrate = true;
	private int calibrateSample = 0;

	void Start()
	{
		
	}

	void Update()
	{
		if (!isCalibrate)
		{
			isCalibrate = Calibrate();
		}
		else
		{
			test.text = KPC.Hand_Right.transform.position.y.ToString();
			Move();
		}
	}

	private bool Calibrate()
	{
		if (KPC.Hand_Right.transform.position.y < KPC.Hip_Right.transform.position.y)
		{
			if (calibrateSample == 0)
			{
				test.text = "Calibration";

				maxYKinect = KPC.Head.transform.position.y;
				minYKinect = KPC.Hip_Right.transform.position.y;
				ratioKinect = maxYKinect - minYKinect;
				ratioGame = maxYGame - minYGame;
				calibrateSample++;


			}
			else if (calibrateSample <= 20)
			{
				maxYKinect = (maxYKinect + KPC.Head.transform.position.y) / 2;
				minYKinect = (minYKinect + KPC.Hip_Right.transform.position.y) / 2;
				calibrateSample++;
			}
			else
			{
				Debug.Log(maxYKinect.ToString());
				Debug.Log(minYKinect.ToString());
				return true;
			}
		}
		else
		{
			test.text = "Prener la de Position Calibration";
		}

		return false;
	}

	private void Move()
	{
		float posHand = KPC.Hand_Right.transform.position.y / (KPC.Head.transform.position.y - KPC.Hip_Right.transform.position.y) * ratioGame;
		test.text += "\n" + posHand.ToString();

		if (posHand > maxYGame)
		{
			posHand = maxYGame;
		}
		else if (posHand < minYGame)
		{
			posHand = minYGame;
		}

		box.transform.position = new Vector3(-25, posHand, 50);

	}

}
