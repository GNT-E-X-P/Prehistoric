using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AppleController : MonoBehaviour
{
    private List<GameObject> apples = new List<GameObject>();

    enum State
    {
        Bear,
        Drop,
        Bite,
        Eatup
    }
    State state;

    void Start()
    {
        state = State.Bear;
        transform.parent.GetComponent<TreeAppleManager>().CntApple(this.gameObject);

        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        for (int i = 0; i<transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            apples.Add(transform.GetChild(i).gameObject);
        }

        apples[0].SetActive(true);
    }

    public void Drop()
    {
        if (state == State.Bear)
        {
            state = State.Drop;

            transform.parent.GetComponent<TreeAppleManager>().DropAppel();
            transform.parent = null;

            gameObject.GetComponent<Rigidbody>().useGravity = true;
            //gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
