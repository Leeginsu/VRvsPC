using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CamPos : MonoBehaviourPun, IPunObservable
{
    GameObject mainCam;
    public GameObject rocketBullet;
    public Transform firePos;
    public GameObject ch;

    int rocketCount;

    Vector3 receiveRocketPos;

    Quaternion receiveRocketRot = Quaternion.identity;
    float lerpSpeed = 50;

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
    
    public GameObject rocket;

    
    Vector3 dir;

    public bool isFire;
    PhotonView pv;

    // Update is called once per frame
    void Update()
    {
        // 로켓 카운트가 있을 때
        
        if (photonView.IsMine)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //ry = -mainCam.transform.eulerAngles.x;
                //rx = mainCam.transform.eulerAngles.y;
                //attackMode = true;
                //ch.SetActive(true);

                //일반
                //로켓을 생성
                //rocket = Instantiate(rocketBullet);
                //rocket.transform.position = firePos.position;
                //rocket.transform.parent = firePos;
                //rocket.GetComponent<Rigidbody>().useGravity = false;

                //포톤용
                //rocket = PhotonNetwork.Instantiate("Rocket", firePos.position, transform.rotation);
                //rocket.transform.parent = firePos;
                //rocket.GetComponent<Rigidbody>().useGravity = false;

                rocket = PhotonNetwork.Instantiate("Rocket", firePos.position, transform.rotation);
                rocket.transform.parent = firePos;
                pv = rocket.GetComponent<PhotonView>();
                //rocket.transform.parent = firePos;
                pv.ObservedComponents[0] = firePos;
                //photonView.RPC(nameof(RocketInstantiateRpc), RpcTarget.All, false);
                pv.RPC("InstantiateRpc", RpcTarget.All, firePos);

            }
            

            else if (Input.GetButton("Fire1"))
            {
                //rocket.transform.eulerAngles = transform.eulerAngles;

                dir = transform.forward;


            }
            else if (Input.GetButtonUp("Fire1"))
            {

                //attackMode = false;
                //ch.SetActive(false);

                //일반
                //로켓 발사
                //rocket.transform.forward = dir + Vector3.up * 0.5f;
                //rocket.GetComponent<Rigidbody>().useGravity = true;
                //rocket.GetComponent<Rocket>().rb.velocity = rocket.transform.forward * 50f;
                //rocket.GetComponent<Rocket>().spark1.SetActive(true);

                //포톤용
                //photonView.RPC(nameof(RocketFireRpc), RpcTarget.All , dir);
                
                pv.RPC("RocketTest", RpcTarget.All, dir, true);
                rocketCount--;
            }

            NormalCam();
        }
        else
        {
            rocket.transform.position = Vector3.Lerp(rocket.transform.position, receiveRocketPos, lerpSpeed * Time.deltaTime);
            rocket.transform.rotation = Quaternion.Lerp(rocket.transform.rotation, receiveRocketRot, lerpSpeed * Time.deltaTime);
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
        print("들어왔니");
        //rocket.transform.parent = firePos;
        rocket.GetComponent<Rigidbody>().useGravity = isBool;
    }

    [PunRPC]
    void RocketFireRpc(Vector3 dir)
    {
        print("해줄래"+dir);
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



    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rocket.transform.position);
            stream.SendNext(rocket.transform.rotation);
        }
        else
        {
            receiveRocketPos = (Vector3)stream.ReceiveNext();
            receiveRocketRot = (Quaternion)stream.ReceiveNext();
        }
    }



}
