using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;
using Valve.VR.InteractionSystem; // include at top of script with other imports


public class HandGrab : MonoBehaviour
{
    public GameObject grabbedObject;
    public SteamVR_Input_Sources hand;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Input.GetStateUp("GrabPinch", hand))
        {
            Debug.Log(grabbedObject.tag);
            Debug.Log(grabbedObject.tag == "Grabbable");

            if (grabbedObject.tag == "Grabbable")
            { // && GrabbedObject != null) 
                /*
                GrabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                GrabbedObject.GetComponent<Rigidbody>().useGravity = true;
                GrabbedObject.transform.parent = null;
                


                Hand h = GetComponent<Hand>();
                Rigidbody toThrow = GrabbedObject.GetComponent<Rigidbody>();
                toThrow.velocity = h.trackedObject.GetVelocity();
                toThrow.angularVelocity = h.trackedObject.GetAngularVelocity();
                */

                SpringJoint joint = GetComponent<SpringJoint>();
                joint.connectedBody = null;

                Debug.Log("Let gooooooooo!");

                grabbedObject = null;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        // check if the trigger is pressed
        

        if (SteamVR_Input.GetStateDown("GrabPinch", hand))
        {
            Debug.Log("GRAB");

            if (other.tag == "Grabbable" && grabbedObject == null)
            {
                grabbedObject = other.gameObject;

                /*
                GrabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                GrabbedObject.GetComponent<Rigidbody>().useGravity = false;
                GrabbedObject.transform.parent = transform;
                */

                // attach other end of a spring component to an object
                SpringJoint joint = GetComponent<SpringJoint>();
                joint.connectedBody = grabbedObject.GetComponent<Rigidbody>();
            }
        }
    }
}
