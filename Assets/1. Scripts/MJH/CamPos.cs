using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CamPos : MonoBehaviourPun
{
    GameObject mainCam;
    //public GameObject rocketBullet;
    public Transform firePos;
    public GameObject ch;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            firePos.gameObject.SetActive(true);
        }
        //ch.SetActive(false);
        mainCam = Camera.main.gameObject;
        mainCam.transform.eulerAngles = new Vector3(5, 0, 0);
    }


    float rx, ry;
    float rotSpeed = 220f;
    //bool attackMode = false;
    
    //소환할 로켓 오브젝트
    public GameObject rocket;

    // 발사할 방향
    Vector3 dir;

    //public bool isFire;
    PhotonView pv;

    // Update is called once per frame
    void Update()
    {
        // 로켓 카운트가 있을 때
        // 내것일 때 체크
        if (photonView.IsMine)
        {
            // 마우스 왼쪽 버튼을 눌렀을 때
            if (Input.GetButtonDown("Fire1") && GetComponentInParent<PcPlayer>().rocketCount >0)
            {
                // 1. 로켓 소환
                rocket = PhotonNetwork.Instantiate("RocketTest", firePos.position, transform.rotation);
                // 2. 로켓을 FirePos 자식으로 붙임
                //rocket.transform.parent = firePos;
                
                // 3. 소환된 로켓의 포톤 뷰를 가져옴
                pv = rocket.GetComponent<PhotonView>();
                //pv.ObservedComponents[0] = firePos;

            }
            

            else if (Input.GetButton("Fire1") && GetComponentInParent<PcPlayer>().rocketCount > 0)
            {
                rocket.transform.position = firePos.position;
                dir = transform.forward;
            }
            else if (Input.GetButtonUp("Fire1") && GetComponentInParent<PcPlayer>().rocketCount > 0)
            {
                SoundManager.instance.PlayEffect("Audio/firework_sound");
                pv.RPC("RocketTest", RpcTarget.All, dir, true);
                if(GetComponentInParent<PcPlayer>().rocketCount > 0)
                {
                    GetComponentInParent<PcPlayer>().rocketCount--;
                }
            }

            NormalCam();
        }
        else
        {

        }

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
}
