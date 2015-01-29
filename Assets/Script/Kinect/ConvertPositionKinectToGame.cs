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

			float rightHandPercentBetweenHipsAndNeck = ((handPositionY - minKinect)) / (maxKinect - minKinect);

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
		return min;
	}

	public static float ConvertX(KinectPointController KPC, GameObject bodyPart, float min, float max)
	{
		if (KPC.sw.trackedPlayers[0] != -1)
		{
			float handPositionX = bodyPart.transform.position.x;
			float minKinect = KPC.Shoulder_Left.transform.position.x;
			float maxKinect = KPC.Shoulder_Right.transform.position.x;

			float rightHandPercentBetweenHipsAndNeck = ((handPositionX - minKinect)) / (maxKinect - minKinect);

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
		return min;
	}

}
