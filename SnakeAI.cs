using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAI : MonoBehaviour
{
    public GameObject player;
    public GameObject birdRest;
    public int speed;

    private float ttAnimation = 0.5f;
    private bool spriteStateIdle;
    [SerializeField] private Sprite snakeMove;
    [SerializeField] private Sprite snakeIdle;


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
        if (player.transform.position.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;

        }
        if (player.transform.position.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;

        }
        if (player.GetComponent<StormManager>().isStorm == true)
        {
            canPlay = true;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            canPlay = false;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 20, player.transform.position.z), 2 * speed * Time.deltaTime);

        }
        if (ttAnimation > 0)
        {
            ttAnimation -= 1 * Time.deltaTime;
        }
        if (ttAnimation <= 0 && spriteStateIdle)
        {
            GetComponent<SpriteRenderer>().sprite = snakeMove;
            spriteStateIdle = false;
            ttAnimation = 0.5f;
        }
        if (ttAnimation <= 0 && !spriteStateIdle)
        {
            GetComponent<SpriteRenderer>().sprite = snakeIdle;
            spriteStateIdle = true;
            ttAnimation = 0.5f;
            if (canPlay)
            {
                source.PlayOneShot(move, 0.7f);
            }
        }
    }
}
