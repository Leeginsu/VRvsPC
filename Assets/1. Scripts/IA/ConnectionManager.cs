using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectionManager : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update
    void Start()
    {
     
        //서버 접속
        //서버에 App ID, 지역 요청
        PhotonNetwork.ConnectUsingSettings();
        print("서버 접속");
    }


    //네임서버 연결 성공(Lobby 진입 불가능)
    public override void OnConnected()
    {
        base.OnConnected();
        print("서버 연결 접속");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("마스터 접속");


        //닉네임 설정
        PhotonNetwork.NickName = "Ian";

        //로비 진입 요청
        PhotonNetwork.JoinLobby();
    }

    //로비 진입 성공 시 호출
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("씬이동");
        //PhotonNetwork.LoadLevel("LobbyScene");
    }


}
