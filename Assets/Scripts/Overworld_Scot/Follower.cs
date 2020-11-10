﻿using GraphGrammars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Caravan leader;
    public Follower next;
    public float followDistance = 1f;

    public List<Node> path = new List<Node>();

    // Start is called before the first frame update
    void Start()
    {
        leader = FindObjectOfType<Caravan>();
        leader.followers.Add(this);
        if(leader.followers[0] != this)
        {
            next = leader.followers[leader.followers.IndexOf(this) - 1];
        }

    }

    void FixedUpdate()
    {
        if (next != null)
        {
            if (Vector2.Distance(this.transform.position, next.transform.position) > followDistance)
            {
                if (path.Count > 0)
                {
                    transform.position = Vector2.MoveTowards(transform.position, ((OasisNode)path[0]).getOasis().transform.position, 8f * Time.deltaTime);
                }
            }
        }
        else
        {
            if (Vector2.Distance(this.transform.position, leader.transform.position) > followDistance)
            {
                if (path.Count > 0)
                {
                    transform.position = Vector2.MoveTowards(transform.position, ((OasisNode)path[0]).getOasis().transform.position, 8f * Time.deltaTime);

                }

            }
        }

        if(path.Count > 0 && transform.position == ((OasisNode)path[0]).getOasis().transform.position)
        {
            path.RemoveAt(0);
        }

    }
}
