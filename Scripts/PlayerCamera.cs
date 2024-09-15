using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    GameObject cameraHolder;
    bool isStorm = false;
    // Start is called before the first frame update
    void Start()
    {
        cameraHolder = transform.GetChild(0).gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if(!isStorm)
        {
            if(GetComponent<PlayerMovement>().facingRight == true)
            {
                if (cameraHolder.transform.localPosition.x < 1.5)
                {
                    cameraHolder.transform.localPosition += new Vector3(5 * Time.deltaTime, 0);
                }
            }
            if (GetComponent<PlayerMovement>().facingRight == false)
            {
                if (cameraHolder.transform.localPosition.x > -1.5)
                {
                    cameraHolder.transform.localPosition += new Vector3(-5 * Time.deltaTime, 0);
                }
            }
        }
        if(isStorm)
        {

        }
    }
}
