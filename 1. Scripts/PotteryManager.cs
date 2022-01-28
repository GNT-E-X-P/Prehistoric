using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PotteryManager : MonoBehaviour
{
    PotteryController potteryC;

    public enum State
    {
        OnWheel,
        Combing,
        Combed,
        GrabedHand,
        InStove,
        Completion
    }

    public State state;

    // Start is called before the first frame update
    void Start()
    {
        potteryC = GetComponent<PotteryController>();
        this.state = State.OnWheel;
        //transform.parent.GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(state);
        switch (this.state)
        {
            case State.OnWheel:
                potteryC.Spin();
                break;
            case State.Combing:
                potteryC.Spin();
                potteryC.Scale();
                break;
            case State.Combed:
                potteryC.Spin();
                break;
            case State.GrabedHand:

                break;
            case State.InStove:
                this.state = State.Completion;
                Invoke("CallChange", 2f);
                break;
            case State.Completion:
                
                break;
        }
    }

    void CallChange()
    {
        potteryC.Change();
    }
}
