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
        print("VR ���� ����" + isVR);
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
            print("������ ���� ����");
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

    [PunRPC]
    void setPlayers()
    {
        print("�÷��̾� �߰�");
        players.Add(PhotonNetwork.NickName);
    }


    [PunRPC]
    void NotionRPC()
    {
        
        //players.Add(PhotonNetwork.NickName);
        foreach (var item in players)
        {
            print("�÷��̾�" + item);
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

    //joinRoom�� ������!
    public void JoinRoom()
    {
        print("������");
        
        //���� ��
        //PhotonNetwork.JoinRoom("Main");

    }


    public override void OnJoinedRoom()
    {

        base.OnJoinedRoom();
        print("OnJoinedRoom ����!");
        photonView.RPC("setPlayers", RpcTarget.All);
        photonView.RPC("NotionRPC", RpcTarget.All);
        //�г��� ����
        //photonView.RPC("makeNickName", RpcTarget.All);
    }

    //�� ���� ��û ���� 
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomFailed ���� ����");
    }
    
    public void onStart()
    {
        PhotonNetwork.LoadLevel("ProtoScene");
    }
}
