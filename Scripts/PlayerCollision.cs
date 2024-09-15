using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public Vector2 resetPoint;
    public bool hiding = false;
    public bool immune;
    private float immuneTime = 5;

    [SerializeField] private AudioClip finish;
    [SerializeField] private AudioClip checkpoint;
    [SerializeField] private AudioClip hit;

    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (immune)
        {
            immuneTime -= 1 * Time.deltaTime;
        }
        if (immuneTime <= 0 && immune)
        {
            immune = false;
            immuneTime = 5;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.tag == "Checkpoint")
        {
            resetPoint = collision.transform.position;
        }
        if (collision.tag == "Killzone")
        {
            resetPlayer();
        }
        if (collision.tag == "Bush")
        {
            Debug.Log("hiding");
            hiding = true;
        }*/

        switch (collision.tag)
        {
            case "Checkpoint":
                resetPoint = collision.transform.position;
                source.PlayOneShot(checkpoint, 0.7f);
                break;
            case "Killzone":
                resetPlayer();
                break;
            case "Enemy":
                if (immune == false)
                {
                    resetPlayer();
                    immune = true;
                }
                break;
            case "Bush":
                Debug.Log("hiding");
                hiding = true;
                break;
            case "Finish":
                source.PlayOneShot(finish, 0.7f);
                SceneManager.LoadScene(0);
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("triggerLeft");
        if (collision.tag == "Bush")
        {
            Debug.Log("exited");
            hiding = false;
        }
    }

    void resetPlayer()
    {
        transform.position = resetPoint;
        source.PlayOneShot(hit, 0.7f);
    }
}
