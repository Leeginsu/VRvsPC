using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Rigidbody rb;

    [Tooltip("로켓 발사 스피드")]
    [Range(10f,50f)]public float rocketSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.velocity = transform.forward * rocketSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rb.transform.forward = rb.velocity.normalized;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
