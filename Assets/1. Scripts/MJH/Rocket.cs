using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rocket : MonoBehaviourPun
{
    public Rigidbody rb;

    public GameObject spark1;
    public GameObject spark2;
    public GameObject hitFX;



    [Tooltip("로켓 발사 스피드")]
    [Range(10f,50f)]public float rocketSpeed = 50f;
    public float rocketPower = 50f;
    public float rocketUp = 0.5f;

    bool useGravity;
    public float gravity = -9.81f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        
        //GetComponent<ConstantForce>().force = new Vector3(0, -9.8f, 0);
        //Physics.gravity = new Vector3(0,-9.8f,0);
        //rb.velocity = transform.forward * rocketSpeed;
    }

    

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.transform.forward = rb.velocity.normalized;
        if (useGravity)
        {
            rb.velocity += new Vector3(0, gravity * Time.fixedDeltaTime, 0);
        }

        
    }

    [PunRPC]
    void InstantiateRpc(Transform dir)
    {
        transform.parent = dir;
        //rb.useGravity = isBool;
    }

    [PunRPC]
    void RocketTest(Vector3 dir, bool isBool)
    {
        //rocket = Instantiate(rocketBullet, firePos.position, transform.rotation);
        transform.forward = dir + Vector3.up * rocketUp;
        useGravity = isBool;
        rb.velocity = transform.forward * rocketPower;
        spark1.SetActive(true);
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        var fx = Instantiate(hitFX, transform.position,Quaternion.identity);
        Destroy(fx, 1.5f);

        if(collision.gameObject.layer == LayerMask.NameToLayer("Head"))
        {
            ScoreManager.instance.PCSCORE += 1;
        }
        Destroy(gameObject);
        
    }



}
