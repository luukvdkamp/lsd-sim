using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : MonoBehaviour
{
    public float minDashReset;
    public float maxDashReset;
    private float dashResetTime;
    private float resetCounter;


    public float dashSpeed;
    public float dashDistance;

    public GameObject player;

    private bool ableToDash;

    public GameObject redPuf;

    private void Start()
    {
        dashResetTime = Random.Range(minDashReset, maxDashReset);
    }

    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        //dash
        resetCounter += Time.deltaTime;
        if (resetCounter > dashResetTime)
        {
            resetCounter = 0;
            dashResetTime = Random.Range(minDashReset, maxDashReset);
            StartCoroutine(DashTowardsPlayer());

            //activate spikes
            redPuf.SetActive(true);
        }
    }

    IEnumerator DashTowardsPlayer()
    {
        // Calculate the direction to dash towards
        Vector3 dashDirection = (player.transform.position - transform.position).normalized;

        // Calculate the destination point for the dash
        Vector3 dashDestination = transform.position + dashDirection * dashDistance;


        // Move towards the player with dash speed
        while (transform.position != dashDestination)
        {
            transform.position = Vector3.MoveTowards(transform.position, dashDestination, dashSpeed * Time.deltaTime);
            yield return null;
        }

        // Reset ableToDash
        ableToDash = false;

        //disable spikes
        redPuf.SetActive(false);
    }
}
