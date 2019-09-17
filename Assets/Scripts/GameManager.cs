using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform platformGenerator;
    private Vector3 platformStartPoint;
    private Vector3 playerStartPoint;
    public PlayerController thePlayer;
    public DeathMenu deathScreen;

    private PlatformDestroyer[] platformList;

    private ScoreManager theScoreManager;

    // Start is called before the first frame update
    void Start()
    {
        platformStartPoint = platformGenerator.position;
        playerStartPoint = thePlayer.transform.position;
        theScoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartGame()
    {
        theScoreManager.scoreIncreasing = false;

        deathScreen.gameObject.SetActive(true);
    
        //StartCoroutine("RestartGameCo");
    }
    public void Reset()
    {
        deathScreen.gameObject.SetActive(false);
        platformList = FindObjectsOfType<PlatformDestroyer>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }
        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        theScoreManager.scoreCount = 0;
        theScoreManager.scoreIncreasing = true;
    }
    /*  public IEnumerator RestartGameCo()
      {
          theScoreManager.scoreIncreasing = false;
          //thePlayer.gameObject.SetActive(false);  
          yield return new WaitForSeconds(0.6f);
          platformList = FindObjectsOfType<PlatformDestroyer>();
          for(int i = 0; i< platformList.Length;i++)
          {
              platformList[i].gameObject.SetActive(false);
          }
          thePlayer.transform.position = playerStartPoint;
          platformGenerator.position = platformStartPoint;
          theScoreManager.scoreCount = 0;
          theScoreManager.scoreIncreasing = true;

          //thePlayer.gameObject.SetActive(true);
          }*/

}
