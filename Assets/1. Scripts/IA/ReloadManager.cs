using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ReloadManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        //PhotonNetwork.LoadLevel("LobbyScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
