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
    //public AudioSource bgm;
    //public AudioClip bgm;
    //UI 관련
    public GameObject TimePanel;
    public GameObject ScorePanel;
    TextMeshProUGUI TimeTXT;
    PhotonView SC;
    AudioSource audioSource;
    public GameObject cv;

    public Dictionary<int, PhotonView> pcPlayer = new Dictionary<int, PhotonView>();

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
            cv.transform.parent = vr.transform;
            cv.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        }
        else
        {


         

            PhotonNetwork.Instantiate("Player_Proto", Vector3.zero, Quaternion.identity);

            //if (GameObject.Find("Player_BetaM"))
            //{
            //    PhotonNetwork.Instantiate("Player_BetaG", Vector3.zero, Quaternion.identity);
            //}
            //else
            //{
            //    PhotonNetwork.Instantiate("Player_BetaM", Vector3.zero, Quaternion.identity);
            //}
            //StartCoroutine(CheckRPC());
            cv.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;          
        }        
    }

    public void AddPcPlayer(PhotonView pv)
    {
        pcPlayer[pv.ViewID] = pv;

        if(PhotonNetwork.IsMasterClient)
        {
            if(pcPlayer.Count == PhotonNetwork.CurrentRoom.MaxPlayers - 1)
            {
                int index = 0;
                foreach(PhotonView view in pcPlayer.Values)
                {                    
                    photonView.RPC(nameof(setPlayerCnt), RpcTarget.All, view.ViewID, index);
                    index++;
                }
            }
        }
    }






    [PunRPC]
    public void setPlayerCnt(int viewId, int index)
    {
        pcPlayer[viewId].transform.position = PCspawnList[index].position;
    }

    IEnumerator CheckRPC()
    {
        // RPC 호출
        photonView.RPC("setPlayerCnt", RpcTarget.All);

        // RPC 호출 완료까지 대기
        yield return new WaitForSeconds(2f);
        print("RPCplayerIndex" + playerIndex);
      
        // 대기 후에 실행될 코드
        Debug.Log("RPC 호출 완료 확인");
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
      
    }

    public void onRestart()
    {
        print("재시작");
        if (PhotonNetwork.IsMasterClient)
        {

            PhotonNetwork.LoadLevel("ReloadScene");

        }

    }

    public void onExit()
    {
        print("게임종료");
        Application.Quit();
    }
}
