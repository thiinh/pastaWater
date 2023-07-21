using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip potRecallSound, potThrow, pickUp;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        potRecallSound = Resources.Load<AudioClip>("Pot Recall");
        potThrow = Resources.Load<AudioClip>("Pot Throw");
        pickUp = Resources.Load<AudioClip>("Pick Up");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "potRecall":
                audioSrc.PlayOneShot(potRecallSound);
                break;
            case "potThrow":
                audioSrc.PlayOneShot(potThrow);
                break;
            case "pickUp":
                audioSrc.PlayOneShot(pickUp);
                break;
        }
    }
}
