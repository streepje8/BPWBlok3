using Openverse.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * MouseCursor
 * Wessel Roelofse
 * 01/04/2022
 * 
 * This script moves the mouse cursor on the tiles
 */
public class MouseCursor : MonoBehaviour
{
    public Vector2IntReference currentlyHoveredtile;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(currentlyHoveredtile.Value.x, transform.position.y, currentlyHoveredtile.Value.y), 10f * Time.deltaTime);
    }
}
