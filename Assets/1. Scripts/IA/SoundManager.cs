using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource audio;

    public AudioClip[] bgmList;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
    }
    
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

//void Start()
//{
//    // ���� ī�޶� ã��
//    Camera mainCamera = Camera.main;

//    if (mainCamera != null)
//    {
//        // ���� ī�޶� �پ� �ִ� AudioListener�� ã��
//        AudioListener audioListener = mainCamera.GetComponent<AudioListener>();

//        if (audioListener != null)
//        {
//            // ã�Ҵٸ� audioListener�� ����� �� ����
//            Debug.Log("AudioListener found!");
//        }
//        else
//        {
//            Debug.LogWarning("AudioListener not found on the main camera.");
//        }
//    }
//    else
//    {
//        Debug.LogWarning("Main camera not found.");
//    }
//}