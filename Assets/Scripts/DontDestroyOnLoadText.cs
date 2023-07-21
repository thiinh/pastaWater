using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadText : MonoBehaviour
{
    // Update is called once per frame
    void Awake()
    {
        GameObject[] objsText = GameObject.FindGameObjectsWithTag("Text");

        if (objsText.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
