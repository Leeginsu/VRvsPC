using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HitEffect : MonoBehaviourPun
{
    public GameObject hitEffectFactory;
    bool effectOn = false;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.isKinematic == true)
        {
            effectOn = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (effectOn)
        {
            GameObject hitEffect = PhotonNetwork.Instantiate("hitEffectFactory", transform.position, Quaternion.identity);
            Destroy(hitEffect.gameObject, 5);
            effectOn = false;
        }
    }
}
