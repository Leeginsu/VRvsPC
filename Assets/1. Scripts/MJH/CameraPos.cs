using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    [Tooltip("카메라 회전 속도")]
    [Range(1, 300)] public float rotSpeed = 10;

    [Tooltip("카메라 줌인 속도")]
    [Range(100, 200)] public float zoomSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    [HideInInspector]
    public bool attackMode = false;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButton("Fire1"))
        {
            print("조준 중");
            attackMode = true;
            AttackCam();

        }
        else if (Input.GetButtonUp("Fire1"))
        {
            
            attackMode = false;
            RocketFire();

        }

        if (attackMode == false)
        {
            NormalCam();
        }

        

        Zoom();
    }

    void Zoom()
    {
        float wheel = Input.GetAxis("Mouse ScrollWheel");

        Vector3 zoomDir = transform.parent.position - transform.position;

        transform.position += zoomDir * wheel * Time.deltaTime * zoomSpeed;
    }

    // 평상시 카메라 회전
    void NormalCam()
    {
        float mx = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;

        Vector3 pos = transform.parent.position;

        transform.RotateAround(transform.parent.position, Vector3.up, mx);

        transform.LookAt(pos);
    }

    float rx, ry;
    // 조준시 카메라 회전
    void AttackCam()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        rx += mx * rotSpeed * Time.deltaTime;
        ry += my * rotSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(-ry, rx, 0);

        // 크로스헤어 UI 활성화


    }

    // 로켓 발사
    void RocketFire()
    {
        print("발사");
    }
}
