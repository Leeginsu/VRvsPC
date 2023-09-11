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
        PhotonNetwork.Instantiate("VRPlayer", VRspawnPos.position, Quaternion.identity);
        for (int i = 0; i < PCplayerCnt-1; i++)
        {
            PhotonNetwork.Instantiate("Player_Proto", PCspawnList[i].position, Quaternion.identity);
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
            print("게임 종료");
            //점수 가지고 엔딩씬 넘어가기
            //PhotonNetwork.LoadLevel("EndingScene");
        }
    }
}
