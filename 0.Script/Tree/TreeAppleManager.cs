//#define DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class TreeAppleManager : MonoBehaviour
{
    
    
    [SerializeField]
    private List<AppleController> appleCtrl = new List<AppleController>();

    private int appleCnt;
    private bool runRoutine = false;

    public void CntApple(GameObject apple)
    {
        appleCnt++;
        appleCtrl.Add(apple.GetComponent<AppleController>());
    }

    public void DropAppel()
    {
        DLog("AppleManager : DropAppel");
        appleCnt--;
    }

    public void StartDrop()
    {
        if (!runRoutine && appleCnt != 0)
        {
            DLog("AppleManager : StartDrop");
            StartCoroutine("Routine");
        }
    }

    public void StopDrop()
    {
        if (runRoutine)
        {
            DLog("AppleManager : StopDrop");
            StopCoroutine("Routine");
            runRoutine = false;
        }
    }

    IEnumerator Routine()
    {
        DLog("AppleManager : StartCoroutine");
        runRoutine = true;

        int n = appleCtrl.Count;

        while(appleCnt != 0)
        {
            yield return new WaitForSeconds(0.5f);
            appleCtrl[Random.Range(0, n)].Drop();
        }
    }

    private void Update()
    {   }

    [Conditional("DEBUG")]
    void DLog(string str)
    {
        UnityEngine.Debug.Log(str);
    }
}
