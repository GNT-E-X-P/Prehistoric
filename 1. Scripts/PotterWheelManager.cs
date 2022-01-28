using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotterWheelManager : MonoBehaviour
{
    PotterWheelController potterwheelC;

    private int source = 0;

    enum State
    {
        PotteryOut,
        PotteryIn
    }

    State state;

    // Start is called before the first frame update
    private void Start()
    {
        potterwheelC = GetComponent<PotterWheelController>();
        this.state = State.PotteryOut;
    }

    // Update is called once per frame
    void Update()
    {
        switch (this.state)
        {
            case State.PotteryOut:

                break;
            case State.PotteryIn:
                potterwheelC.Spin();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pottery_source")
        {
            Destroy(other.gameObject);
            source++;
            if (source == 4)
            {
                potterwheelC.CreatePottery();
                this.state = State.PotteryIn;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Pottery_before")
        {
            this.state = State.PotteryOut;
        }
    }
}
