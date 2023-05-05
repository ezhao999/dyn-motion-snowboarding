using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InitialBoost : MonoBehaviour
{
    // TODO: When a controller button is held,
    //       Add force in speicfic direction for
    //       specific duration of time. After that,
    //       disable the boost function.
    //       If controller button is let go, stop boosting.

    public Rigidbody rb;
    public bool isBoosted = false;
    public int duration = 144;
    public int magnitude = 2000;
    public int boostLeft;
    public bool held;
    //private bool lastHeld;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boostLeft = duration;
        //lastHeld = false;
    }

    // Update is called once per frame
    void Update()
    {
        // boost works when button pressed and held down for the FIRST TIME
        // else disable boost function entirely

        if (!isBoosted)
        {
            if (held)
            {
                isBoosted = !isBoosted; //set to true
            }
        } else
        {
            if (held && boostLeft > 0)
            {
                boost();
                boostLeft--;
            } else
            {
                boostLeft = 0;
            }
        }
    }

/*    void OnReset(InputAction.CallbackContext context)
    {
        Debug.Log("boost button detected");
        if (context.performed)
        {
            Debug.Log("boost button held");

            held = true;
        }
        if (context.canceled)
        {
            Debug.Log("boost button released");
            held = false;
        }
    }*/

    void OnBoostHeld()
    {
        Debug.Log("boost button held");
        held = true;
    }

    void OnBoostRelease()
    {
        Debug.Log("boost button released");
        held = false;
    }
    void boost()
    {
        rb.AddRelativeForce(Vector3.forward * magnitude);
    }
}
