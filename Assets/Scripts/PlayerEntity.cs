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

// The following class represents the concept of a player character in a stealth game, and contains functionality to simulate the player's movement and "death."
public class PlayerEntity : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 0.1f;
    public bool hasKey = false;
    public GameObject Key;
    public Vector3 defaultPosition;

    private float clickTime;
    private int clickCount = 0;
    private const float DOUBLE_CLICK_TIME_LIMIT = 0.5f;
    private const float DASH_TIME_LIMIT = 1.5f;
    private float dashTime;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseInSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            clickCount++;
            StopAllCoroutines();
            StartCoroutine(SingleOrDoubleClick(transform.position, mouseInSpace, speed));
        }
    }

    IEnumerator SingleOrDoubleClick(Vector3 start, Vector3 destination, float speed)
    {
        clickTime = 0f;
        while (clickTime < DOUBLE_CLICK_TIME_LIMIT)
        {
            if (Input.GetMouseButtonDown(0) && clickCount >= 2)
            {
                StartCoroutine(DashTo(start, destination, speed));
                yield break;
            }
            clickTime += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(MoveTo(start, destination, speed));
        Debug.Log("Only one click detected. Moving at normal speed.");
        clickCount = 0;
    }

    IEnumerator MoveTo(Vector3 start, Vector3 destination, float speed)
    {
        while ((transform.position - destination).sqrMagnitude > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator DashTo(Vector3 start, Vector3 destination, float speed)
    {
        clickCount = 0;
        Debug.Log("Double click detected, commence dash.");
        dashTime = 0;
        float dashSpeed = 2f * speed;
        while (dashTime <= DASH_TIME_LIMIT)
        {
            while ((transform.position - destination).sqrMagnitude > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, destination, dashSpeed * Time.deltaTime);
                yield return null;
            }
            dashTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Dash has run out.");

        while ((transform.position - destination).sqrMagnitude > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void resetPosition()
    {
        transform.position = defaultPosition;
        hasKey = false;
        Key.SetActive(true);
        Debug.Log("Your position is reset");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            StopAllCoroutines();
            this.resetPosition();
        }
    }
}
