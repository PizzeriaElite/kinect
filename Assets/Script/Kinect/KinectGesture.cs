using UnityEngine;
using System.Collections;

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

}

public interface Segment
{
	 bool Check(KinectPointController kpc);
}

