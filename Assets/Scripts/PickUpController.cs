using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUpController : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider collid;
    public Transform player, potContainer, chimera;
    public PotSway potSwayScript;
    public AudioClip recall;

    [Header ("Values")]
    public Vector3 sizeChange;
    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    //public Text Collectibles;
    public int totalItems;
    public int itemsCollected;

    public bool equipped;
    public static bool slotFull;

    private void Awake()
    {
        totalItems = GameObject.FindGameObjectsWithTag("Collectible").Length;
        itemsCollected = 0;

    }
    void Start()
    {
        //Setup
        if (!equipped)
        {
            rb.isKinematic = false;
            collid.isTrigger = false;
        }
        if (equipped)
        {
            rb.isKinematic = true;
            collid.isTrigger = true;
            slotFull = true;
        }
    }

    private void Update()
    {
        // Check if player is in range and "Left Mouse Button" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped /*&& distanceToPlayer.magnitude <= pickUpRange*/ && Input.GetButtonDown("Fire2") && !slotFull) Recall();

        // Drop if equipped and "Right Mouse Button" is pressed
        if (equipped && Input.GetButtonDown("Fire1")) Drop();

        // Restart scene when press "R"
        if (Input.GetKeyDown(KeyCode.R)) restartScene();

        // Restart run when pres "U"
        if (Input.GetKeyDown(KeyCode.U)) restartRun();
    }

    private void Recall()
    {
        equipped = true;
        slotFull = true;

        potSwayScript.enabled = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(potContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        transform.localScale = transform.localScale - sizeChange;

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        collid.isTrigger = true;

        //Play sound effect
        AudioManager.PlaySound("potRecall");
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        potSwayScript.enabled = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        collid.isTrigger = false;

        //Pot carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(chimera.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(chimera.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 75);

        //Play sound effect
        AudioManager.PlaySound("potThrow");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Collectible")
        {
            Debug.Log("ingredient acquired.");
            Destroy(collision.gameObject);

            //Play sound effect
            AudioManager.PlaySound("pickUp");

            itemsCollected++;

            Recall();
        }
        if (collision.collider.tag == "Door")
        {
            Debug.Log("I've hit a door");

            if (GameObject.FindWithTag("Collectible") == null)
            {
                Debug.Log("GOGOGOGO");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
    
    void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateItemList()
    {
        totalItems = GameObject.FindGameObjectsWithTag("Collectible").Length;
    }

    void restartRun()
    {
        SceneManager.LoadScene("Tutorial Level");

        Destroy(GameObject.Find("GameManager"));
        Destroy(GameObject.Find("Canvas"));
    }
}
