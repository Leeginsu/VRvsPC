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
     
        //���� ����
        //������ App ID, ���� ��û
        PhotonNetwork.ConnectUsingSettings();
        print("���� ����");
    }


    //���Ӽ��� ���� ����(Lobby ���� �Ұ���)
    public override void OnConnected()
    {
        base.OnConnected();
        print("���� ���� ����");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("������ ����");


        //�г��� ����
        PhotonNetwork.NickName = "Ian";

        //�κ� ���� ��û
        PhotonNetwork.JoinLobby();
    }

    //�κ� ���� ���� �� ȣ��
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("���̵�");
        //PhotonNetwork.LoadLevel("LobbyScene");
    }


}
