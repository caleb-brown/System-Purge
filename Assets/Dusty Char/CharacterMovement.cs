using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour {

    public bool jump = false;
    public bool grounded = true;
    public bool foundThing = false;
    public bool canPass = false;
    public bool changeBox = false;
    public bool isStarted = false;
 
    private float timeRemainingTransparency = 5; 
    private float timeRemainingTransfer = 5;
    private float checkTimeRemaining = 5;
    

    public Transform sightStart, sightEnd;
    public float secondBoxX, secondBoxY;
    public float firstBoxX, firstBoxY;

    // Use this for initialization
    void Start ()
    {
        //StartCoroutine(myCoroutine());
	}
	
    IEnumerator myCoroutine()
    {
        //print("wasssssup");
        yield return new WaitForSeconds(3f);
        //print("now what?");
    }

	// Update is called once per frame
	void Update ()
    {
        //Movement();
        powerTransparency();
        
        powerTransfer();
       // StartCoroutine(myCoroutine());
       // checkTimers();
      //  print("transfer time: " + timeRemainingTransfer);
      //  print("check time: " + checkTimeRemaining);
       // print("IS starting: " + isStarted);
	}

    public void Movement()
    {
        //Is going to handle all the character movement
        //if(Input.GetKey(KeyCode.D))
        //{
        //    transform.Translate(Vector2.right * 4f * Time.deltaTime);  //Makes it to where the player can travel to the right
        //    transform.eulerAngles = new Vector2(0,0); //this allows the "rotation" of the sprite, facing right / left
        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.Translate(Vector2.left * -4f * Time.deltaTime);  //Makes it to where the player can travel to the left
        //    transform.eulerAngles = new Vector2(0, 180);
        //}

        if (Input.GetKeyDown(KeyCode.T))
        {
            powerTeleport();
        }
            

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (canPass == true)
                canPass = false;
            else
                canPass = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if(SceneManager.GetActiveScene().name == "GMLevel1")
            {
                StartCoroutine(GameManager.gManager.SetGameStateAndLoad("GMLevel2"));
            }
            else
            {
                StartCoroutine(GameManager.gManager.SetGameStateAndLoad("GMLevel1"));
            }
        }
            

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (changeBox == true)
                changeBox = false;
            else
                changeBox = true;
        }
                                             
    }

    public void powerTransfer()
    {
        //Transfer (enabling the player to "transfer" thier "hit" box behind them)
        if (changeBox == true  && timeRemainingTransfer >= 0.0 )
        {        
            GetComponent<BoxCollider2D>().offset = new Vector2(secondBoxX, secondBoxY);
            timeRemainingTransfer -= Time.deltaTime;                 
        }
       
        else
        {
            GetComponent<BoxCollider2D>().offset = new Vector2(firstBoxX, firstBoxY);
            changeBox = false;
            timeRemainingTransfer = 5;
        }
            
            
    }

    public void powerTransparency()
    {
        //Transparency (enabling the player to "glitch" through certain boundaries.)
        if (canPass == true && timeRemainingTransparency >= 0.0)
        {
            Physics2D.IgnoreLayerCollision(8, 10, true);  //Make sure that the "barriers" that the player needs to "phase" through are part of the barrier layer.
            timeRemainingTransparency -= Time.deltaTime;
        }
        else
        {
            canPass = false;
            Physics2D.IgnoreLayerCollision(8, 10, false);
            timeRemainingTransparency = 5;
        }
        //still need to work on "re increasing" the time remaining variable.
    }

     public void powerTeleport()
    {
        //Teleportation (enabling teh player to teleport to a location ahead of him.)
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.red);

        foundThing = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Bounding Wall")); //Pretty much saying if the cast hits a bounding wall, will not enable teleportation
        if (foundThing)
            transform.position = new Vector2(sightStart.position.x, sightStart.position.y); //Player doesn't move (ish)

        else
            transform.position = new Vector2(sightEnd.position.x, sightEnd.position.y);     //Player moves to the end of the "ray cast"
    }

/*    void checkTimers()
    {
        if (timeRemainingTransparency <= 0.0 && checkTimeRemaining >= 0.0 && isStarted == false)
        {
            isStarted = true;
            timeRemainingTransparency += Time.deltaTime;
            checkTimeRemaining -= Time.deltaTime;
        }
        

        if (timeRemainingTransfer < 0.0 && checkTimeRemaining >= 5 /*&& isStarted == false)
        {
            isStarted = true;
            timeRemainingTransfer += Time.deltaTime;
            checkTimeRemaining -= Time.deltaTime;
        }
        isStarted = false;


    }*/

}
