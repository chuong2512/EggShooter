using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class CameraSize : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("map"))
        {
            Application.targetFrameRate = 60;
            float aspect = (float)Screen.height / (float)Screen.width;
            aspect = (float)Math.Round(aspect, 2);
            //Debug.Log("aspect map =" + aspect);
            if (aspect == 1.6f)
                GetComponent<Camera>().orthographicSize = 12.23f;                    //16:10
            else if (aspect == 1.78f)
                GetComponent<Camera>().orthographicSize = 13.6f;    //16:9
            else if (aspect == 1.5f)
                GetComponent<Camera>().orthographicSize = 11.46f;                  //3:2
            else if (aspect == 1.33f)
                GetComponent<Camera>().orthographicSize = 10.16f;                  //4:3
            else if (aspect == 1.67f)
                GetComponent<Camera>().orthographicSize = 12.68f;                  //5:3
            else if (aspect == 1.25f)
                GetComponent<Camera>().orthographicSize = 9.5f;                  //5:4
            else if (aspect == 2.06f)
                GetComponent<Camera>().orthographicSize = 15.7f;                  //2960:1440
            else if (aspect == 2.17f)
                GetComponent<Camera>().orthographicSize = 16.45f; //iphone x
            else if (aspect == 2.16f)
                GetComponent<Camera>().orthographicSize = 16.45f;
            else if (aspect == 2.11f)
                GetComponent<Camera>().orthographicSize = 16f;//iphone xsmax
            else if (aspect == 2.12f || aspect == 2.13f)
                GetComponent<Camera>().orthographicSize = 15.22f;
            else if (aspect == 2.09f)
                GetComponent<Camera>().orthographicSize = 15.22f;
            else if (aspect == 1.98f || aspect >= 2 && aspect <= 2.2)
                GetComponent<Camera>().orthographicSize = 15f;
            else if (aspect == 2f || aspect == 2.1f)
                GetComponent<Camera>().orthographicSize = 15.22f;    //OppoA73
        }

        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("game"))//1.2
        {
            Application.targetFrameRate = 60;
            float aspect = (float)Screen.height / (float)Screen.width;
            aspect = (float)Math.Round(aspect, 2);
            Debug.Log("aspect game =" + aspect);
            if (aspect == 2.06f)
                GetComponent<Camera>().orthographicSize = 8f;                  //2960:1440
            else if (aspect == 2.17f)
                GetComponent<Camera>().orthographicSize = 8.4f;
            else if (aspect == 2.11f)
                GetComponent<Camera>().orthographicSize = 7.9f;  //iphone x
            else if (aspect == 2f)
                GetComponent<Camera>().orthographicSize = 7.68f;
            else if (aspect == 1.98f || aspect >= 2 && aspect <= 2.2)
                GetComponent<Camera>().orthographicSize = 7.8f;
            //OppoA73 
        }
    }

}
