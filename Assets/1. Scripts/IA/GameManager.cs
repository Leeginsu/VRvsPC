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
    int playerIndex = 0;
   // Start is called before the first frame update
   void Start()
    {
        int PCplayerCnt = PhotonNetwork.CurrentRoom.PlayerCount;

        //for (int i = 0; i < PCplayerCnt-1; i++)
        //{

        // 게임 씬에 입장한 플레이어 수에 따라 스폰 위치 선택
   

        if (ConnectionManager.instance.isVR)
        {
            GameObject vr = PhotonNetwork.Instantiate("VRPlayer_TEST", VRspawnPos.position, Quaternion.identity);
             //GameObject.Find("CenterEyeAnchor").GetComponent<Camera>().enabled = true;
        }
        else
        {
            if (photonView.IsMine)
            {
                photonView.RPC("setPlayerCnt", RpcTarget.All);
            }
            PhotonNetwork.Instantiate("Player_Proto", PCspawnList[playerIndex].position, Quaternion.identity);

        }
    }

    [PunRPC]
    void setPlayerCnt()
    {
        playerIndex++;
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
