using Openverse.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public Vector2IntReference currentlyHoveredtile;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(currentlyHoveredtile.Value.x, transform.position.y, currentlyHoveredtile.Value.y), 10f * Time.deltaTime);
    }
}
