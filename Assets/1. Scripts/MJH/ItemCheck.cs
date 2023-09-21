using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCheck : MonoBehaviour
{
    public ItemManager mySpawner;

    // Start is called before the first frame update
    void Start()
    {


    }


    // Update is called once per frame
    void Update()
    {

        
    }

    private void OnDestroy()
    {
        mySpawner.DestroyedItem(this);

    }
}
