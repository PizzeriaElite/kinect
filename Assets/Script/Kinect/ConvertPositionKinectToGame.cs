using UnityEngine;
using System.Collections;

public class ConvertPositionKinectToGame
{
	public static float ConvertY(KinectPointController KPC, GameObject bodyPart, float min, float max)
	{
		if (KPC.sw.trackedPlayers[0] != -1)
		{
			float handPositionY = bodyPart.transform.localPosition.y;
			float minKinect = KPC.Hip_Center.transform.localPosition.y;
			float maxKinect = KPC.Shoulder_Center.transform.localPosition.y;

			return Convert(handPositionY, minKinect, maxKinect, min, max);

		}
		return min;
	}

	public static float ConvertX(KinectPointController KPC, GameObject bodyPart, float min, float max)
	{
		if (KPC.sw.trackedPlayers[0] != -1)
		{
			return Convert(bodyPart.transform.position.x, KPC.Shoulder_Left.transform.position.x, KPC.Shoulder_Right.transform.position.x, min, max);
		}
		return min;
	}

	private static float Convert(float handPosition, float minKinect, float maxKinect, float min, float max)
	{
		float rightHandPercentBetweenHipsAndNeck = 0;
		float diffKinect = (maxKinect - minKinect);

		if (diffKinect != 0)
		{
			rightHandPercentBetweenHipsAndNeck = ((handPosition - minKinect)) / diffKinect;
		}

		if (rightHandPercentBetweenHipsAndNeck <= 0)
		{
			return min;
		}
		else if (rightHandPercentBetweenHipsAndNeck >= 1)
		{
			return max;
		}
		else
		{
			return rightHandPercentBetweenHipsAndNeck * (max - min) + min;
		}
	}

}
