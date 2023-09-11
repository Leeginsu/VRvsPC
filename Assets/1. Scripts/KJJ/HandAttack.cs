using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAttack : MonoBehaviour
{
    public Transform hand;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Ray ray = new Ray(hand.position, -hand.right);
        //RaycastHit hit;

        //bool isHit = Physics.Raycast(ray, out hit);


        //if (isHit)
        //{

        //}
        //else
        //{

        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("무언가 닿았다");
    }

    private void OnTriggerEnter(Collider other)
    {
        print("무언가 닿았다");
    }
}
