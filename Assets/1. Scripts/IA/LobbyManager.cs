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

    bool isVR;
    int VRPlayerCnt = 0;

    private void Awake()
    {
        isVR = ConnectionManager.instance.isVR;
        print("VR ���� ����" + isVR);
        photonView.RPC("setVRPlayer", RpcTarget.All);
    }

    [PunRPC]
    void setVRPlayer()
    {
        if (isVR)
        {
            VRPlayerCnt++;
        }
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

    [PunRPC]
    void setPlayers()
    {
        print("�÷��̾� �߰�");
      
        photonView.RPC("NotionRPC", RpcTarget.All);
    }

    void RemoveRoomList()
    {
        for (int i = 0; i < PlayerPanel.childCount; i++)
        {
            Destroy(PlayerPanel.GetChild(i).gameObject);
        }
    }
  
    [PunRPC]
    void NotionRPC()
    {
        RemoveRoomList();
        //remove
        for (int i = 1; i <= PhotonNetwork.CurrentRoom.PlayerCount - VRPlayerCnt; i++)
        {
            int num = PhotonNetwork.CurrentRoom.PlayerCount - VRPlayerCnt;
            print("PC�÷��̾�" + num);
            GameObject obj = Resources.Load<GameObject>("PlayerListTXT");
            GameObject playerList = Instantiate(obj, PlayerPanel);
            TextMeshProUGUI txt = playerList.GetComponent<TextMeshProUGUI>();
            txt.text = "Player" + i;
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

        //���� ��
        PhotonNetwork.JoinRoom("Main");

    }


    public override void OnJoinedRoom()
    {

        base.OnJoinedRoom();
        print("OnJoinedRoom ����!");
        photonView.RPC("NotionRPC", RpcTarget.All);
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    print("������ ���� ����");
        //    photonView.RPC("NotionRPC", RpcTarget.All);
        //}
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
