using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using Photon.Realtime;
public class LobbyManager : MonoBehaviourPunCallbacks
{
    public static LobbyManager instance;

    public bool isVR;
    public GameObject StartBTN;
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
    void Start()
    {
        CreateRoom();
        if (isVR)
        {
            StartBTN.SetActive(true);
        }
        else
        {
            StartBTN.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
        PhotonNetwork.CreateRoom("Main", roomOptions);

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
        JoinRoom();
    }
    //joinRoom�� ������!
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("Test");
    }


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom ����!");
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
