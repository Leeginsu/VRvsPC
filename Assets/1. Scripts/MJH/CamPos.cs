using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CamPos : MonoBehaviourPun
{
    GameObject mainCam;
    public GameObject rocketBullet;
    public Transform firePos;
    public GameObject ch;

    int rocketCount;

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

        rocketCount = GetComponentInParent<PcPlayer>().rocketCount;
    }


    float rx, ry;
    float rotSpeed = 220f;
    bool attackMode = false;
    [HideInInspector]
    public GameObject rocket;

    
    Vector3 dir;

    public bool isFire;
    PhotonView pv;

    // Update is called once per frame
    void Update()
    {
        // ���� ī��Ʈ�� ���� ��
        
        if (photonView.IsMine)
        {
            if (Input.GetButtonDown("Fire1") || rocketCount > 0)
            {
                //ry = -mainCam.transform.eulerAngles.x;
                //rx = mainCam.transform.eulerAngles.y;
                //attackMode = true;
                //ch.SetActive(true);

                //�Ϲ�
                //������ ����
                //rocket = Instantiate(rocketBullet);
                //rocket.transform.position = firePos.position;
                //rocket.transform.parent = firePos;
                //rocket.GetComponent<Rigidbody>().useGravity = false;

                //�����
                //rocket = PhotonNetwork.Instantiate("Rocket", firePos.position, transform.rotation);
                //rocket.transform.parent = firePos;
                //rocket.GetComponent<Rigidbody>().useGravity = false;

                rocket = PhotonNetwork.Instantiate("Rocket", firePos.position, transform.rotation);
                rocket.transform.parent = firePos;
                //photonView.RPC(nameof(RocketInstantiateRpc), RpcTarget.All, false);
                pv = rocket.GetComponent<PhotonView>();
                pv.RPC("InstantiateRpc", RpcTarget.All, firePos);

            }

            else if (Input.GetButton("Fire1") ||  rocketCount > 0)
            {
                //rocket.transform.eulerAngles = transform.eulerAngles;

                dir = transform.forward;


            }
            else if (Input.GetButtonUp("Fire1") || rocketCount > 0)
            {

                //attackMode = false;
                //ch.SetActive(false);

                //�Ϲ�
                //���� �߻�
                //rocket.transform.forward = dir + Vector3.up * 0.5f;
                //rocket.GetComponent<Rigidbody>().useGravity = true;
                //rocket.GetComponent<Rocket>().rb.velocity = rocket.transform.forward * 50f;
                //rocket.GetComponent<Rocket>().spark1.SetActive(true);

                //�����
                //photonView.RPC(nameof(RocketFireRpc), RpcTarget.All , dir);
                
                pv.RPC("RocketTest", RpcTarget.All, dir, true);
                rocketCount--;
            }


            NormalCam();
        }
        else
        {

        }
        

        //if (attackMode == false)
        //{
        //    NormalCam();
        //}
        //else
        //{

        //   // AttackCam();
        //}
    }

    [PunRPC]
    void RocketInstantiateRpc(bool isBool)
    {
        //rocket = Instantiate(rocketBullet, firePos.position, transform.rotation);
        print("���Դ�");
        //rocket.transform.parent = firePos;
        rocket.GetComponent<Rigidbody>().useGravity = isBool;
    }

    [PunRPC]
    void RocketFireRpc(Vector3 dir)
    {
        print("���ٷ�"+dir);
        rocket.transform.forward = dir + Vector3.up * 0.5f;
        rocket.GetComponent<Rigidbody>().useGravity = true;
        rocket.GetComponent<Rocket>().rb.velocity = rocket.transform.forward * 50f;
        rocket.GetComponent<Rocket>().spark1.SetActive(true);
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
