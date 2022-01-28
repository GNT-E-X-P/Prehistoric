using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class StoneSpark : MonoBehaviour
{
    private bool grabCtrlR = false;

    public ParticleSystem effect;

    private Vector3 point1;
    private Vector3 point2;

    private MeshCollider meshCollider;
    private CapsuleCollider capsuleCollider;

    #region Component Segments
    private void Start()
    {
        meshCollider = transform.GetComponent<MeshCollider>();
        capsuleCollider = transform.GetComponent<CapsuleCollider>();

        capsuleCollider.enabled = false;
        meshCollider.enabled = true;
    }

    private void Update()
    {
        // Empty
        //체크 박스 활성화
    }
    #endregion

    #region Cal Effect Rotation
    private void OnCollisionEnter(Collision collision)
    {
        
        if(grabCtrlR)
        {
            point1 = collision.contacts[0].point;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (grabCtrlR)
        {
            point2 = collision.contacts[0].point;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (grabCtrlR && collision.gameObject.tag == "Stone")
        {
            Vector3 _dir = (point2 - point1).normalized;
            // 방향을 바라보는 Quaternion을 구한다.
            Quaternion _rot = Quaternion.LookRotation(_dir);

            Instantiate(effect, point2, _rot);
        }
    }
    #endregion

    #region Events
    public void OnPickUp(Interactable interactable)
    {
        if(interactable.attachedToHand.handType == SteamVR_Input_Sources.RightHand)
        {
            grabCtrlR = true;
        }
        meshCollider.enabled = false;
        capsuleCollider.enabled = true;
    }

    public void DetachHand()
    {
        grabCtrlR = false;
        meshCollider.enabled = true;
        capsuleCollider.enabled = false;
    }
    #endregion
}
