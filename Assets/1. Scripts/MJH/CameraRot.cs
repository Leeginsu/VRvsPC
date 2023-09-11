using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRot : MonoBehaviour
{

    float rx, ry;


    [Tooltip("카메라 회전 속도")]
    [Range(1, 300)] public float rotSpeed = 10;



    private void Awake()
    {
        transform.eulerAngles = new Vector3(20, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        //transform.eulerAngles = new Vector3(20, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if(GetComponentInParent<CameraPos>().attackMode == false)
        {
            float my = Input.GetAxis("Mouse Y");

            ry += my * rotSpeed * Time.deltaTime;

            // 위아래 각도 제한
            ry = Mathf.Clamp(ry, -30, 30);

            transform.localEulerAngles = new Vector3(-ry, 0, 0);
        }
        
    
    }


    
}
