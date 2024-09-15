using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormManager : MonoBehaviour
{
    public int timeToStorm = 30;
    public int timeInStorm = 30;
    public bool canStart = true;
    public bool isStorm = false;
    public bool canEnd = false;


    //private GameObject spriteHolder;
    private SpriteRenderer flash;
    private bool canFlash;
    private float flashTime = 0.3f;
    private SpriteRenderer stormSprite;
    //IEnumerator beforeStorm;
    // Start is called before the first frame update
    void Start()
    {
        //spriteHolder = transform.GetChild(0).GetChild(0).gameObject;
        flash = transform.GetChild(0).GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        stormSprite = transform.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        stormSprite.enabled = false;
        flash.enabled = false;
    }
    IEnumerator beforeStorm() {
        canStart = false;
        timeToStorm--;
        if (timeToStorm == 0)
        {
            isStorm = true;
            canEnd = true;
            timeInStorm = 60;
        }
        yield return new WaitForSeconds(1f);
        canStart = true;
    }
    IEnumerator storm()
    {
        canEnd = false;
        timeInStorm--;
        if (timeInStorm == 0)
        {
            isStorm = false;
            canStart = true;
            timeToStorm = 15;
        }
        yield return new WaitForSeconds(1f);
        canEnd = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (canStart == true && isStorm == false)
        {
            stormSprite.enabled = false;
            canEnd = false;
            StartCoroutine(beforeStorm());
        }
        if (canEnd == true && isStorm)
        {
            stormSprite.enabled = true;
            canStart = false;
            StartCoroutine(storm());
        }
        if (isStorm && canFlash)
        {
            flash.enabled = true;
            flashTime -= 1 * Time.deltaTime;
            if (flashTime <= 0)
            {
                canFlash = false;
                flash.enabled = false;
                flashTime = 0.3f;
            }
        }
        if (!isStorm)
        {
            canFlash = true;
        }
    }
    
}
