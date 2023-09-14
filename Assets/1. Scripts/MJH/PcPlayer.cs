using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PcPlayer : MonoBehaviourPun, IPunObservable
{

    Animator anim;
    public GameObject player;

    // 플레이어 속도
    [Range(1,10)]
    public float moveSpeed = 5f;

    // 플레이어 점프 가능 횟수
    [SerializeField]
    int jumpCount = 0;
    bool isJump = false;
    public float jumpPower = 10f;

    // 로켓 아이템
    //[SerializeField]
    public int rocketCount = 0;
    public bool isRocket = false;
    public float rocketPower = 20f;


    /// <summary>
    /// 포톤
    /// </summary>
    Vector3 receivePos;

    Quaternion receiveRot = Quaternion.identity;

    float lerpSpeed = 50;
    public GameObject trCam;



    int randomIndex;
    GameObject respawnPos;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.SerializationRate = 60;

        if (photonView.IsMine)
        {
            trCam.SetActive(true);
        }

        jumpCount = 1;
        rocketCount = 2;
        anim = player.GetComponent<Animator>();

        respawnPos = GameObject.Find("PCPlayerPosList");

        

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        if (photonView.IsMine && isHit == true)
        {
            hitTime += Time.deltaTime;

            if (hitTime >= 3f)
            {
                photonView.RPC(nameof(SetBool), RpcTarget.All, "Hit", false);
                //anim.SetBool("Hit", false);
                hitTime = 0;
                isHit = false;
            }  
        }

        PlayerRespawn();
        if (fall)
        {
            respawn();
        }
    }
    void respawn()
    {
        transform.position = respawnPos.transform.GetChild(randomIndex).transform.position;
        ScoreManager.instance.VRSCORE += 1;
        fall = false;
    }


    float moveZ;
    float moveX;
    void PlayerMove()
    {
        if (photonView.IsMine)
        {
            moveZ = 0f;
            moveX = 0f;

            if (Input.GetKey(KeyCode.W))
            {
                moveZ += 1f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                moveX -= 1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveZ -= 1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveX += 1f;
            }

            Vector3 dir = new Vector3(moveX, 0, moveZ);
            dir.Normalize();
            transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);

            if (moveX != 0 || moveZ != 0)
            {
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 20f);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (jumpCount > 0)
                {
                    transform.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                    isJump = true;
                    photonView.RPC(nameof(SetTriggerRpc), RpcTarget.All, "Jump");
                    jumpCount--;
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (rocketCount > 0 && isRocket == false)
                {
                    transform.GetComponent<Rigidbody>().AddForce(Vector3.up * rocketPower, ForceMode.Impulse);
                    isRocket = true;
                    photonView.RPC(nameof(SetTriggerRpc), RpcTarget.All, "Jumping");
                    rocketCount--;
                }
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, receivePos, lerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(player.transform.rotation, receiveRot, lerpSpeed * Time.deltaTime);
        }

        anim.SetFloat("Horizontal", moveZ);
        anim.SetFloat("Vertical", moveX);
    }

    bool fall = false;
    void PlayerRespawn()
    {
        randomIndex = Random.Range(0, respawnPos.transform.childCount);

        if (transform.position.y < -60f)
        {
            fall = true;
        }
    }


    public float hitTime = 0;
    bool isHit;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = 1;
            if (isJump == true)
            {
                photonView.RPC(nameof(SetTriggerRpc), RpcTarget.All, "Land");
                isJump = false;
            }

            if(isRocket == true)
            {
                photonView.RPC(nameof(SetTriggerRpc), RpcTarget.All, "Land");
                isRocket = false;
            }
            else
            {
                //photonView.RPC(nameof(SetTriggerRpc), RpcTarget.All, "Land");
            }

        }



        if(collision.gameObject.tag == "Untagged")
        {
            if(collision.gameObject.GetComponent<Rigidbody>().isKinematic == false)
            {
                photonView.RPC(nameof(SetBool), RpcTarget.All, "Hit", true);
                //anim.SetBool("Hit",true);
                isHit = true;
                
                
            }
        }
    }

    [PunRPC]
    void SetBool(string parameter, bool isBool)
    {
        anim.SetBool(parameter, isBool);
        
    }


    [PunRPC]
    void SetTriggerRpc(string parameter)
    {
        anim.SetTrigger(parameter);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(player.transform.rotation);
            stream.SendNext(moveZ);
            stream.SendNext(moveX);
        }
        else
        {
            receivePos = (Vector3)stream.ReceiveNext();
            receiveRot = (Quaternion)stream.ReceiveNext();
            moveZ = (float)stream.ReceiveNext();
            moveX = (float)stream.ReceiveNext();
        }
    }

}
