using UnityEngine;
using System.Collections;
using System;

public class FrontClap: KinectGesture
{
	private FCSegment1A seg1A = new FCSegment1A();
	private FCSegment1B seg1B = new FCSegment1B();
	private FCSegment2 seg2 = new FCSegment2();

	private bool segment1 = false;

	public FrontClap(KinectPointController kpc):base(kpc)
	{

	}

	public override bool Check()
	{
		if (segment1)
		{
			if (seg2.Check(kpc))
			{
				Reset();
				Debug.Log("Success");
				return true;
			}
			else
			{
				time += Time.deltaTime;
				if (time >= EXPIRE_TIME)
				{
					Debug.Log("Reset");
					Reset();
				}
			}
		}
		else if (seg1A.Check(kpc) || seg1B.Check(kpc))
		{
			segment1 = true;
		}

		return false;
	}

	private void Reset()
	{
		segment1 = false;
		time = 0;
	}
}

public class FCSegment1A: Segment
{
	public bool Check(KinectPointController kpc)
	{
		Vector3 shoulder = kpc.Shoulder_Center.transform.position;
		Vector3 handLeft = kpc.Hand_Left.transform.position;
		Vector3 handRight = kpc.Hand_Right.transform.position;

		if (handLeft.z > shoulder.z && handRight.z > shoulder.z)
		{
			float halfChest = kpc.Spine.transform.position.y + ((shoulder.y - kpc.Spine.transform.position.y) / 2);
			if (handRight.y > shoulder.y && handLeft.y < halfChest)
			{
				return true;
			}
		}
		return false;
	}
}

public class FCSegment1B: Segment
{
	public bool Check(KinectPointController kpc)
	{
		Vector3 shoulder = kpc.Shoulder_Center.transform.position;
		Vector3 handLeft = kpc.Hand_Left.transform.position;
		Vector3 handRight = kpc.Hand_Right.transform.position;

		if (handLeft.z < shoulder.z && handRight.z < shoulder.z)
		{
			float halfChest = kpc.Spine.transform.position.y + ((shoulder.y - kpc.Spine.transform.position.y) / 2);
			if (handLeft.y > shoulder.y && handRight.y < halfChest)
			{
				return true;
			}
		}
		return false;
	}
}

public class FCSegment2: Segment
{
	private const float PRECISION = 0.15f;

	public bool Check(KinectPointController kpc)
	{
		Vector3 handLeft = kpc.Hand_Left.transform.position;
		Vector3 handRight = kpc.Hand_Right.transform.position;

		if (EqualWithMargin(handLeft.x, handRight.x, PRECISION) &&
			EqualWithMargin(handLeft.y, handRight.y, PRECISION) &&
			EqualWithMargin(handLeft.z, handRight.z, PRECISION))
		{
			return true;
		}

		return false;
	}

	private bool EqualWithMargin(float f1, float f2, float margin)
	{
		float diff = Math.Abs(f1 - f2);

		if (diff < margin)
		{
			return true;
		}

		return false;
	}
}

