using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.UIElements;
using UnityEngine;

public class PotteryController : MonoBehaviour
{
    PotteryManager potteryM;

    public GameObject potteryAfter;
    private int cnt;
    private bool combCheck;


    //// Start is called before the first frame update
    void Start()
    {
        potteryM = GetComponentInParent<PotteryManager>();
        cnt = 0;
        combCheck = false;
    }

    //// Update is called once per frame
    void Update()
    {
        if (cnt == 2 && combCheck == false)
        {
            if (transform.localScale.y < (transform.localScale.x * 3))
            {
                potteryM.state = PotteryManager.State.Combing;
            }
            else
            {
                potteryM.state = PotteryManager.State.Combed;
                combCheck = true;
            }
        }
        else if(combCheck == false)
        {
            potteryM.state = PotteryManager.State.OnWheel;
        }
    }

    public void Spin()
    {
        transform.Rotate(0, 1.0f, 0, Space.World);  // 월드 좌표 Y 축을 기준으로 회전
        if (transform.localRotation.y > 359.99f)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,
                                                  transform.rotation.y % 360.0f,
                                                  transform.rotation.z);
        }
    }

    public void Scale()
    {
        if (transform.localScale.y < (transform.localScale.x * 3))
        {
            transform.localScale += new Vector3(0, 0.01f, 0); // 객체의 스케일을 0.0025f씩 증가 (0, 0.0025f, 0)
        }
    }

    public void Change()
    {
        gameObject.SetActive(false);
        potteryAfter = Instantiate(potteryAfter);
        potteryAfter.transform.position = transform.position;
        Destroy(transform.parent.gameObject);
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    switch (other.tag)
    //    {
    //        case "Stove":
    //            potteryM.state = PotteryManager.State.InStove;
    //            break;
    //    }
    //}

    public void OnHoverBegin()
    {
        GetComponent<MeshCollider>().isTrigger = true;
        Debug.Log("OnHoverBegin");
        cnt++;
    }
    public void OnHoverEnd()
    {
        GetComponent<MeshCollider>().isTrigger = false;
        Debug.Log("OnHoverEnd");
        cnt--;
    }

}
