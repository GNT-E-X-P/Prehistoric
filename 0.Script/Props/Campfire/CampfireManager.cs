using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CampfireManager : MonoBehaviour
{
    private GameObject campfireBase;
    private GameObject campfireWood;
    private GameObject campfireFire;

    private bool active = false;

    #region Component Segments
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
    #endregion

    #region Manager Function
    public void CampfireActive(bool active)
    {
        this.active = active;

        if (active)
        {
            campfireFire.SetActive(true);
            campfireFire.SendMessage("CampfireOn");
        }
        else
        {
            campfireFire.SetActive(false);
        }
    }

    public void Upload(GameObject child)
    {
        switch (child.name)
        {
            case "CampfireBase":
                campfireBase = child;
                break;
            case "CampfireWood":
                campfireWood = child;
                break;
            case "CampfireFire":
                campfireFire = child;
                break;
            default:
                break;
        }

        if (campfireBase && campfireWood && campfireFire)
        {
            UploadEnd();
        }
    }

    private void UploadEnd()
    {
        campfireFire.SetActive(false);
        campfireBase.SetActive(true);
        campfireWood.SetActive(true);
    }
    #endregion

}






