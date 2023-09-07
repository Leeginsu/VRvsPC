using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    [Tooltip("ī�޶� ȸ�� �ӵ�")]
    [Range(1, 300)] public float rotSpeed = 10;

    [Tooltip("ī�޶� ���� �ӵ�")]
    [Range(100, 200)] public float zoomSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float mx = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;

        Vector3 pos = transform.parent.position;

        transform.RotateAround(transform.parent.position, Vector3.up, mx);

        transform.LookAt(pos);

        Zoom();
    }

    void Zoom()
    {
        float wheel = Input.GetAxis("Mouse ScrollWheel");

        Vector3 zoomDir = transform.parent.position - transform.position;

        transform.position += zoomDir * wheel * Time.deltaTime * zoomSpeed;
    }
}
