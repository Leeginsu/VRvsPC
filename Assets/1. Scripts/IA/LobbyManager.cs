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

    private void Awake()
    {
        isVR = ConnectionManager.instance.isVR;
        print("VR ���� ����" + isVR);
    }

    void setVRPlayer()
    {
        if (isVR)
        {
            //VRPlayerCnt++;
            //VRPlayerTXT.SetActive(true);
            photonView.RPC("setVRPlayerTXT", RpcTarget.All);
            StartBTN.SetActive(true);
        }
        else
        {
            photonView.RPC("NotionRPC", RpcTarget.All);
            VRPlayerTXT.SetActive(false);
            StartBTN.SetActive(false);
        }
    }

   

    [PunRPC]
    void setVRPlayerTXT()
    {
        VRPlayerCnt++;
        VRPlayerTXT.SetActive(true);
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
        //�� �ɼ� ����
        RoomOptions roomOptions = new RoomOptions();

        //�ִ� �ο� ����
        //�ִ� �ο� ����Ʈ
        roomOptions.MaxPlayers = 3;

        //�� ����Ʈ ����
        roomOptions.IsVisible = false;

        //����� ��û
        PhotonNetwork.JoinOrCreateRoom("Main", roomOptions, TypedLobby.Default);

    }

    //�� ���� ȣ�� �Լ�
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreateRoom");
        
    }
    
    ////�̹� ������� �־, ������
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoom" + returnCode + message);
       
    }




    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"�÷��̾� {newPlayer.NickName} �� ����.");
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
  
    [PunRPC]
    void NotionRPC()
    {
        RemovePlayerList();
        //remove
        for (int i = 1; i <= PhotonNetwork.CurrentRoom.PlayerCount - VRPlayerCnt; i++)
        {
            print("VRPlayerCnt" + VRPlayerCnt);
            int num = PhotonNetwork.CurrentRoom.PlayerCount - VRPlayerCnt;
            print("PC�÷��̾�" + num);
            GameObject obj = Resources.Load<GameObject>("PlayerListTXT");
            GameObject playerList = Instantiate(obj, PlayerPanel);
            TextMeshProUGUI txt = playerList.GetComponent<TextMeshProUGUI>();
            txt.text = "Player" + i;
        } 
    }


    //joinRoom�� ������!
    public void JoinRoom()
    {

        //���� ��
        PhotonNetwork.JoinRoom("Main");

    }


    public override void OnJoinedRoom()
    {

        base.OnJoinedRoom();
        print("OnJoinedRoom ����!");
        //photonView.RPC("setVRPlayer", RpcTarget.All);
        setVRPlayer();
        //if (!isVR)
        //{
        //    photonView.RPC("NotionRPC", RpcTarget.All);
        //}
        

    }

    //�� ���� ��û ���� 
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed ���� ����");
    }
    
    public void onStartBTNClick()
    {

        //if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        //{
            PhotonNetwork.LoadLevel("ProtoScene");
        //}
    }
}
