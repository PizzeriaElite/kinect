using UnityEngine;
using System.Collections;
using Kinect;

public class Paddle: MonoBehaviour
{
	public KinectPointController pointController;
	public GameObject paddleControllerGameObject;
	public const int MAX_BOARD_HEIGHT = 8;
	public const int MIN_BOARD_HEIGHT = -8;
	KinectSensor kinect = null;

	private void FixedUpdate()
	{

		float handPositionY = paddleControllerGameObject.transform.localPosition.y;
		float hipsPositionY = pointController.Hip_Center.transform.localPosition.y;
		float rightShoulderPositionY = pointController.Shoulder_Right.transform.localPosition.y;
		//	Debug.Log (pointController.sw.trackedPlayers.Length.ToString());
		if (pointController.sw.trackedPlayers[0] != -1)
		{
			float rightHandPercentBetweenHipsAndNeck = ((handPositionY - hipsPositionY)) / (rightShoulderPositionY - hipsPositionY);
			float newHeight = rigidbody.transform.localPosition.z;

			if (rightHandPercentBetweenHipsAndNeck <= 0)
			{
				newHeight = MIN_BOARD_HEIGHT;
			}
			else if (rightHandPercentBetweenHipsAndNeck >= 1)
			{
				newHeight = MAX_BOARD_HEIGHT;
			}
			else
			{
				newHeight = rightHandPercentBetweenHipsAndNeck * 16 + MIN_BOARD_HEIGHT;
				//Debug.Log(rightHandPercentBetweenHipsAndNeck);
			}

			rigidbody.transform.localPosition = new Vector3(rigidbody.transform.localPosition.x, rigidbody.transform.localPosition.y, newHeight);
		}
	}
}