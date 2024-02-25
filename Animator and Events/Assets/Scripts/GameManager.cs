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

    private float YellowX, BlueX, RedX;
    private string winning;
    
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

    public void RestartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void EndGame()
    {
        InGameCanvas.enabled = false;
        EndCanvas.enabled = true;
        Time.timeScale = 0f;
    }

    public void WhosLeading()
    {
        YellowX = YellowPlayer.position.x;
        BlueX = BluePlayer.position.x;
        RedX = RedPlayer.position.x;

        if (YellowX < BlueX && YellowX < RedX)
        {
            winning = "yellow";
            Place.color = Color.yellow; Place.text = "Yellow In The Lead"; return;
            
        }
        else if (YellowX < BlueX && YellowX > RedX)
        {
            winning = "blue";
            Place.color = Color.blue; Place.text = "Blue In The Lead"; return;
        }
        else if (YellowX > BlueX && YellowX < RedX)
        {
            winning = "red";
            Place.color = Color.red; Place.text = "Red In The Lead"; return;
        }
        else if (YellowX > BlueX && YellowX > RedX)
        {
            if (BlueX < RedX) { winning = "blue"; Place.color = Color.blue; Place.text = "Blue In The Lead"; return; }
            else { winning = "red"; Place.color = Color.red; Place.text = "Red In The Lead"; return; }
        }

    }

    public void WhoWon(string whoWon)
    {
        winning = whoWon;
            switch (winning)
            {
                case "red": { WhoWonText.color = Color.red; break; }
                case "blue": { WhoWonText.color = Color.blue; break; }
                case "yellow": { WhoWonText.color = Color.yellow; break; }
                default: break;
            }
            EndGame();
            WhoWonText.text = $"{winning} Won";
    }

        
  
}
