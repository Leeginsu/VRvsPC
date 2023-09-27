using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public AudioSource bgm;
    public AudioClip[] bgmList;
    // Start is called before the first frame update
    void Start()
    {
        
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
