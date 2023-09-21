using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        int layerMaskPlayer = 1 << LayerMask.NameToLayer("Player");

        Collider[] col = Physics.OverlapSphere(transform.position, 5, layerMaskPlayer);

        for (int i = 0; i < col.Length; i++)
        {
            if (col[i] != null)
            {
                Rigidbody rb = col[i].GetComponent<Rigidbody>();
                rb.AddExplosionForce(500, transform.position, 5);
            }
        }
    }
}
