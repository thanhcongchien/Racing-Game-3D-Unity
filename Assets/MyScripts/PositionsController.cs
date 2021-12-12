using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using KartGame.KartSystems;

public class PositionsController : MonoBehaviour
{
    public PositionsController instance;
    public int totalPlayers;
    PlayerInfo[] allPlayerInfo;
    Text tableText;
    GameFlowManager flowManager;


    private void Awake()
    {
    }
    void Start()
    {
       //Start the coroutine we define below
        StartCoroutine(WaitSetupPositions());
        tableText = gameObject.GetComponent<Text>();
        tableText.text="Starting...";
        GameObject objManager = GameObject.Find("RaceGameManager");
        if(objManager!=null){
            flowManager = objManager.GetComponent<GameFlowManager>();
        }
    }

    void Update(){
        SetupPositions();
        DeterminePositions();
        bool isEndGame = flowManager.GetEndGame();
        if(isEndGame){
            SetFirstPositionPlayerName();
        }
    }

    IEnumerator WaitSetupPositions()
    {
        //yield on a new YieldInstruction that waits for 1 second.
        yield return new WaitForSeconds(1);
        SetupPositions();
    }
    void SetupPositions(){
		allPlayerInfo = FindObjectsOfType(typeof(PlayerInfo)) as PlayerInfo[];
        totalPlayers=allPlayerInfo.Length;
	}

    void DeterminePositions(){
        if(allPlayerInfo!=null){
            if(allPlayerInfo.Length>0){
                PlayerInfo[] ordered_Players = allPlayerInfo.OrderBy(i => i.currentPlayerLap).ToArray();
                int counter=1;
                tableText.text="";
                foreach (PlayerInfo order in ordered_Players){
                    tableText.text += "#" + counter + " - " + order.PlayerName + "\n";
                    //if (ordered_Players[0])
                    //{
                    //    firstRank.text = "#" + counter + " - " + order.PlayerName[0] + "\n";
                    //    firstRank.color = new Color(1f, 0.8431372549f, 0f);
                    //}
                    //if (ordered_Players.Length >= 2 && ordered_Players[1])
                    //{
                    //    secondRank.text = "#" + counter + " - " + order.PlayerName[1] + "\n";
                    //    secondRank.color = new Color(0.75294117647f, 0.75294117647f, 0.75294117647f);
                    //}
                    //else
                    //{
                    //    secondRank.text = "";
                    //}

                    //if (ordered_Players.Length >= 3 && ordered_Players[2])
                    //{
                    //    thirdRank.text = "#" + counter + " - " + order.PlayerName[2] + "\n";
                    //    thirdRank.color = new Color(1f,1f,1f);
                    //}
                    //else
                    //{
                    //    thirdRank.text = "";
                    //}

                    order.PlayerPosition=counter;
                    counter++;                    
                }
            }
        }
    }

    public void SetFirstPositionPlayerName(){
        if(allPlayerInfo!=null){
            if(allPlayerInfo.Length>0){
                PlayerInfo[] ordered_Players = allPlayerInfo.OrderBy(i => i.currentPlayerLap).ToArray();
                string nameFirstPosition=ordered_Players[0].PlayerName;
                ValuesBetweenScenes.NameOfWinner=nameFirstPosition;
            }
        }
    }


}
