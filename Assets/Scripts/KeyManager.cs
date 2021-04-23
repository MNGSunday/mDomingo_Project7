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

// The following class represents the concept of a key within a game, and contains functions to simulate a key being "collected" in a level. 

public class KeyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerEntity Player;
    public GameObject Key;
    void Start()
    {
        Player.hasKey = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.hasKey = true;
            Key.SetActive(false);
            Debug.Log("The player has grabbed the key. Go for the exit!");
        }
    }
}
