using UnityEngine;
using System;
using System.Collections;

public enum PrimaryHand {Left, Right, None};

public class KinectGesture
{
	protected KinectPointController kpc = null;
	protected float time = 0;

	public KinectGesture(KinectPointController kpc)
	{
		this.kpc = kpc;
	}

	public virtual bool Check()
	{
		return false;
	}

	public virtual SwipeGestureType CheckSwipe()
	{
		return SwipeGestureType.None;
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

	public bool IsHandRaised(GameObject handToCheck, KinectPointController kpc)
	{
		//Values used to verify if a hand is raised. A swipe will only work
		//if the dominant hand is placed between the shoulders and spine.
		//The first hand to be raised will have priority during the swipe gesture.
		Vector3 hipCenter = kpc.Hip_Center.transform.localPosition;
		Vector3 shoulderCenter = kpc.Shoulder_Center.transform.localPosition;
		Vector3 handPosition = handToCheck.transform.localPosition;
		if (handPosition.y > hipCenter.y && handPosition.y < shoulderCenter.y) 
		{
			return true;
		}
		return false;
	}
}

public interface Segment
{
	bool Check(KinectPointController kpc, PrimaryHand primaryHand = PrimaryHand.None);
}

