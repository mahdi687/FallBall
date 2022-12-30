using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesMouvement : MonoBehaviour
{

    void Update()
    {
        transform.position = transform.position + Vector3.left * FindObjectOfType<ObstacleSpawner>().obstaclesSpeedMovement * Time.deltaTime;
        StartCoroutine(Destroy());
    }
  

    IEnumerator Destroy()
    {
     
        yield return new  WaitForSeconds(3);
        Destroy(gameObject);
    }
}
