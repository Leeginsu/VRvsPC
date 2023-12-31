using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PcPlayer : MonoBehaviourPun, IPunObservable
{

    Animator anim;
    public GameObject player;

    // 플레이어 속도
    [Range(1,20)]
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
    PhotonView SC;
    // Start is called before the first frame update
    void Start()
    {
        

        PhotonNetwork.SerializationRate = 60;
        SC = GameObject.Find("ScoreManager").GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            trCam.SetActive(true);
            rocketCount = 0;
        }

        jumpCount = 1;
        anim = player.GetComponent<Animator>();

        respawnPos = GameObject.Find("PCPlayerPosList");
        randomIndex = Random.Range(0, respawnPos.transform.childCount);
        
        GameManager.instance.AddPcPlayer(photonView);
    }

    // Update is called once per frame
    void Update()
    {
        if(isHit == false)
        {
            PlayerMove();
        }

        if (photonView.IsMine && isHit == true)
        {
            hitTime += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (rocketCount > 0)
                {
                    photonView.RPC(nameof(SetBool), RpcTarget.All, "Hit", false);
                    photonView.RPC(nameof(SetTriggerRpc), RpcTarget.All, "Jumping");
                    transform.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0) ;
                    transform.GetComponent<Rigidbody>().AddForce((transform.forward * 5f + (Vector3.up * rocketPower)), ForceMode.Impulse);
                    isRocket = true;
                    rocketCount--;

                }
            }

            if (hitTime >= 3f)
            {
                photonView.RPC(nameof(SetBool), RpcTarget.All, "Hit", false);
                //anim.SetBool("Hit", false);
                hitTime = 0;
                isHit = false;
            }  
        }

        if (photonView.IsMine)
        {
            PlayerRespawn();
            
        }
        if (fall)
        {
            respawn();
        }

    }
    void respawn()
    {
        //ScoreManager.instance.VRSCORE += 1;
        SC.RPC("UpdateVRScore", RpcTarget.All);
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
                if (rocketCount > 0)
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
        if (transform.position.y < -30f)
        {
            fall = true;
            transform.position = respawnPos.transform.GetChild(randomIndex).transform.position;
        }
    }



    public ItemCheck check;

    public float hitTime = 0;
    public bool isHit;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = 1;
            if (isJump == true && photonView.IsMine == true)
            {
                photonView.RPC(nameof(SetTriggerRpc), RpcTarget.All, "Land");
                isJump = false;
            }

            if(isRocket == true && photonView.IsMine == true)
            {
                photonView.RPC(nameof(SetTriggerRpc), RpcTarget.All, "Land");
                isRocket = false;
            }
            else
            {
                //photonView.RPC(nameof(SetTriggerRpc), RpcTarget.All, "Land");
            }

        }



        if(collision.gameObject.layer == LayerMask.NameToLayer("Grabable"))
        {
            if (collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.5f)
            {
                if (photonView.IsMine)
                {
                    photonView.RPC(nameof(SetBool), RpcTarget.All, "Hit", true);
                    //anim.SetBool("Hit",true);
                    isHit = true;

                    Vector3 dir = collision.gameObject.transform.position - transform.position;

                    transform.GetComponent<Rigidbody>().AddForce((-dir * 200f + (Vector3.up * 1000f)) * Time.deltaTime, ForceMode.Impulse);
                }
            }
            //if(collision.gameObject.GetComponent<Rigidbody>().isKinematic == false)
            //{
            //    photonView.RPC(nameof(SetBool), RpcTarget.All, "Hit", true);
            //    //anim.SetBool("Hit",true);
            //    isHit = true; 
            //}
        }

        if(collision.gameObject.tag == "Earthquake")
        {
            if (photonView.IsMine)
            {
                photonView.RPC(nameof(SetBool), RpcTarget.All, "Hit", true);
                //anim.SetBool("Hit",true);
                isHit = true;

                Vector3 dir = collision.gameObject.transform.position - transform.position;

                transform.GetComponent<Rigidbody>().AddForce(((new Vector3(-dir.x,0, -dir.y) * 200f) + (Vector3.up * 1000f)) * Time.deltaTime, ForceMode.Impulse);
            }
        }

        if(collision.gameObject.tag == "Bullet")
        {
            print("++");
            Destroy(collision.gameObject);
            print("prev_rocketCount" + rocketCount);
            print("photonView.IsMine" + photonView.IsMine);
            if (photonView.IsMine)
            {
              
                if (rocketCount < 4)
                {
                    print("rocketCount"+ rocketCount);
                    this.rocketCount++;
                }
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
