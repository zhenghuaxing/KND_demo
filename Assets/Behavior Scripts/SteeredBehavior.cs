﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/SteeredBehavior")]
public class SteeredBehavior : FilteredFlockBehavior
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbours, return no adjustment
        if (context.Count == 0)
            return Vector2.zero;

        //
        Vector2 coheisonMove = Vector2.zero;
		List<Transform> filteredContext = (filter == (null)) ? context : filter.Filtered(agent,context);
        foreach (Transform item in filteredContext)
        {
            coheisonMove += (Vector2)item.position;
        }

        coheisonMove /= context.Count;

        //offset from agent position
        coheisonMove -= (Vector2)agent.transform.position;
        coheisonMove = Vector2.SmoothDamp(agent.transform.up,coheisonMove,ref currentVelocity,agentSmoothTime);
        return coheisonMove;
    }
}
