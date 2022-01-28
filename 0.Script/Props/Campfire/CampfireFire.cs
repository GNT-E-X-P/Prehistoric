using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireFire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.parent.GetComponent<CampfireManager>().Upload(this.gameObject);
    }

    public void CampfireOn()
    {
        GetComponentInChildren<ParticleSystem>().Play();
    }
    public void CampfireOff()
    {

    }
}
