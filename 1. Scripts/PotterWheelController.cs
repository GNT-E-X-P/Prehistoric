using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotterWheelController : MonoBehaviour
{
    [SerializeField] private GameObject pottery;

    // Start is called before the first frame update
    void Start()
    {

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

    public void CreatePottery()
    {
        pottery = Instantiate(pottery);
        pottery.transform.position = new Vector3(transform.position.x,
                                                 transform.position.y + 0.1f,
                                                 transform.position.z);
    }
}