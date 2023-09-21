using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemManager : MonoBehaviourPun
{

    public List<GameObject> items;
    public GameObject item;

    public float delayTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        //item = Instantiate(Resources.Load<GameObject>("Item"));
        item = PhotonNetwork.Instantiate("Item", transform.position, transform.rotation);
        item.transform.position = transform.position;
        item.GetComponent<ItemCheck>().mySpawner = this;
        items.Add(item);
    }

    // Update is called once per frame
    void Update()
    {
        if (items.Count <= 0)
        {
            delayTime += Time.deltaTime;
            if (delayTime > 3)
            {
                //item = Instantiate(Resources.Load<GameObject>("Item"));
                item = PhotonNetwork.Instantiate("Item", transform.position, transform.rotation);
                item.GetComponent<ItemCheck>().mySpawner = this;
                items.Add(item);
            }
        }
    }
    internal void DestroyedItem(ItemCheck item)
    {
        if (items.Contains(item.gameObject))
            items.Remove(item.gameObject);
        delayTime = 0;
    }

}
