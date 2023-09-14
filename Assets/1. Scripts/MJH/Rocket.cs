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
    public float rocketPower = 7.5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        //rb.velocity = transform.forward * rocketSpeed;
    }

    

    // Update is called once per frame
    void Update()
    {
        //rb.transform.forward = rb.velocity.normalized;


        
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
        transform.forward = dir + Vector3.up * rocketPower;
        rb.useGravity = isBool;
        rb.velocity = transform.forward * 50f;
        spark1.SetActive(true);
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        var fx = Instantiate(hitFX, transform.position,Quaternion.identity);
        Destroy(fx, 1.5f);

        if(collision.gameObject.tag == "Head")
        {
            ScoreManager.instance.PCSCORE += 1;
        }
        Destroy(gameObject);
        
    }



}
