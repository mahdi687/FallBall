using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject Gold;
    public float timerSpawn;
    public float obstaclesSpeedMovement;
    public int speedAdding;

    private List<Vector3> listPostion = new List<Vector3>();
    void Start()
    {
        int i = -5;
        while(i<=5)
        {
            Vector3 pos = transform.position + new Vector3(0, 0, 1);
            listPostion.Add(pos);
            i += 2;
        }
        StartCoroutine(spawnObstacles());
    }

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<PlayerMouvement>().gameOver)
        {
            obstaclesSpeedMovement = 15;
        }
        if (FindObjectOfType<ScoreManager>().Score % 20 == 0 && FindObjectOfType<ScoreManager>().Score != 0 && speedAdding==0 && obstaclesSpeedMovement < 30)
        {
            speedAdding++;
            FindObjectOfType<PlayerMouvement>().playerSpeed += 0.5f;
            obstaclesSpeedMovement +=1.5f;
        }
        if(FindObjectOfType<ScoreManager>().Score % 20 != 0)
            speedAdding=0;
    }

    IEnumerator spawnObstacles()
    {
        yield return new WaitForSeconds(timerSpawn);

        if (FindObjectOfType<groundManager>().groundFinishMove && !FindObjectOfType<PlayerMouvement>().gameOver)

        {   

            int indexPositionOfObstacle = Random.Range(0,listPostion.Count);
            if(Random.value<=.3)
            {
                int indexPositionOfGold = Random.Range(0, listPostion.Count);
                while(indexPositionOfGold == indexPositionOfObstacle)
                {
                    indexPositionOfGold = Random.Range(0, listPostion.Count);
                }
                Instantiate(Gold, listPostion[indexPositionOfGold]+new Vector3(5,0,Random.Range(-3,3)), Quaternion.identity);
               
            }
            
            

            GameObject currentObstacle = Instantiate(obstacles[Random.Range(0, obstacles.Length)],
                listPostion[indexPositionOfObstacle]+new Vector3(10, 0, Random.Range(-3,3)), Quaternion.identity);
            StartCoroutine(spawnObstacles());

        }
    }
}
