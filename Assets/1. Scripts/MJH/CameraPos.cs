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

    
    public bool attackMode = false;
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            print("조준 중");
            attackMode = true;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            print("발사");
            attackMode = false;
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
}
