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

        //방 옵션 설정
        RoomOptions roomOptions = new RoomOptions();

        //최대 인원 설정
        //최대 인원 디폴트
        roomOptions.MaxPlayers = 3;

        //룸 리스트 공개
        roomOptions.IsVisible = false;

        //방생성 요청
        PhotonNetwork.CreateRoom("Main", roomOptions);

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
        JoinRoom();
    }
    //joinRoom은 방참가!
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("Test");
    }


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom 입장!");
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
