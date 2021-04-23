/*
Name: Marc Domingo
Student ID: 2346778
Chapman Email: mdomingo@chapman.edu
Course Number and Section: 236-03
Assignment: Project 7
This is my own work, and I did not cheat on this assignment.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The following class represents the concept of a game's UI, and contains functions for each of the buttons on the UI in Unity.
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public PlayerEntity PlayerStatus;
    public ExitManager ExitStatus;

    private Vector2 playerStartPosition = new Vector2(11.31f, 0f);

    public GameObject playButton;
    public GameObject quitButton;
    public GameObject trueQuitButton;


    void Start()
    {
        Player.SetActive(false);
        Debug.Log("Press 'Play' to Start!");
    }

    private void Update()
    {
        if (ExitStatus.hasExited == true)
        {
            playButton.SetActive(true);
        }
    }

    public void OnPlayButtonPress()
    {
        ExitStatus.hasExited = false;
        Player.SetActive(true);
        PlayerStatus.defaultPosition = playerStartPosition;
        PlayerStatus.resetPosition();
        trueQuitButton.SetActive(false);
        playButton.SetActive(false);
    }

    public void OnQuitButtonPress()
    {
        ExitStatus.hasExited = false;
        PlayerStatus.resetPosition();
        Player.SetActive(false);
        trueQuitButton.SetActive(true);
        playButton.SetActive(true);
        Debug.Log("You have quit the game, but can press 'Play' to retry.");
    }

    public void OnTrueQuitButtonPress()
    {
        PlayerStatus.resetPosition();
        Player.SetActive(false);
        playButton.SetActive(false);
        quitButton.SetActive(false);
        Debug.Log("You have quit the application.");
        Application.Quit();
    }

   
}
