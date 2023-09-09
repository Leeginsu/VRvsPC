using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : MonoBehaviour
{
    public GameObject bomb;
    public Transform firePos;
    public float chargeTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    //
    bool isMaking = false;
    private void OnTriggerEnter(Collider other)
    {
        print("fff");
        if (other.gameObject.CompareTag("Player"))
        {
            print("fff");
            if (!isMaking)
            {
                StartCoroutine(makeBomb());
            }
        }
    }

    IEnumerator makeBomb()
    {
        isMaking = true;
        transform.position += transform.up * -0.5f;
        Instantiate(bomb, firePos.position, firePos.rotation);

        yield return new WaitForSeconds(chargeTime);
        transform.position += transform.up * 0.5f;
        isMaking = false;
    }
}
