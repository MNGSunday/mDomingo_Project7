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

// The following class represents the concept of a level's exit within a game, and contains functions to simulate a locked and unlocked door.
public class ExitManager : MonoBehaviour
{
    public PlayerEntity PlayerInventory;
    public GameObject Player;

    public bool hasExited = false;

    private bool IsUnlocked = false;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInventory.hasKey == true)
        {
            this.GetComponent<SpriteRenderer>().color = Color.green;
            IsUnlocked = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && IsUnlocked == true)
        {
            Player.SetActive(false);
            hasExited = true;
            Debug.Log("The player has escaped!");
        }
    }
}
