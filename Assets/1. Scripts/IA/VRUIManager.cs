using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR;
using OVRTouchSample;
public class VRUIManager : MonoBehaviourPunCallbacks
{
    public GameObject StartBTN;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onStartVRBTNClick();
    }

    public void onStartVRBTNClick()
    {

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            print("click");
            PhotonNetwork.LoadLevel("ProtoScene_Net");
        }
    }
}
