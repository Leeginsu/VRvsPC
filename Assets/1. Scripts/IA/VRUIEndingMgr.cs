using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.XR;
using OVRTouchSample;
using Photon.Pun;

public class VRUIEndingMgr : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onRestartVRBTNClick();
        onQuitVRBTNClick();
    }

    public void onRestartVRBTNClick()
    {
        //if (OVRInput.GetDown(OVRInput.Button.One)&& PhotonNetwork.IsMasterClient)
        //{
        //    print("����� click");
        //    PhotonNetwork.LoadLevel("ReloadScene");
        //}
    }

    public override void OnLeftRoom()
    {
        print("�� ������");
        //PhotonNetwork.LoadLevel("LobbyScene");
    }

    public void onQuitVRBTNClick()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            print("���� ��ư Ŭ��");
            //PhotonNetwork.LoadLevel("ProtoScene_Net");
        }
    }

}
