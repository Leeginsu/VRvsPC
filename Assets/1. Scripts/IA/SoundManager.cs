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
            // ã�� ī�޶���� ��ȸ�ϸ鼭 ó��
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