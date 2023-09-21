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
        LoadingUI.SetActive(true);
        yield return new WaitForSeconds(4f);
        PhotonNetwork.LoadLevel("ProtoScene_Net");
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
