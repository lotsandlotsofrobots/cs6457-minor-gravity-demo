using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public UnityEngine.SceneManagement.Scene scene;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        scene = gameObject.scene;
        rb = gameObject.GetComponent<Rigidbody>();
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

                    Debug.Log(t.name + " - Vector: " + v + ", Distance: " + d);

                    rb.AddForce(v, ForceMode.Force);
                    
                }
            }



        }
    }
}
