using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public Transform[] PCPlayerPosList;
    public Transform VRPlayerPos;

    int PCPlayerCNT = 0;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!ConnectionManager.instance.isVR)
        {
            PhotonNetwork.Instantiate("Player", PCPlayerPosList[0].position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setSpawnPos()
    {
        
    }
}
