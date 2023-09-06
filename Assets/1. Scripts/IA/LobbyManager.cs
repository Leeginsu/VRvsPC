using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;

    public bool isVR;

    public static bool isPresent()
    {
        var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
        foreach (var xrDisplay in xrDisplaySubsystems)
        {
            if (xrDisplay.running)
            {
                return true;
            }
        }
        return false;
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;

        Debug.Log("VR Device = " + isPresent().ToString());
        isVR = isPresent();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
