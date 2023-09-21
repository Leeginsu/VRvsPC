using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemManager : MonoBehaviourPun
{

    public GameObject[] rocPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InstantiateManager();
    }


    GameObject item;
    // 로켓을 소환한다.
    void InstantiateManager()
    {
        for (int i = 0; i < rocPos.Length; i++)
        {
            if(rocPos[i].transform.childCount < 2)
            {
                Invoke("InstantiateRocket", 3);
            }
        }
    }

    void InstatianteRocket(Vector3 position)
    {
        item = PhotonNetwork.Instantiate("Item", position, Quaternion.identity);
        
        item.transform.SetParent(gameObject.transform);
    }
    // 소환된 로켓은 배열의 자식으로 들어간다.
    // 만약 자식에 로켓이 있는지 판단 후 없으면 5초 뒤 재생성 한다.
   

}
