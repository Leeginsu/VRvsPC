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
        PhotonNetwork.AutomaticallySyncScene = true;
        onRestartVRBTNClick();
        onQuitVRBTNClick();
    }

    public void onRestartVRBTNClick()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            print("재시작 click");
            //PhotonNetwork.LeaveRoom();
          
            //PhotonNetwork.LoadLevel("ProtoScene_Net");
        }
    }

    public override void OnLeftRoom()
    {
        print("방 나가기");
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    public void onQuitVRBTNClick()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            print("종료 버튼 클릭");
            //PhotonNetwork.LoadLevel("ProtoScene_Net");
        }
    }

}
