using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRot : MonoBehaviour
{

    float rx, ry;


    [Tooltip("ī�޶� ȸ�� �ӵ�")]
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

            // ���Ʒ� ���� ����
            ry = Mathf.Clamp(ry, -30, 30);

            transform.localEulerAngles = new Vector3(-ry, 0, 0);
        }
        
    
    }


    
}
