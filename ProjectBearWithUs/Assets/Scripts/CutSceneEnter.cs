using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneEnter : MonoBehaviour
{
    public GameObject thePlayer;
    public GameObject cutSceneCam;

    void onTriggerEnter (Collider other)
    {
        cutSceneCam.SetActive(true);
        thePlayer.SetActive(false);
    }
}
