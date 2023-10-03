using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    AudioSource bgm;
    public AudioClip[] bgmList;
    // Start is called before the first frame update
    void Start()
    {
        Camera[] cameras = GameObject.FindObjectsOfType<Camera>();

        if (cameras.Length > 0)
        {
            // 찾은 카메라들을 순회하면서 처리
            foreach (Camera camera in cameras)
            {
                Debug.Log("Found Camera: " + camera.name);
            }
        }
        else
        {
            Debug.LogWarning("No cameras found in the scene.");
        }

        bgm = GetComponent<AudioSource>();
        print(bgm);
        bgm.Play();
    }
    
    public void PlayBGM()
    {
        bgm.Play();
    }

    public void StopBGM()
    {
        bgm.Stop();
    }


}

//void Start()
//{
//    // 메인 카메라를 찾음
//    Camera mainCamera = Camera.main;

//    if (mainCamera != null)
//    {
//        // 메인 카메라에 붙어 있는 AudioListener를 찾음
//        AudioListener audioListener = mainCamera.GetComponent<AudioListener>();

//        if (audioListener != null)
//        {
//            // 찾았다면 audioListener를 사용할 수 있음
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