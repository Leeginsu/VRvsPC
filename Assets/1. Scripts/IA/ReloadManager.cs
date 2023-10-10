using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ReloadManager : MonoBehaviourPunCallbacks
{
    public Slider bar;
    //public GameObject LoadingUI;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //PhotonNetwork.LoadLevel("ProtoScene_Net");
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
       
        //LoadingUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        PhotonNetwork.LoadLevel("ProtoScene_Net");

    }

    // Update is called once per frame
    void Update()
    {
        bar.value += Time.deltaTime * 0.5f;
        //if(bar.value >= bar.maxValue)
        //{
        //    bar.value = 0;
        //}
    }
}
