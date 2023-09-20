using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public Transform[] PCspawnList;
    public Transform VRspawnPos;

    //UI 관련
    public GameObject TimePanel;
    public GameObject ScorePanel;
    TextMeshProUGUI TimeTXT;
    PhotonView SC;

    void Awake()
    {
        instance = this;
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    int playerIndex = 0;
   // Start is called before the first frame update
   void Start()
    {
        SC = GameObject.Find("ScoreManager").GetComponent<PhotonView>();
     
        gameTime = originGameTime;
        //UI set
        ScorePanel.SetActive(false);
        TimePanel.SetActive(true);
        TimeTXT = TimePanel.GetComponent<TextMeshProUGUI>();
        int PCplayerCnt = PhotonNetwork.CurrentRoom.PlayerCount;
 

        // 게임 씬에 입장한 플레이어 수에 따라 스폰 위치 선택
   
        if (ConnectionManager.instance.isVR)
        {
            GameObject vr = PhotonNetwork.Instantiate("VRPlayer_TEST", VRspawnPos.position, VRspawnPos.rotation);
             //GameObject.Find("CenterEyeAnchor").GetComponent<Camera>().enabled = true;
        }
        else
        {
            photonView.RPC("setPlayerCnt", RpcTarget.All);
         
            PhotonNetwork.Instantiate("Player_Proto", PCspawnList[playerIndex].position, Quaternion.identity);

        }

       

    }

    [PunRPC]
    public void setPlayerCnt()
    {
        playerIndex++;
        print("playerIndex" + playerIndex);
    }

    float currentTime = 0;
    public float originGameTime = 60f;
    float gameTime;
    
    // Update is called once per frame
    public void SetTime()
    {
        Timer();

        if (gameTime < 0)
        {
            //ScoreManager.instance.scoreView();
            SC.RPC("scoreView", RpcTarget.All);

            //UI 켜기
            //ScorePanel.SetActive(true);
            //TimePanel.SetActive(false);
            gameTime = originGameTime;
        }
    }

    float min;
    float sec;
    void Timer()
    {
        gameTime -= Time.deltaTime;

        if (gameTime >= 0)
        {
            min = (int)gameTime / 60;
            // 60으로 나눠서 생기는 나머지를 초단위로 설정
            sec = gameTime % 60;
            // UI를 표현해준다
            TimeTXT.text = string.Format("{00:00}:{01:00}", min, (int)sec);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        //currentTime += Time.deltaTime;
        SetTime();
        if (Input.GetKeyDown(KeyCode.Q)&& PhotonNetwork.IsMasterClient)
        {
         
            PhotonNetwork.LoadLevel("ReloadScene");
            //photonView.RPC("removePlayerList", RpcTarget.All);
        }
    }

    [PunRPC]
    void removePlayerList()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in players)
        {
            print(item.gameObject);
            Destroy(item.gameObject);
        }
        GameObject vrplayer = GameObject.FindWithTag("VRPlayer");
        Destroy(vrplayer);
        print("리로드씬 전환");
        PhotonNetwork.LoadLevel("ReloadScene");
    }

    public void onRestart()
    {
        print("재시작");
        PhotonNetwork.LoadLevel("ReloadScene");
        //PhotonNetwork.LoadLevel("ReloadScene");
    }

    public void onExit()
    {
        print("게임종료");
        Application.Quit();
    }
}
