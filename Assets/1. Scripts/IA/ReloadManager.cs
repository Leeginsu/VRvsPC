using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ReloadManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //PhotonNetwork.LoadLevel("LobbyScene");
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("ProtoScene_Net");
            //StartCoroutine(LoadScene());
        }
        
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        PhotonNetwork.LoadLevel("ProtoScene_Net");
       
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
