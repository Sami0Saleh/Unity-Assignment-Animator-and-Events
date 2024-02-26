using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Canvas StartCanvas;
    [SerializeField] Canvas EndCanvas;
    [SerializeField] Canvas InGameCanvas;

    [SerializeField] TextMeshProUGUI WhoWonText;
    [SerializeField] TextMeshProUGUI Place;

    [SerializeField] Transform RedPlayer;
    [SerializeField] Transform BluePlayer;
    [SerializeField] Transform YellowPlayer;

    private float YellowZ, BlueZ, RedZ;
    private string winning;
    private bool gameEnded = false;
    
    void Start()
    {
        Time.timeScale = 0;
        StartCanvas.enabled = true;
        EndCanvas.enabled = false;
        InGameCanvas.enabled = false;

        EventManager.WhosLeading += WhosLeading;
        EventManager.WhoWon += WhoWon;
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        StartCanvas.enabled = false;
        EndCanvas.enabled = false;
        InGameCanvas.enabled = true;
    }
    public void EndGame()
    {
        InGameCanvas.enabled = false;
        EndCanvas.enabled = true;
       
    }
    public void WhosLeading()
    {
        YellowZ = YellowPlayer.position.z;
        BlueZ = BluePlayer.position.z;
        RedZ = RedPlayer.position.z;

        if (YellowZ > BlueZ && YellowZ > RedZ)
        {
            winning = "yellow";
            Place.color = Color.yellow; Place.text = "Yellow In The Lead"; return;
            
        }
        else if (YellowZ < BlueZ && YellowZ > RedZ)
        {
            winning = "blue";
            Place.color = Color.blue; Place.text = "Blue In The Lead"; return;
        }
        else if (YellowZ > BlueZ && YellowZ < RedZ)
        {
            winning = "red";
            Place.color = Color.red; Place.text = "Red In The Lead"; return;
        }
        else if (YellowZ < BlueZ && YellowZ < RedZ)
        {
            if (BlueZ > RedZ) { winning = "blue"; Place.color = Color.blue; Place.text = "Blue In The Lead"; return; }
            else { winning = "red"; Place.color = Color.red; Place.text = "Red In The Lead"; return; }
        }

    }

    public void WhoWon(string[] whoWon)
    {
            winning = whoWon[0];
        if (!gameEnded)
        {
            switch (winning)
            {
                case "red": { WhoWonText.color = Color.red; break; }
                case "blue": { WhoWonText.color = Color.blue; break; }
                case "yellow": { WhoWonText.color = Color.yellow; break; }
                default: break;
            }
            WhoWonText.text = $"{winning} Won";
        }
            EndGame();           
            gameEnded = true;
    }
}
