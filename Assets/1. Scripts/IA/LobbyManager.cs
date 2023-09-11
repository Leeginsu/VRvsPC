using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager instance;

    public GameObject StartBTN;

    public Transform PlayerPanel;
    public GameObject VRPlayerTXT;

    bool isVR;
    public int VRPlayerCnt = 0;
    public List<int> VRPlayerList = new List<int>(); 
    private void Awake()
    {
        isVR = ConnectionManager.instance.isVR;
        print("VR 접속 여부" + isVR);
    }


    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        CreateRoom();
        //setVRPlayer();

    }

    void Update()
    {
        print(PhotonNetwork.NickName);
    }

    public void CreateRoom()
    {
        //방 옵션 설정
        RoomOptions roomOptions = new RoomOptions();

        //최대 인원 설정
        //최대 인원 디폴트
        roomOptions.MaxPlayers = 3;

        //룸 리스트 공개
        roomOptions.IsVisible = false;

        //방생성 요청
        PhotonNetwork.JoinOrCreateRoom("Main", roomOptions, TypedLobby.Default);

    }

    //방 생성 호출 함수
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreateRoom");
        
    }
    
    ////이미 만들어져 있어서, 방참가
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoom" + returnCode + message);
       
    }




    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"플레이어 {newPlayer.NickName} 방 참가.");
        Debug.Log($"MAX {PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}");
        //players.Add(newPlayer.NickName);
        //photonView.RPC("UpdatePlayerList", RpcTarget.All, players);

    }

    public List<string> players = new List<string>();


    void RemovePlayerList()
    {
        for (int i = 0; i < PlayerPanel.childCount; i++)
        {
            Destroy(PlayerPanel.GetChild(i).gameObject);
        }
    }
  


    //joinRoom은 방참가!
    public void JoinRoom()
    {

        //조인 후
        PhotonNetwork.JoinRoom("Main");

    }
    public override void OnJoinedRoom()
    {

        base.OnJoinedRoom();
        print("OnJoinedRoom 입장!");
        
        setPlayerList();
        
    }


    void setPlayerList()
    {

        photonView.RPC("setPlayerTXT", RpcTarget.All, isVR);
        StartBTN.SetActive(isVR);

        //if (isVR)
        //{
        //    //VRPlayerCnt++;
        //    //VRPlayerTXT.SetActive(true);
        //    photonView.RPC("setVRPlayerTXT", RpcTarget.AllBuffered, true);
        //    StartBTN.SetActive(true);
        //} else
        //{
        //    photonView.RPC("setVRPlayerTXT", RpcTarget.All, false);
        //    //photonView.RPC("NotionRPC", RpcTarget.All);
        //    //VRPlayerTXT.SetActive(false);
        //    //StartBTN.SetActive(false);
        //}
    }
    



    [PunRPC]
    void setPlayerTXT(bool vr)
    {
        if(vr)
        {
            VRPlayerCnt++;
            VRPlayerTXT.SetActive(true);
        }

        NotionRPC();
    }

    [PunRPC]
    void NotionRPC()
    {
        RemovePlayerList();
        //remove
        //int cnt = VRPlayerList.Count;
        for (int i = 1; i <= PhotonNetwork.CurrentRoom.PlayerCount - VRPlayerCnt; i++)
        {
            print("VRPlayerCnt" + VRPlayerCnt);
            int num = PhotonNetwork.CurrentRoom.PlayerCount - VRPlayerCnt;
            print("PC플레이어" + num);
            GameObject obj = Resources.Load<GameObject>("PlayerListTXT");
            GameObject playerList = Instantiate(obj, PlayerPanel);
            TextMeshProUGUI txt = playerList.GetComponent<TextMeshProUGUI>();
            txt.text = "Player" + i;
        }
    }


    //방 참가 요청 실패 
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed 입장 실패");
    }
    
    public void onStartBTNClick()
    {

        //if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        //{
            PhotonNetwork.LoadLevel("ProtoScene");
        //}
    }
}
