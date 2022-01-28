using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireBase : MonoBehaviour
{
    private bool active = false;
    private bool message = false;

    public int cnt = 0;
    
    private void Start()
    {
        transform.parent.GetComponent<CampfireManager>().Upload(this.gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!message)
        {
            cnt++;
            Debug.Log(cnt);
            if (cnt > 200)
            {
                active = true;
                message = true;
                transform.parent.GetComponent<CampfireManager>().CampfireActive(this.active);
            }
        }
    }
}

