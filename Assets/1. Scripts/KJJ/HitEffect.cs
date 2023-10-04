using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
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
            GameObject hitEffect = Instantiate(hitEffectFactory);
            hitEffect.transform.position = transform.position;
            Destroy(hitEffect.gameObject, 5);
            effectOn = false;
        }
    }
}
