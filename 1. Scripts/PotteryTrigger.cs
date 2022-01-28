using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PotteryTrigger : MonoBehaviour
{
    [SerializeField] private Transform pottery;
    [SerializeField] private bool isHandInPottery;

    public List<GameObject> obj = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        isHandInPottery = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "VRCR" || other.name == "VRCL")
        {
            isHandInPottery = true;
        }
        if (other.tag == "apple" || other.tag == "fish" || other.tag == "etc")
        {
            for(int idx = 0; idx < obj.Count; idx++)
            {
                if (other.name == obj[idx].name)
                {
                    Debug.Log("중복");
                }
                else
                {
                    obj.Add(other.gameObject);
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.name == "VRCR" || other.name == "VRCL")
        {
            isHandInPottery = false;
        }

        if (other.name == "apple" || other.name == "fish" || other.name == "etc")
        {
            if (!(isHandInPottery)) // 토기안에 손이 없으면
            {
                Debug.Log(pottery.rotation.eulerAngles);
                if (pottery.eulerAngles.x > 45.0f || pottery.eulerAngles.x < -45.0f ||
                    pottery.eulerAngles.z > 45.0f || pottery.eulerAngles.z < -45.0f)
                {
                    Debug.Log("통과");
                }
                else
                {
                    other.transform.position = transform.position;
                }
            }
        }
    }
}
