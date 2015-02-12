using UnityEngine;
using System.Collections;
using System;

public enum SwipeGestureType {Left, Right, Up, Down, None};
public enum GestureState {Ready, InProgress, Done, Waiting, Calibrating};
public class SwipeGesture: KinectGesture
{
	protected const float EXPIRE_TIME = 0.05f;
	private SwipeSegmentStart swipeSegmentStart = new SwipeSegmentStart();
	private SwipeSegmentMiddle swipeSegmentMiddle = new SwipeSegmentMiddle();
	private SwipeSegmentEnd swipeSegmentEnd = new SwipeSegmentEnd();
	private SwipeGestureType swipeGestureType = SwipeGestureType.None;
	private GestureState swipeGestureState = GestureState.Waiting;
	private PrimaryHand primarySwipeHand = PrimaryHand.None;
	
	public SwipeGesture(KinectPointController kpc):base(kpc)
	{
		
	}
	
	public override SwipeGestureType CheckSwipe()
	{
		kpc.gameObject.name = swipeGestureState.ToString();
		switch (swipeGestureState)
		{
		case GestureState.Calibrating:
			CalibrateGesture ();
			break;
		case GestureState.Waiting:
			CheckForGestureReady();
			break;
		case GestureState.Ready:
			CheckForGestureInProgress();
			break;
		case GestureState.InProgress:
			GestureTimerTick();
			CheckForGestureDone();
			break;
		case GestureState.Done:
			ResetGesture ();
			return swipeGestureType;
		}
	
		return SwipeGestureType.None;
	}
	
	private bool IsGestureTimeOver()
	{
		if (time >= EXPIRE_TIME) 
		{
			return true;
		}
		return false;
	}

	private void GestureTimerTick()
	{
		time += Time.deltaTime;
		
		if (IsGestureTimeOver())
		{
			ResetGestureNoSwipeDetected();
		}
	}

	private void CheckForGestureReady()
	{
		primarySwipeHand = GetPrimaryHand(kpc);
		if (swipeSegmentStart.Check(kpc, primarySwipeHand))
		{
			swipeGestureType = swipeSegmentStart.GetSwipeDirection(kpc, primarySwipeHand);
			swipeGestureState = GestureState.Ready;
		}
	}

	private void CheckForGestureInProgress()
	{
		primarySwipeHand = GetPrimaryHand(kpc);
		if (primarySwipeHand == PrimaryHand.None) 
		{
			swipeGestureState = GestureState.Waiting;
		}
		else if (swipeSegmentMiddle.Check (kpc, primarySwipeHand))
		{
			swipeGestureState = GestureState.InProgress;
		}
	}
	
	private void CheckForGestureDone()
	{
		SwipeGestureType swipeEndGestureType = swipeSegmentEnd.GetSwipeDirection (kpc,primarySwipeHand);
		if (swipeSegmentEnd.Check (kpc,primarySwipeHand) && swipeEndGestureType == swipeGestureType) 
		{
			swipeGestureState = GestureState.Done;
		}
	}

	private void ResetGesture()
	{
		swipeGestureState = GestureState.Calibrating;
		time = 0;
	}

	private void ResetGestureNoSwipeDetected()
	{
		swipeGestureState = GestureState.Waiting;
		time = 0;
	}
	
	private bool IsPrimaryHandNeutralPosition()
	{
		float handRight = kpc.Hand_Right.transform.localPosition.x;
		float hipRight = kpc.Hip_Right.transform.localPosition.x;
		if (primarySwipeHand == PrimaryHand.Right)
		{
			if(handRight < hipRight)
			{
				return false;
			}
		} 
		else if (primarySwipeHand == PrimaryHand.Left) 
		{
			if(kpc.Hand_Left.transform.localPosition.x > kpc.Hip_Left.transform.localPosition.x)
			{
				return false;
			}
		}
		return true;
	}

	private void CalibrateGesture()
	{
		if(this.IsPrimaryHandNeutralPosition())
		{
			swipeGestureState = GestureState.Waiting;
		}
	}

	public PrimaryHand GetPrimaryHand(KinectPointController kpc)
	{
		ResetHandColors (kpc);

		//Needed to keep hand priority.
		switch (primarySwipeHand) 
		{
		case PrimaryHand.Left:
			if (IsHandRaised (kpc.Hand_Left, kpc)) 
			{
				kpc.Hand_Left.gameObject.renderer.material.color = Color.green;
				return PrimaryHand.Left;
			} 
			else
			{
				return PrimaryHand.None;
			}
		case PrimaryHand.Right:
			if (IsHandRaised (kpc.Hand_Right, kpc)) 
			{
				kpc.Hand_Right.gameObject.renderer.material.color = Color.green;
				return PrimaryHand.Right;
			}
			else
			{
				return PrimaryHand.None;
			}
		case PrimaryHand.None:
			if (IsHandRaised (kpc.Hand_Right, kpc)) 
			{
				kpc.Hand_Right.gameObject.renderer.material.color = Color.green;
				return PrimaryHand.Right;
			}
			else if (IsHandRaised (kpc.Hand_Left, kpc)) 
			{
				kpc.Hand_Left.gameObject.renderer.material.color = Color.green;
				return PrimaryHand.Left;
			} 
			break;
		}
		return PrimaryHand.None;
	}

	public void ResetHandColors(KinectPointController kpc)
	{
		kpc.Hand_Right.gameObject.renderer.material.color = Color.white;
		kpc.Hand_Left.gameObject.renderer.material.color = Color.white;
	}
}

public class SwipeSegmentStart: Segment
{
	public bool Check(KinectPointController kpc, PrimaryHand primaryHand)
	{
		Vector3 hipRight = kpc.Hip_Right.transform.localPosition;
		Vector3 hipLeft = kpc.Hip_Left.transform.localPosition;
		Vector3 handRight = kpc.Hand_Right.transform.localPosition;
		Vector3 handLeft = kpc.Hand_Left.transform.localPosition;

		switch (primaryHand)
		{
		case PrimaryHand.Right:
			if (handRight.x > hipRight.x)
			{
				return true;
			}
			if (handRight.x < hipLeft.x) 
			{
				return true;
			}
			break;
		case PrimaryHand.Left:
			if (handLeft.x > hipRight.x) 
			{
				return true;
			}
			if (handLeft.x < hipLeft.x) 
			{
				return true;
			}
			break;
		}
		return false;
	}
	
	public SwipeGestureType GetSwipeDirection(KinectPointController kpc, PrimaryHand primaryHand)
	{
		Vector3 hipRight = kpc.Hip_Right.transform.localPosition;
		Vector3 hipLeft = kpc.Hip_Left.transform.localPosition;
		Vector3 handRight = kpc.Hand_Right.transform.localPosition;
		Vector3 handLeft = kpc.Hand_Left.transform.localPosition;

		if(primaryHand == PrimaryHand.Right)
		{
			if (handRight.x > hipRight.x) 
			{
				return SwipeGestureType.Left;
			}
			if (handRight.x < hipRight.x) 
			{
				return SwipeGestureType.Right;
			}
		}
		if(primaryHand == PrimaryHand.Left)
		{
			if (handLeft.x > hipLeft.x) 
			{
				return SwipeGestureType.Left;
			}
			if (handLeft.x < hipLeft.x) 
			{
				return SwipeGestureType.Right;
			}
		}
		return SwipeGestureType.None;
	}
}

public class SwipeSegmentMiddle: Segment
{
	public bool Check(KinectPointController kpc, PrimaryHand primaryHand)
	{
		Vector3 handRight = kpc.Hand_Right.transform.localPosition;
		Vector3 handLeft = kpc.Hand_Left.transform.localPosition;
		Vector3 hipRight = kpc.Hip_Right.transform.localPosition;
		Vector3 hipLeft = kpc.Hip_Left.transform.localPosition;
		switch (primaryHand)
		{
		case PrimaryHand.Right:
			if (handRight.x < hipRight.x && handRight.x > hipLeft.x)
			{
				return true;
			}
			break;
		case PrimaryHand.Left:
			if (handLeft.x < hipRight.x && handLeft.x > hipLeft.x)
			{
				return true;
			}
			break;
		}
		return false;
	}
}

public class SwipeSegmentEnd: Segment
{
	private const float PRECISION = 0.50f;
	
	public bool Check(KinectPointController kpc, PrimaryHand primaryHand)
	{
		Vector3 handRight = kpc.Hand_Right.transform.localPosition;
		Vector3 handLeft = kpc.Hand_Left.transform.localPosition;
		Vector3 hipLeft = kpc.Hip_Left.transform.localPosition;
		Vector3 hipRight = kpc.Hip_Right.transform.localPosition;

		switch (primaryHand)
		{
		case PrimaryHand.Right:
			if (handRight.x < hipLeft.x)
			{
				return true;
			}
			if (handRight.x > hipRight.x)
			{
				return true;
			}
			break;
		case PrimaryHand.Left:
			if (handLeft.x < hipLeft.x)
			{
				return true;
			}
			if (handLeft.x > hipRight.x)
			{
				return true;
			}
			break;
		}
		
		return false;
	}
	
	public SwipeGestureType GetSwipeDirection(KinectPointController kpc, PrimaryHand primaryHand)
	{
		Vector3 hipRight = kpc.Hip_Right.transform.localPosition;
		Vector3 hipLeft = kpc.Hip_Left.transform.localPosition;
		Vector3 handRight = kpc.Hand_Right.transform.localPosition;
		Vector3 handLeft = kpc.Hand_Left.transform.localPosition;

		if(primaryHand == PrimaryHand.Right)
		{
			if (handRight.x < hipLeft.x) 
			{
				return SwipeGestureType.Left;
			}
			if (handRight.x > hipRight.x) 
			{
				return SwipeGestureType.Right;
			}
		}
		if(primaryHand == PrimaryHand.Left)
		{
			if (handLeft.x < hipLeft.x) 
			{
				return SwipeGestureType.Left;
			}
			if (handLeft.x > hipRight.x) 
			{
				return SwipeGestureType.Right;
			}
		}
		return SwipeGestureType.None;
	}
}

