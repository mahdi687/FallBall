using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class groundManager : MonoBehaviour
{
    public GameObject groundPrefab;
    public GameObject startGround;
    
    public bool groundFinishMove;
    public bool finishRandomGround;
    private List<GameObject> groundListForward = new List<GameObject> ();
    private List<GameObject> groundListBack = new List<GameObject> ();
    private Vector3 firstForwardPos;
    private Vector3 firstBackPos;

    public int numberOfGrounds;

    
    void Start()
    {   
        /*setting the position of the 2 ground (forward and back ) by calculating the ground z scale
        and add it to the first grounds postion*/
        firstForwardPos = startGround.transform.position + Vector3.forward * startGround.transform.localScale.z+new Vector3(0,-10,0);
        firstBackPos = startGround.transform.position + Vector3.back * startGround.transform.localScale.z+new Vector3(0, -10, 0);
        StartCoroutine(GroundForward(firstForwardPos, numberOfGrounds, groundListForward));
        StartCoroutine(GroundBack(firstBackPos, numberOfGrounds, groundListBack));
        StartCoroutine(MoveGround(groundPrefab, transform.position, startGround.transform.position, 2));

    }


    
    void Update()
    {
       
    }


    //no matter how much we spawn grounds they will be in the ground manager 
    IEnumerator GroundForward(Vector3 position , int number , List<GameObject> newList)
    {
        finishRandomGround = false;
        
            for (int i = 0; i < number; i++)
            {
                //creating new ground
                GameObject currentGround = Instantiate(groundPrefab, position, Quaternion.identity);
                //adding it to the list of Forwardgrounds
                newList.Add(currentGround);
                //putting the current ground in the groundManager 
                currentGround.transform.SetParent(startGround.transform.parent);
                //setting the position
                position = currentGround.transform.position + Vector3.forward * currentGround.transform.localScale.z;
                yield return new WaitForSeconds(.1f);
            if(ScoreManager.instance.Score==0)
                StartCoroutine(MoveGround(currentGround, currentGround.transform.position, currentGround.transform.position + new Vector3(0, 10f, 0), .5f));
            }
            finishRandomGround = true;
        
        
            
    }

    
    IEnumerator GroundBack(Vector3 position, int number, List<GameObject> newList)
    {
        finishRandomGround = false;
        for (int i = 0; i < number; i++)
        {
           
            GameObject currentGround = Instantiate(groundPrefab, position, Quaternion.identity);
            newList.Add(currentGround);
            currentGround.transform.SetParent(startGround.transform.parent);
            position = currentGround.transform.position + Vector3.back * currentGround.transform.localScale.z;
            yield return new WaitForSeconds(.1f);
            StartCoroutine(MoveGround(currentGround, currentGround.transform.position, currentGround.transform.position + new Vector3(0, 10f, 0), .5f));
        }

        
        finishRandomGround = true;
    }
   
    //google things again xD
    IEnumerator MoveGround(GameObject ground , Vector3 startPos,Vector3 endPos , float timeToMove)
    {
        float t = 0;
        while(t<timeToMove)
        {
            float fraction = t / timeToMove;
            ground.transform.position = Vector3.Lerp(startPos, endPos, fraction);
            t += Time.deltaTime;
            yield return null;
        }
        ground.transform.position = endPos;
        groundFinishMove=true;
    }
}
