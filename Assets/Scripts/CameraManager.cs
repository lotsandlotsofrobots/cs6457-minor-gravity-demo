using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
    public Camera camera;
    public GameObject prefab;

    private bool mouseClick;

    // Start is called before the first frame update
    void Start()
    {
        mouseClick = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseClick = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(mouseClick)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                Debug.Log("objectHit: " + hit.transform.position);
                Debug.Log("objectHit Point: " + hit.point);
                // Do something with the object that was hit by the raycast.

                GameObject parent = GameObject.Find("AntiGravityZone");
                GameObject g = Instantiate(prefab, hit.point, Quaternion.identity, parent.transform);
                
                
            }

            mouseClick = false;
        }


    }


}
