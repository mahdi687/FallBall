using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public UiManager uiManager;

    public bool startToRotateCamera=false;
    private bool cameraRotateFinish=true;

    private float rotateSpeed = 90;
    private float rotateAngle = 90;

    public int numberOfTurns;

   // public Color[] colors;
    void Start()
    {
     startToRotateCamera = false;
     cameraRotateFinish = true;
     StartCoroutine(RotateCamera());
     //GetComponent<Camera>().backgroundColor = colors[Random.Range(0,colors.Length)];
    }

   IEnumerator RotateCamera()
    {
        while(true)
        {
            if(ScoreManager.instance.Score%10==0 && ScoreManager.instance.Score!=0 && cameraRotateFinish)
            {
               // GetComponent<Camera>().backgroundColor = colors[Random.Range(0, colors.Length)];
                cameraRotateFinish = false;
                startToRotateCamera = true;
                FindObjectOfType<PlayerMouvement>().touchDisable = true;
                float CurrentSpeed = FindObjectOfType<ObstacleSpawner>().obstaclesSpeedMovement;
                FindObjectOfType<ObstacleSpawner>().obstaclesSpeedMovement = 0;
                float currentAngle = 0;
                while(currentAngle < rotateAngle)
                {
                    transform.RotateAround(Vector3.zero, Vector3.up, rotateSpeed * Time.deltaTime);
                    currentAngle += rotateSpeed * Time.deltaTime;
                    numberOfTurns++;
                    yield return null;
                }

                startToRotateCamera = false;
                cameraRotateFinish=true;
                FindObjectOfType<ObstacleSpawner>().obstaclesSpeedMovement=CurrentSpeed;
                FindObjectOfType<PlayerMouvement>().touchDisable = false;
            }
            yield return null;
        }
    }
}
