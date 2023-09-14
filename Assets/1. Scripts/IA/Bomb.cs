using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Bomb : MonoBehaviourPun
{
    Rigidbody rb;
    float speed = 50;
    bool isHit;
    Transform vrPlayerPos;

    //public GameObject smokeFX;
    public GameObject hitFX;
    //float turnSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(GameObject.FindWithTag("VRPlayer") == null)
        {
            vrPlayerPos = GameObject.Find("VRPlayerPos").transform;
        }
        else
        {
            vrPlayerPos = GameObject.FindWithTag("Head").transform;
        }
     
        transform.LookAt(vrPlayerPos);

        //박스 소멸 시간
        Destroy(gameObject, 10f);
    }





    void Update()
    {
        Turn();
        Move();

    }
    void Turn()
    {
        //var pos = vrPlayerPos.position - transform.position;
        //var rotation = Quaternion.LookRotation(pos);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);

        //transform.Rotate(0, 0, 360 * Time.deltaTime);
    }

    void Move()
    {
        print("Move");
        //smokeFX.SetActive(true);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    [PunRPC]
    void UpdatePosRpc()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //var fx = Instantiate(hitFX, transform.position, Quaternion.identity);
        if (collision.gameObject.CompareTag("Head"))
        {
            Destroy(gameObject);
            ScoreManager.instance.PCSCORE += 1;
        }

        hitFX.SetActive(true);
        //Destroy(fx, 1.5f);

    }
    private void OnTriggerEnter(Collider other)
    {
        //var fx = Instantiate(hitFX, transform.position, Quaternion.identity);
        //Destroy(fx, 1.5f);
        //Destroy(gameObject);
        //if (other.gameObject.CompareTag("Hand"))
        //{
        //    //컬러 변경
        //}
        //else if (other.gameObject.CompareTag("Head"))
        //{
        //    //파티클
        //    //var fx = Instantiate(hitFX, transform.position, Quaternion.identity);
        //    //Destroy(fx, 1.5f);
        //    //Destroy(gameObject);
        //}
    }
}
