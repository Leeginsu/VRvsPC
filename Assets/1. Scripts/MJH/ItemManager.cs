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
    // ������ ��ȯ�Ѵ�.
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
    // ��ȯ�� ������ �迭�� �ڽ����� ����.
    // ���� �ڽĿ� ������ �ִ��� �Ǵ� �� ������ 5�� �� ����� �Ѵ�.
   

}
