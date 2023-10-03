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

        // audio�� null�̶�� �ʱ�ȭ
        if (audio == null)
        {
            audio = GetComponent<AudioSource>();
            if (audio == null)
            {
                return;
            }
        }

        // Ư�� �������� BGM ����
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
    //    // Scene�� �ε�� �� ȣ��Ǵ� �Լ�
    //    // ���⼭ ���ϴ� BGM�� �����ϸ� ��

    

    //    int sceneIndex = scene.buildIndex; // ���� �ε�� Scene�� �ε���
    //    print("����ȣ" + sceneIndex);
    //    // ���� ���, Scene 1���� bgmList[0], Scene 2���� bgmList[1] ������ ������ �� ����
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

