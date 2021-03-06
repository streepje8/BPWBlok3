using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * DroppedItem
 * Wessel Roelofse
 * 01/04/2022
 * 
 * A script to control dropped items in the game scene.
*/
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(BoxCollider))]
public class DroppedItem : MonoBehaviour, IClickable
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
        GetComponent<MeshRenderer>().material = stack.type.material;
        endPosition = transform.position;
        transform.Translate(Vector3.up * 2f);
        GameController.Instance.RegisterEntity(gameObject);
    }

    public void onClick()
    {
        if (FaseManager.Instance.currentFase == GameFase.INTERACT)
        {
            if (Vector3.Distance(transform.position,GameController.Instance.player.transform.position) < 2f)
            {
                GameController.Instance.inventory.inventory.Value.addItem(stack);
                GameController.Instance.inventory.UpdateDisplays();
                GameController.Instance.DeRegisterEntity(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
