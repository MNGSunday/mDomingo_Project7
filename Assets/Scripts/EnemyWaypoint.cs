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

// The following class represents the concept of a patrol point in a game, and contains factors that affect an "enemy's" behavior at that point.
public class EnemyWaypoint : MonoBehaviour
{
    public bool IsEndpoint;
    public bool IsSentry = false;
    public float PauseTime = 1.0f;
}
