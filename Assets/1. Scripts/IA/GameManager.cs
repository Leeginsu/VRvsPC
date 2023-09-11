using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public Transform[] PCspawnList;
    public Transform VRspawnPos;
    bool isGameReady = false;

    public GameObject TimePanel;

    // Start is called before the first frame update
    void Start()
    {
        int PCplayerCnt = PhotonNetwork.CurrentRoom.PlayerCount;

        //for (int i = 0; i < PCplayerCnt-1; i++)
        //{

        // 게임 씬에 입장한 플레이어 수에 따라 스폰 위치 선택
        int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;

        if (ConnectionManager.instance.isVR)
        {
            PhotonNetwork.Instantiate("VRPlayer", VRspawnPos.position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("Player_Proto", PCspawnList[0].position, Quaternion.identity);
        }
    }

    float currentTime = 0;
    float gameTime = 120f;
    bool isEnd;
    // Update is called once per frame
    void SetTime()
    {
        gameTime -= currentTime;
        if(gameTime < 0)
        {
            isEnd = true; 
        }
    }
    // Update is called once per frame
    void Update()
    {
        SetTime();
        if (isEnd)
        {
            print("���� ����");
            //���� ������ ������ �Ѿ��
            //PhotonNetwork.LoadLevel("EndingScene");
        }
    }
}
