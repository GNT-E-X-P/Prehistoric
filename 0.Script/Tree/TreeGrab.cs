using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(TreeAngleCal))]
public class TreeGrab : MonoBehaviour
{
	#region Variables
	protected Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.SnapOnAttach;
	protected Interactable interactable;

	private TreeAngleCal angleCal;

	//public UnityEvent onPickUp;
	//public UnityEvent onDetachFromHand;
	#endregion

	#region Component Segments
	void Awake()
	{
		interactable = GetComponent<Interactable>();
		angleCal = GetComponent<TreeAngleCal>();
	}
    #endregion

    #region Hand Action
    // hand.Update에서 SendMessage
    protected virtual void HandHoverUpdate(Hand hand)
	{
		GrabTypes startingGrabType = hand.GetGrabStarting();

		if (startingGrabType != GrabTypes.None)
		{
			hand.AttachObject(gameObject, startingGrabType, attachmentFlags, null);
		}
	}

	// hand.Update에서 SendMessage
	protected virtual void HandAttachedUpdate(Hand hand)
	{
		if (hand.IsGrabEnding(this.gameObject))
		{
			hand.DetachObject(gameObject);
		}
	}
	#endregion

	#region Grab Events
	protected virtual void OnAttachedToHand(Hand hand)
	{
		angleCal.SetCtrl(hand);
		//onPickUp.Invoke();

		//hand.HoverLock(null);

		//attachPosition = transform.position;
		//attachRotation = transform.rotation;

	}

	protected virtual void OnDetachedFromHand(Hand hand)
	{
		angleCal.DelCtrl(hand);
		//onDetachFromHand.Invoke();

		hand.HoverUnlock(null);
	}
	#endregion
}


