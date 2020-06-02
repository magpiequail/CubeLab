using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    float halfScreen;
    public Camera leftCam;
    public Camera rightCam;
    public static Camera currentCam;
    

    // Start is called before the first frame update
    void Start()
    {
        halfScreen = Screen.width *0.5f;
        currentCam = leftCam;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetMouseButton(0))
        {
            DetectCurrentCamera();

            //Vector3 worldPoint = currentCam.ScreenToWorldPoint(Input.mousePosition);
            //RaycastHit2D hit = Physics2D.Raycast(worldPoint, transform.forward, 1000f);
            //Debug.DrawRay(worldPoint, transform.forward * 10, Color.red);
            
        }

        
    }
    private void DetectCurrentCamera()
    {
        if (Input.mousePosition.x < halfScreen)
        {
            currentCam = leftCam;
        }
        else
        {
            currentCam = rightCam;
        }
    }
    
}
