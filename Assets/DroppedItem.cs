using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(BoxCollider))]
public class DroppedItem : MonoBehaviour
{
    public ItemStack stack;
    private Vector3 endPosition;
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, endPosition, 1f * Time.deltaTime);
    }

    internal void Drop()
    {
        GetComponent<MeshFilter>().mesh = stack.type.mesh;
        endPosition = transform.position;
        transform.Translate(Vector3.up);
    }
}
