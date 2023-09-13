using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BombTrigger : MonoBehaviourPun
{
    public GameObject bomb;
    public Transform firePos;
    public float chargeTime = 5f;

    PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    //
    bool isMaking = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
     
            if (!isMaking)
            {
                //StartCoroutine(makeBomb());
                //makeBomb();
                print("트리거체크111");
                StartCoroutine(makeBomb());
                //photonView.RPC("setTriggerBomb", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void setTriggerBomb()
    {
        print("트리거체크222");
        StartCoroutine(makeBomb());
    }
    IEnumerator makeBomb()
    {
        print("트리거체크333");
        //isMaking = true;
        //transform.position += transform.up * -0.5f;
        photonView.RPC("downTrigger", RpcTarget.All);
        bomb = PhotonNetwork.Instantiate("Bomb", firePos.position, firePos.rotation);


        //pv = bomb.GetComponent<PhotonView>();
        //pv.RPC("UpdatePosRpc", RpcTarget.All);
        //Instantiate(bomb, firePos.position, firePos.rotation);

        yield return new WaitForSeconds(chargeTime);
        photonView.RPC("upTrigger", RpcTarget.All);

    }


    //float currentTime;
    //void makeBomb()
    //{

    //    //isMaking = true;
    //    //transform.position += transform.up * -0.5f;
    //    pv.RPC("downTrigger", RpcTarget.All);
    //    bomb = PhotonNetwork.Instantiate("Bomb", firePos.position, firePos.rotation);


    //    pv = bomb.GetComponent<PhotonView>();
    //    pv.RPC("UpdatePosRpc", RpcTarget.All);
    //    //Instantiate(bomb, firePos.position, firePos.rotation);


    //    pv.RPC("upTrigger", RpcTarget.All);

    //}

    [PunRPC]
    void downTrigger()
    {
        isMaking = true;
        transform.position += transform.up * -0.5f;
    }

    [PunRPC]
    void upTrigger()
    {
        transform.position += transform.up * 0.5f;
        isMaking = false;
    }
}
