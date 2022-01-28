using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.parent.GetComponent<AppleController>().UploadApple(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Red_Apple : OnTriggerEnter");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Red_Apple : OnCollisionEnter");
    }
}
