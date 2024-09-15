using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAI : MonoBehaviour
{
    public GameObject player;
    public GameObject birdRest;
    private float ttAnimation = 0.5f;
    private bool spriteStateIdle;
    [SerializeField] private Sprite birdIdle;
    [SerializeField] private Sprite birdMove;
    [SerializeField] private float speed;
    private bool isRight = true;

    [SerializeField] private AudioClip move;
    private bool canPlay = false;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = player.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<StormManager>().isStorm)
        {
            canPlay = true;
            if(player.GetComponent<PlayerCollision>().hiding || player.GetComponent<PlayerCollision>().immune)
            {
                if (transform.position.x < player.transform.position.x+10 && isRight)
                {
                    transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, 10);
                }
                else if (transform.position.x >= player.transform.position.x + 10)
                {
                    isRight = false;
                }
                else if (transform.position.x <= player.transform.position.x - 10)
                {
                    isRight = true;
                }

                if (transform.position.x > player.transform.position.x - 10 && isRight == false)
                {
                    transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, 10);
                }
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, player.transform.position.y + 12, 10), speed * Time.deltaTime);

                if (isRight)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                if (!isRight)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                if (player.transform.position.x > transform.position.x)
                {
                    GetComponent<SpriteRenderer>().flipX = true;

                }
                if (player.transform.position.x < transform.position.x)
                {
                    GetComponent<SpriteRenderer>().flipX = false;

                }
            }
        }
        else
        {
            canPlay = false;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3 (player.transform.position.x, player.transform.position.y+20, player.transform.position.z), 2 * speed * Time.deltaTime);
        }
        if (ttAnimation > 0)
        {
            ttAnimation -= 1 * Time.deltaTime;
        }
        if (ttAnimation <= 0 && spriteStateIdle)
        {
            GetComponent<SpriteRenderer>().sprite = birdMove;
            spriteStateIdle = false;
            ttAnimation = 0.5f;
        }
        if (ttAnimation <= 0 && !spriteStateIdle)
        {
            GetComponent<SpriteRenderer>().sprite = birdIdle;
            spriteStateIdle = true;
            ttAnimation = 0.5f;
            if (canPlay)
            {
                source.PlayOneShot(move, 0.7f);
            }
        }

    }
}
