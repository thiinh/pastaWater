using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timerText;
    public string timeValue;
    public float time;
    private float secondsCount;
    private int minuteCount;
    private float mSecCount;

    public bool showCollectibles = true;

    public Text collectibles;

    void Update()
    {
        UpdateTimerUI();
        
        Debug.Log(showCollectibles);

        if (showCollectibles) 
        { 
            UpdateItemsUI(); 
        }
    }
    //call this on update
    public void UpdateTimerUI()
    {
        time += Time.deltaTime;
        mSecCount = (int)((time - (int)time) * 100);
        secondsCount = (int)(time % 60);
        minuteCount = (int)(time / 60 % 60);

        timeValue = string.Format("{0:00}:{1:00}:{2:00}", minuteCount, secondsCount, mSecCount);

        timerText.text = timeValue;

        /*
        //set timer UI
        //secondsCount += Time.deltaTime;

        mSecCount = Time.deltaTime * 1000;

        timerText.text = minuteCount + "." + (int)secondsCount + "." + mSecCount.ToString("00");
        if (mSecCount >= 100)
        {
            secondsCount++;
            mSecCount = 0;
        }
        else if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
        */
    }
    public void UpdateItemsUI()
    {
        collectibles.text = "" + GameObject.FindGameObjectWithTag("Pot").GetComponent<PickUpController>().itemsCollected + " / " + GameObject.FindGameObjectWithTag("Pot").GetComponent<PickUpController>().totalItems;
    }
}
