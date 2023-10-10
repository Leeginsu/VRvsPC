using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource audio;

    public AudioClip[] bgmList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        //audio.Play();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        string sceneName = scene.name;

        // audio가 null이라면 초기화
        if (audio == null)
        {
            audio = GetComponent<AudioSource>();
            if (audio == null)
            {
                return;
            }
        }

        // 특정 씬에서만 BGM 변경
        if (sceneName == "LobbyScene")
        {
         
            if (bgmList.Length > 0)
            {
                audio.clip = bgmList[0];
                audio.Play();
            }
        }
        else if (sceneName == "ProtoScene_Net")
        {
         
            if (bgmList.Length > 0)
            {
                audio.clip = bgmList[1];
                audio.Play();
            }
        }
    }


    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    // Scene이 로드될 때 호출되는 함수
    //    // 여기서 원하는 BGM을 설정하면 됨

    

    //    int sceneIndex = scene.buildIndex; // 현재 로드된 Scene의 인덱스
    //    print("씬번호" + sceneIndex);
    //    // 예를 들어, Scene 1에선 bgmList[0], Scene 2에선 bgmList[1] 등으로 설정할 수 있음
    //    if (sceneIndex == 1)
    //    {

    //        audio.clip = Resources.Load("Audio/BGM(Menu)") as AudioClip;
    //        audio.Play();
    //    }
    //    else if (sceneIndex == 3)
    //    {
    //        audio.clip = Resources.Load("Audio/BGM(Battle)") as AudioClip;
    //        audio.Play();
    //    }
    //}

    public void PlayBGM()
    {
        audio.Play();
    }

    public void StopBGM()
    {
        audio.Stop();
    }


    public void PlayEffect(string str)
    {
        //bgm.clip = Resources.Load(str) as AudioClip;
        audio.PlayOneShot(Resources.Load(str) as AudioClip);
        audio.volume = 1f;
    }

}

