using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Rigidbody rb;

    public GameObject spark1;
    public GameObject spark2;
    public GameObject hitFX;

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
        var fx = Instantiate(hitFX, transform.position,Quaternion.identity);
        Destroy(fx, 1.5f);
        Destroy(gameObject);
        
    }
}
