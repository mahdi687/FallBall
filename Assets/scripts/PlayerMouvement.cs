using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMouvement : MonoBehaviour
{
    public float playerSpeed;
    public AudioSource song;
    private bool Move;
    public bool gameOver=false, touchDisable=false;
    private Vector3 direction;
    public ParticleSystem particle;

    private void Start()
    {
        song.Play();
    }
    void Update()
    {

        //player mouvement
        if (Input.GetMouseButtonDown(0) && FindObjectOfType<groundManager>().groundFinishMove && !touchDisable)
        {
            Move = !Move;
            
                if (Move)
                {
                    direction = Vector3.forward;
                }
                else
                    direction = Vector3.back;
        }
        if(!FindObjectOfType<cameraController>().startToRotateCamera)
            transform.position = transform.position + direction * playerSpeed * Time.deltaTime;
        

        //yay raycast ( terrible script but it works)
       RaycastHit hit;
         Ray rayDown = new Ray(transform.position,Vector3.down);
         if(!Physics.Raycast(rayDown,out hit,1f))
         {
            Debug.Log("out of range");
             gameOver = true;
             touchDisable = true;
             playerSpeed = 20;
             direction = Vector3.down;
         }
        

     }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Obstacle")
        {
            gameOver = true;
            touchDisable = true;
            direction = Vector3.left;
        }
      
        //tutorial about particales xD
        if(other.tag=="Gold")
        {
            ScoreManager.instance.AddScore(3);
            ParticleSystem goldParticle;
            goldParticle = Instantiate(particle, other.transform.position, Quaternion.identity);
            //simulating will play the particale from a specific spot ( .5 means half)
            goldParticle.Simulate(.5f, true, false);
            goldParticle.Play();
            Destroy(goldParticle.gameObject,.5f);
            Destroy(other.gameObject);
        }
    }

}

