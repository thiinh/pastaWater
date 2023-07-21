using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    // Update is called once per frame
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Global");

        if (objs.Length > 1) 
        { 
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
