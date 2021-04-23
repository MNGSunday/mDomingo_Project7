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

// The following class represents the concept of an enemy in a stealth game, and contains functions to simulate the behavior of an enemy when spotting a player.
public class EnemyController : MonoBehaviour
{
    public List<EnemyWaypoint> Waypoints = new List<EnemyWaypoint>();
    public float speed = 1.0f;
    public int DestinationWaypoint = 1;

    public GameObject PlayerEntity;
    public PlayerEntity Player;
    public int PlayerIndex = 0;
    public ExitManager ExitStatus;

    private Vector3 Destination;
    private bool Forwards = true;
    private float TimePassed = 0f;

    private Vector3 PlayerPosition;
    private bool huntPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        this.Destination = this.Waypoints[DestinationWaypoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        StopAllCoroutines();
        StartCoroutine(MoveTo());
        StartCoroutine(CheckForPlayer());

        // If the player has successfully beaten the avoiver game, return to patrol.
        if (ExitStatus.hasExited == true)
        {
            huntPlayer = false;
        }

        // If the player was caught and has been sent back to starting position, return to patrol.
        if (PlayerEntity.transform.position == Player.defaultPosition)
        {
            huntPlayer = false;
        }
    }

    IEnumerator CheckForPlayer()
    {
        this.PlayerPosition = this.Player.transform.position;
        if ((transform.position - this.PlayerPosition).sqrMagnitude < 5f)
        {
            huntPlayer = true;
        }

        yield return null;
    }
    IEnumerator MoveTo()
    {
        if (huntPlayer == true)
        {
            PlayerPosition = Player.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, this.PlayerPosition, this.speed * Time.deltaTime);
            yield return null;
        }

        else
        {
            while ((transform.position - this.Destination).sqrMagnitude > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    this.Destination, this.speed * Time.deltaTime);
                yield return null;
            }

            if ((transform.position - this.Destination).sqrMagnitude < 0.01f)
            {
                if (this.Waypoints[DestinationWaypoint].IsSentry)
                {
                    while (this.TimePassed < this.Waypoints[DestinationWaypoint].PauseTime)
                    {
                        this.TimePassed += Time.deltaTime;
                        yield return null;
                    }

                    this.TimePassed = 0f;
                }
            }
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (this.Waypoints[DestinationWaypoint].IsEndpoint)
        {
            if (this.Forwards)
                this.Forwards = false;
            else
                this.Forwards = true;
        }

        if (this.Forwards)
            ++DestinationWaypoint;
        else
            --DestinationWaypoint;

        if (DestinationWaypoint >= this.Waypoints.Count)
            DestinationWaypoint = 0;

        this.Destination = this.Waypoints[DestinationWaypoint].transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            huntPlayer = false;
            Player.resetPosition();
        }
    }
}
