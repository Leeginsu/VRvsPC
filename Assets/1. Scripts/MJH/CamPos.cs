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
    
    //��ȯ�� ���� ������Ʈ
    public GameObject rocket;

    // �߻��� ����
    Vector3 dir;

    //public bool isFire;
    PhotonView pv;

    // Update is called once per frame
    void Update()
    {
        // ���� ī��Ʈ�� ���� ��
        // ������ �� üũ
        if (photonView.IsMine)
        {
            // ���콺 ���� ��ư�� ������ ��
            if (Input.GetButtonDown("Fire1") && GetComponentInParent<PcPlayer>().rocketCount >0)
            {
                // 1. ���� ��ȯ
                rocket = PhotonNetwork.Instantiate("RocketTest", firePos.position, transform.rotation);
                // 2. ������ FirePos �ڽ����� ����
                //rocket.transform.parent = firePos;
                
                // 3. ��ȯ�� ������ ���� �並 ������
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

        //�翷
        //rx = Mathf.Clamp(rx, -45f, 45f);

        //���Ʒ�
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
