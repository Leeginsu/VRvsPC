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

    public List<string> players = new List<string>();

    public Transform PlayerPanel;

    bool isVR;

    private void Awake()
    {
        isVR = ConnectionManager.instance.isVR;
        print("VR 접속 여부" + isVR);
    }
    void Start()
    {
        CreateRoom();
        if (ConnectionManager.instance.isVR)
        {
            StartBTN.SetActive(true);
        }
        else
        {
            StartBTN.SetActive(false);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            print("마스터 서버 접속");
            //players.Add(PhotonNetwork.NickName);
            //photonView.RPC("NotionRPC", RpcTarget.All);
        }
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

    [PunRPC]
    void setPlayers()
    {
        print("플레이어 추가");
        players.Add(PhotonNetwork.NickName);
    }


    [PunRPC]
    void NotionRPC()
    {
        
        //players.Add(PhotonNetwork.NickName);
        foreach (var item in players)
        {
            print("플레이어" + item);
            GameObject obj = Resources.Load<GameObject>("PlayerListTXT");
            GameObject playerList = Instantiate(obj, PlayerPanel);
            TextMeshProUGUI txt = playerList.GetComponent<TextMeshProUGUI>();
            txt.text = item;
        }
           
    }
    int PCPlayerCount = 0;
    [PunRPC]
    void PlusCount()
    {
        PCPlayerCount++;
    }

    //joinRoom은 방참가!
    public void JoinRoom()
    {
        print("방참가");
        
        //조인 후
        //PhotonNetwork.JoinRoom("Main");

    }


    public override void OnJoinedRoom()
    {

        base.OnJoinedRoom();
        print("OnJoinedRoom 입장!");
        photonView.RPC("setPlayers", RpcTarget.All);
        photonView.RPC("NotionRPC", RpcTarget.All);
        //닉네임 설정
        //photonView.RPC("makeNickName", RpcTarget.All);
    }

    //방 참가 요청 실패 
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed 입장 실패");
    }
    
    public void onStart()
    {
        PhotonNetwork.LoadLevel("ProtoScene");
    }
}
