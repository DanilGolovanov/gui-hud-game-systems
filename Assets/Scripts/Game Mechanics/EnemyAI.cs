using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 5.0f;
    public GameObject[] waypoints;
    public float minDistance;
    public int index = 0;
    public float maxDistanceAI;
    public GameObject player;
    public float damagePlayer = 1;

    // Update is called once per frame
    void Update()
    {
        float distanceBetweenPlayerAndAI = Vector2.Distance(transform.position, player.transform.position);

        if (distanceBetweenPlayerAndAI < maxDistanceAI)
        {
            MoveAI(player.transform.position);
            if (GameManager.instance.playerIsAlive == true)
            {
                GameManager.instance.currentHealth -= damagePlayer * Time.deltaTime;
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        float distance = Vector2.Distance(transform.position, waypoints[index].transform.position);

        if (distance < minDistance)
        {
            index++;
        }
        if (index == waypoints.Length)
        {
            index = 0;
        }

        MoveAI(waypoints[index].transform.position);
    }

    void MoveAI(Vector2 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
