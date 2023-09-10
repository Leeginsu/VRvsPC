using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR;
public class ConnectionManager : MonoBehaviourPunCallbacks
{

    public static ConnectionManager instance;

    public bool isVR;
    public static bool isPresent()
    {
        var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
        foreach (var xrDisplay in xrDisplaySubsystems)
        {
            if (xrDisplay.running)
            {
                return true;
            }
        }
        return false;
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;

        Debug.Log("VR Device = " + isPresent().ToString());
        isVR = isPresent();
    }
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
        makeNickName();
        //�κ� ���� ��û
        PhotonNetwork.JoinLobby();

    }



    //�κ� ���� ���� �� ȣ��
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("���̵�");
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    void makeNickName()
    {
        if (isVR)
        {
            PhotonNetwork.NickName = "VRPlayer";
        }
        else
        {
            int playerCountInLobby = PhotonNetwork.CountOfPlayersInRooms;
            Debug.Log("���� �� ���� �ο� ��: " + playerCountInLobby);
            PhotonNetwork.NickName = "Player";
        }
    }
}
