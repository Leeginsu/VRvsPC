using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 10000;
    bool isHit;
    Quaternion hitRotation;
    Vector3 hitPosition;
    Transform vrPlayerPos;
    float turnSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        vrPlayerPos = GameObject.FindWithTag("VRPlayer").transform;
        transform.LookAt(vrPlayerPos);
        
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
        transform.Rotate(0, 0, 360 * Time.deltaTime);
    }

    void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            //컬러 변경
        }
        else if (other.gameObject.CompareTag("Head"))
        {
            //파티클
            Destroy(gameObject, 2f);
        }
    }
}
