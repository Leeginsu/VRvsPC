using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR;
using OVRTouchSample;
public class VRUIManager : MonoBehaviourPunCallbacks
{
    public GameObject LoadingUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onStartVRBTNClick();
    }

    IEnumerator LoadingImg()
    {
        photonView.RPC("viewImg", RpcTarget.All);
       
        yield return new WaitForSeconds(3f);
        PhotonNetwork.LoadLevel("ProtoScene_Net");
    }

    [PunRPC]
    void viewImg()
    {
        LoadingUI.SetActive(true);
    }

    public void onStartVRBTNClick()
    {

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            print("click");
            StartCoroutine(LoadingImg());
            
        }
    }
}
