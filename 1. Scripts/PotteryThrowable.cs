using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]
public class PotteryThrowable : MonoBehaviour
{
    private PotteryManager potteryM;

    protected new Rigidbody rigidbody;
    protected VelocityEstimator velocityEstimator;

    public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.TurnOnKinematic | Hand.AttachmentFlags.SnapOnAttach;

    public ReleaseStyle releaseVelocityStyle = ReleaseStyle.GetFromHand;

    public UnityEvent onHoverBegin;
    public UnityEvent onHoverEnd;

    [HideInInspector]
    public Interactable interactable;

    [Tooltip("The time offset used when releasing the object with the RawFromHand option")]
    public float releaseVelocityTimeOffset = -0.011f;

    public float scaleReleaseVelocity = 1.1f;

    [Tooltip("The release velocity magnitude representing the end of the scale release velocity curve. (-1 to disable)")]
    public float scaleReleaseVelocityThreshold = -1.0f;
    [Tooltip("Use this curve to ease into the scaled release velocity based on the magnitude of the measured release velocity. This allows greater differentiation between a drop, toss, and throw.")]
    public AnimationCurve scaleReleaseVelocityCurve = AnimationCurve.EaseInOut(0.0f, 0.1f, 1.0f, 1.0f);

    protected virtual void Awake()
    {
        velocityEstimator = GetComponent<VelocityEstimator>();
        potteryM = transform.GetChild(0).GetComponent<PotteryManager>();
        interactable = GetComponent<Interactable>();

        rigidbody = GetComponent<Rigidbody>();
        rigidbody.maxAngularVelocity = 50.0f;
    }

    protected virtual void OnHandHoverBegin(Hand hand)
    {
        //GetComponent<MeshCollider>().isTrigger = true;
        onHoverBegin.Invoke();
    }

    protected virtual void HandHoverUpdate(Hand hand)
    {

        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType != GrabTypes.None && (potteryM.state == PotteryManager.State.Combed || potteryM.state == PotteryManager.State.GrabedHand))
        {
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
            potteryM.state = PotteryManager.State.GrabedHand;
        }
    }

    protected virtual void OnHandHoverEnd(Hand hand)
    {
        //GetComponent<MeshCollider>().isTrigger = false;
        onHoverEnd.Invoke();
    }

    protected virtual void OnDetachedFromHand(Hand hand)
    {
        GetComponent<Rigidbody>().isKinematic = false;

        Vector3 velocity;
        Vector3 angularVelocity;

        GetReleaseVelocities(hand, out velocity, out angularVelocity);

        rigidbody.velocity = velocity;
        rigidbody.angularVelocity = angularVelocity;
    }

    protected virtual void OnAttachedToHand(Hand hand)
    {
        hand.HoverLock(null);

        rigidbody.interpolation = RigidbodyInterpolation.None;

        if (velocityEstimator != null)
            velocityEstimator.BeginEstimatingVelocity();

    }

    protected virtual void HandAttachedUpdate(Hand hand)
    {
        if (hand.IsGrabEnding(this.gameObject))
        {
            hand.DetachObject(gameObject, false);
        }

    }

    public virtual void GetReleaseVelocities(Hand hand, out Vector3 velocity, out Vector3 angularVelocity)
    {
        if (hand.noSteamVRFallbackCamera && releaseVelocityStyle != ReleaseStyle.NoChange)
            releaseVelocityStyle = ReleaseStyle.ShortEstimation; // only type that works with fallback hand is short estimation.

        switch (releaseVelocityStyle)
        {
            case ReleaseStyle.ShortEstimation:
                if (velocityEstimator != null)
                {
                    velocityEstimator.FinishEstimatingVelocity();
                    velocity = velocityEstimator.GetVelocityEstimate();
                    angularVelocity = velocityEstimator.GetAngularVelocityEstimate();
                }
                else
                {
                    Debug.LogWarning("[SteamVR Interaction System] Throwable: No Velocity Estimator component on object but release style set to short estimation. Please add one or change the release style.");

                    velocity = rigidbody.velocity;
                    angularVelocity = rigidbody.angularVelocity;
                }
                break;
            case ReleaseStyle.AdvancedEstimation:
                hand.GetEstimatedPeakVelocities(out velocity, out angularVelocity);
                break;
            case ReleaseStyle.GetFromHand:
                velocity = hand.GetTrackedObjectVelocity(releaseVelocityTimeOffset);
                angularVelocity = hand.GetTrackedObjectAngularVelocity(releaseVelocityTimeOffset);
                break;
            default:
            case ReleaseStyle.NoChange:
                velocity = rigidbody.velocity;
                angularVelocity = rigidbody.angularVelocity;
                break;
        }

        if (releaseVelocityStyle != ReleaseStyle.NoChange)
        {
            float scaleFactor = 1.0f;
            if (scaleReleaseVelocityThreshold > 0)
            {
                scaleFactor = Mathf.Clamp01(scaleReleaseVelocityCurve.Evaluate(velocity.magnitude / scaleReleaseVelocityThreshold));
            }

            velocity *= (scaleFactor * scaleReleaseVelocity);
        }
    }
}
