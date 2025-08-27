using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameCanvas;
    private List<PlayerData> playersArray;
    public LoadPlayers loader = new LoadPlayers();
    private int playerCreated = 0;

    [Header("Cars Starting Point")]
    public GameObject startingPoint;

    [Header("Cars Management")]
    public string filepath = "/Scripts/Utils/data.txt";
    public int numberOfCars = 4;

    [Header("Relevant Info")]
    public int lapsToComplete = 0;
    public float spawnTime = 0.0f;

    [Header("Leader board")]
    //public List<Vector3> leaderBoardPos = new List<Vector3>();
    public List<GameObject> carObjects = new List<GameObject>();
    public List<GameObject> carInfoObjects = new List<GameObject>();

    public GameObject resetCanvas;
    public TextMeshPro winner;
    #region Base Functions

    void Awake()
    {
        CreatePlayersArray();
        gameCanvas = GameObject.Find("GameCanvas");
    }
    void Start()
    {
        StartCoroutine("InstantiatePlayers");
    }
    #endregion

    #region 
    void CreatePlayersArray()
    {
        loader.configGame(filepath, numberOfCars);
        lapsToComplete = loader.numberOfLaps;
        spawnTime = loader.miliSecondsDelay / 1000;
        playersArray = loader.playersArray;
    }
    IEnumerator InstantiatePlayers()
    {
        GameObject raceCar;
        AICarScript raceCarAI;
        GameObject playerCarData;
        PlayerInfoUI playerCarDataUI;

        if (playerCreated < playersArray.Count)
        {
            raceCar = Instantiate(Resources.Load("SkyCar")) as GameObject;
            raceCar.transform.position = startingPoint.transform.position;
            raceCarAI = raceCar.GetComponent<AICarScript>();
            raceCarAI.playerName = playersArray[playerCreated].name;
            raceCarAI.velocity = playersArray[playerCreated].velocity;
            raceCarAI.bodyColor = playersArray[playerCreated].bodyColor;
            raceCarAI.gameManager = gameObject.GetComponent<GameManager>();

            carObjects.Add(raceCar);
            //gameCanvas.GetComponent<GamePanelHandler>().createCard(raceCarAI);

            playerCreated++;

            yield return new WaitForSeconds(spawnTime);
            StartCoroutine("InstantiatePlayers");
        }
        else
        {
            playerCreated = 0;
            yield return new WaitForSeconds(0.000001f);
        }
    }

    public void CheckConditions(GameObject car, int lap, int remainingNode)
    {
        if (lap >= lapsToComplete)
        {
            CleanGameElements();
            winner.text += '\n' + car.GetComponent<AICarScript>().playerName;
            resetCanvas.SetActive(true);
        }
    }

    void CleanGameElements()
    {
        foreach (GameObject carObject in carObjects)
        {
            Destroy(carObject);
        }
        carObjects.Clear();

        foreach (GameObject carInfoObject in carInfoObjects)
        {
            Destroy(carInfoObject);
        }
        carInfoObjects.Clear();
    }
    public void ResetGame()
    {
        Debug.Log("Reset Function");
        CreatePlayersArray();
        resetCanvas.SetActive(false);
        Debug.Log("Re-creating assets");
        StartCoroutine("InstantiatePlayers");
    }
    #endregion
}
