#define DEBUG

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TreeAngleCal : MonoBehaviour
{
    private bool TEST = false;

    [SerializeField]
    private GameObject pos;
    private TreeAppleManager appleManager;

    #region Variables 
    private GameObject obj_ctrlL;
    private GameObject obj_ctrlR;
    private Vector3 catchPos;
    private Vector3 fixPos;

    private bool isRunCalPos;

    private Vector3 treeFirstPos;

    private readonly float radius = 2.0f;
    #endregion

    #region Component Segments
    private void Start()
    {
        treeFirstPos = transform.position;
        catchPos = treeFirstPos;

        fixPos = new Vector3(0.05f, 0f, 0f);
        catchPos += fixPos;

        appleManager = GetComponent<TreeAppleManager>();

        if (pos) pos.SetActive(false);

        if (TEST)
        {
            pos.SetActive(true);
            pos.transform.position = catchPos;
        }
    }

    void Update()
    {
        if (obj_ctrlL && obj_ctrlR)
        {
            float x = 90.0f - GetDegree_x(treeFirstPos, catchPos);
            float z = GetDegree_z(treeFirstPos, catchPos) - 90.0f;
            Vector3 angle = new Vector3(x, 0, z);


            if (angle.z > radius)
            {
                angle.z = radius;
            }
            if (angle.z < -radius)
            {
                angle.z = -radius;
            }

            if (angle.x > radius)
            {
                angle.x = radius;
            }
            if (angle.x < -radius)
            {
                angle.x = -radius;
            }

            transform.eulerAngles = angle;
            appleManager.StartDrop();
        }
        else
        {
            appleManager.StopDrop();
        }
    }
    #endregion Component Segments

    #region Cal Angle fun
    float GetDegree_x(Vector3 _from, Vector3 _to)
    {
        return Mathf.Atan2(_to.y - _from.y, _to.z - _from.z) * 180 / Mathf.PI;
    }

    float GetDegree_z(Vector3 _from, Vector3 _to)
    {
        return Mathf.Atan2(_to.y - _from.y, _to.x - _from.x) * 180 / Mathf.PI;
    }
    #endregion

    IEnumerator CalPos()
    {
        isRunCalPos = true;

        DLog("TreeAngle, StartCoroutine(CalPose());");

        Vector3 temp = (obj_ctrlL.transform.position + obj_ctrlR.transform.position) / 2;
        temp += fixPos;
        catchPos.y = temp.y;

        if (TEST) pos.transform.position = catchPos;

        Vector3 beforeCtrlPos = temp;
        Vector3 afterCtrlPos;

        while (obj_ctrlL && obj_ctrlR)
        {
            yield return null;

            temp = (obj_ctrlL.transform.position + obj_ctrlR.transform.position) / 2;
            afterCtrlPos = temp;

            catchPos += (afterCtrlPos - beforeCtrlPos);
            if (TEST) pos.transform.position = catchPos;

            beforeCtrlPos = afterCtrlPos;
        }
    }

    public void SetCtrl(Hand hand)
    {
        if (hand.handType == SteamVR_Input_Sources.LeftHand)
        {
            DLog($"TreeAngle, SetCtrl hand.handType => {hand.handType}");
            obj_ctrlL = hand.gameObject;
        }
        else
        {
            DLog($"TreeAngle, SetCtrl hand.handType => {hand.handType}");
            obj_ctrlR = hand.gameObject;
        }

        if (!isRunCalPos && obj_ctrlL && obj_ctrlR)
        {
            StartCoroutine(CalPos());
        }
    }

    public void DelCtrl(Hand hand)
    {
        if (hand.handType == SteamVR_Input_Sources.LeftHand)
        {
            DLog($"TreeAngle, DelCtrl hand.handType => {hand.handType}");
            obj_ctrlL = null;
        }
        else
        {
            DLog($"TreeAngle, DelCtrl hand.handType => {hand.handType}");
            obj_ctrlR = null;
        }

        if (isRunCalPos)
        {
            StopCoroutine(CalPos());
            isRunCalPos = false;

            transform.rotation = Quaternion.Euler(Vector3.zero);
            catchPos = treeFirstPos;

            if (TEST) pos.transform.position = catchPos;

            DLog($"TreeAngle, StopCoroutine(CalPos());");
        }
    }

    [Conditional("DEBUG")]
    void DLog(string str)
    {
        UnityEngine.Debug.Log(str);
    }
}