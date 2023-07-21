using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Text timerText;
    public GameObject gMan;

    private void Awake()
    {
        timerText.text = GameObject.Find("GameManager").GetComponent<GameManager>().timeValue;
        GameObject.Find("GameManager").GetComponent<GameManager>().showCollectibles = false;
        GameObject.Find("Collectibles").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Destroy(GameObject.Find("GameManager"));
            Destroy(GameObject.Find("AudioManager"));
            Destroy(GameObject.Find("Canvas"));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 6);
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
            Debug.Log("ESCAPED");
        }
    }
}
