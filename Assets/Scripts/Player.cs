using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public UnityEngine.SceneManagement.Scene scene;
    Rigidbody rb;
    public bool applyGravityCube = true;

    public Vector3 extraGravityForceVector;

    // Start is called before the first frame update
    void Start()
    {
        scene = gameObject.scene;
        rb = gameObject.GetComponent<Rigidbody>();
        //rb.AddForce(new Vector3(0f, 1000f, 0f), ForceMode.Force);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sumOfForces = new Vector3(0, 0, 0);
        

        foreach (GameObject g in scene.GetRootGameObjects())
        {
            if (g.name == "ExtraHeavyGravityCubes")
            {
                for (int i = 0; i < g.transform.childCount; i++)
                {
                    //Debug.Log("Heavy cube: " + g.transform.GetChild(i).transform.position);
                    
                    Transform t = g.transform.GetChild(i);
                    Vector3 v = transform.position - t.position;
                    v.Normalize();

                    float d = Vector3.Distance(transform.position, t.position);

                    float gravity = 9.81f * ((t.transform.GetComponent<Rigidbody>().mass) / (d * d));

                    v *= -gravity;// * 2500;

                    //Debug.Log(t.name + " - Vector: " + v + ", Distance: " + d);
                    if (applyGravityCube)
                    {
                        rb.AddForce(v, ForceMode.Force);
                        //Debug.Log("velocity: " + rb.velocity);
                    }

                    rb.AddForce(extraGravityForceVector, ForceMode.Force);

                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        const int antiGravityLayer = 7;
        //if (other.material.name == "AntiGravityZone")
        if (other.gameObject.layer == antiGravityLayer)
        {
            Debug.Log("Inside antigrav zone.");
            rb.useGravity = false;

            //rb.AddForce(new Vector3(0, 100, 0), ForceMode.Force);
            extraGravityForceVector = new Vector3(0, 1, 0);
        }
        else
        {
            Debug.Log("enter other material: " + other.material.name);
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        const int antiGravityLayer = 7;
        if (other.gameObject.layer == antiGravityLayer)
        {
            Debug.Log("Left antigrav zone.");
            rb.useGravity = true;
            extraGravityForceVector = new Vector3(0, 0, 0);

        }
        else
        {
            Debug.Log("left other material: " + other.material.name);
        }
    }
}
