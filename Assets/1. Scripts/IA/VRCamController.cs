using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class VRCamController : MonoBehaviourPun
{
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            cam.SetActive(true);
        }
        else
        {
            cam.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
