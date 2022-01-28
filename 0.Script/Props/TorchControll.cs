using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class TorchControll : MonoBehaviour
{
    private GameObject on;
    private GameObject off;

    private CapsuleCollider capsuleCollider;
    private MeshCollider meshCollider;

    private void Start()
    {
        off = transform.GetChild(0).gameObject;
        on = transform.GetChild(1).gameObject;

        on.SetActive(false);
        off.SetActive(true);

        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.enabled = false;

        meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = on.GetComponent<MeshFilter>().sharedMesh;
        meshCollider.enabled = true;
    }

    public void OnParticleCollision(GameObject other)
    {
        Debug.Log($"other.name = {other.name} other.tag = {other.tag}");
        if (other.tag == "CampfireFire")
        {
            SetOn();
        }
    }

    public void SetOn()
    {
        off.SetActive(false);
        on.SetActive(true);
        on.GetComponentInChildren<ParticleSystem>().Play();
    }

    #region Events
    public void OnPickUp()
    {
        capsuleCollider.enabled = true;
        meshCollider.enabled = false;
    }

    public void DetachHand()
    {
        capsuleCollider.enabled = false;
        meshCollider.enabled = true;
    }
    #endregion
}
