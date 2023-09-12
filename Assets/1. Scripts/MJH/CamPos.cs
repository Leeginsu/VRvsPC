using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPos : MonoBehaviour
{
    GameObject mainCam;
    public GameObject rocketBullet;
    public Transform firePos;
    public GameObject ch;

    // Start is called before the first frame update
    void Start()
    {
        ch.SetActive(false);
        mainCam = Camera.main.gameObject;
        mainCam.transform.eulerAngles = new Vector3(5, 0, 0);
    }


    float rx, ry;
    float rotSpeed = 220f;
    bool attackMode = false;
    GameObject rocket;
    Vector3 dir;
    // Update is called once per frame
    void Update()
    {
        // 로켓 카운트가 있을 때

        if (Input.GetButtonDown("Fire1"))
        {
            //ry = -mainCam.transform.eulerAngles.x;
            //rx = mainCam.transform.eulerAngles.y;
            attackMode = true;
            ch.SetActive(true);

            //로켓을 생성
            rocket = Instantiate(rocketBullet);
            rocket.transform.position = firePos.position;
            rocket.transform.parent = firePos;
            rocket.GetComponent<Rigidbody>().useGravity = false;
            //Vector3 dir = firePos.forward;
            //rocket.transform.forward = dir + Vector3.up * 0.5f;
        }
        else if (Input.GetButton("Fire1"))
        {
            //rocket.transform.eulerAngles = transform.eulerAngles;

             dir = transform.forward;
            
        }
        else if (Input.GetButtonUp("Fire1"))
        {

            attackMode = false;
            ch.SetActive(false);
            //로켓 발사
            rocket.transform.forward = dir + Vector3.up * 0.5f;
            rocket.GetComponent<Rigidbody>().useGravity = true;
            rocket.GetComponent<Rocket>().rb.velocity = rocket.transform.forward * 50f;
            rocket.GetComponent<Rocket>().spark1.SetActive(true);
            //RocketFire();

        }
        NormalCam();

        //if (attackMode == false)
        //{
        //    NormalCam();
        //}
        //else
        //{

        //   // AttackCam();
        //}
    }



    void NormalCam()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        rx += mx * rotSpeed * Time.deltaTime;
        ry += my * rotSpeed * Time.deltaTime;

        //양옆
        //rx = Mathf.Clamp(rx, -45f, 45f);

        //위아래
        //ry = Mathf.Clamp(ry, -25f, 20f);

        transform.localEulerAngles = new Vector3(-ry, rx, 0);
    }

    void AttackCam()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        rx += mx * rotSpeed * Time.deltaTime;
        ry += my * rotSpeed * Time.deltaTime;

        //rx = Mathf.Clamp(rx, -30f, 30f);
        //ry = Mathf.Clamp(ry, -30f, 10f);

        mainCam.transform.localEulerAngles = new Vector3(-ry, rx, 0);
    }



    void RocketFire()
    {
        print("로켓발사");

    }

}
