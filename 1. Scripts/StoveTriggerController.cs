using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveTriggerController : MonoBehaviour
{
    PotteryManager potteryM;

    [SerializeField] ParticleSystem flame;
    [SerializeField] ParticleSystem smoke1;
    [SerializeField] ParticleSystem smoke2;
    [SerializeField] ParticleSystem spark;
    [SerializeField] GameObject flamelight;

    
    private int maxParticle = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pottery_before")
        {
            other.transform.parent.position = new Vector3(196.729f, 43.348f, 119.54f);
            other.transform.parent.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            other.GetComponentInParent<Rigidbody>().isKinematic = true;
            potteryM = other.GetComponent<PotteryManager>();
            potteryM.state = PotteryManager.State.InStove;
            Invoke("TurnOffFlame", 3.0f);
            test();
        }
    }

    void TurnOffFlame()
    {
        smoke1.Stop();
        smoke2.Stop();
        spark.Stop();
        flamelight.SetActive(false);
    }

    void test()
    {
        var main = flame.main;
        main.maxParticles-= 1;
        if(main.maxParticles < 10)
        {
            main.maxParticles = 0;
        }
        else
        {
            Invoke("test", 0.2f);
        }
    }
}
