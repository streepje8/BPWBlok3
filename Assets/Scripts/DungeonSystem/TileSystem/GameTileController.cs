using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * GameTileController
 * Wessel Roelofse
 * 01/04/2022
 * 
 * A script that controles the game tiles on runtime
 */
public class GameTileController : MonoBehaviour
{
    public GameTile type;
    public byte NeighborCode;
    [HideInInspector]public Material myMaterial;

    [HideInInspector]public bool containsEntity = false;

    public void roomGenerated()
    {
        if(myMaterial == null)
        {
            MeshRenderer mr = GetComponent<MeshRenderer>();
            myMaterial = new Material(mr.material);
            mr.material = myMaterial;
        }
        myMaterial.SetTexture("_MainTex", type.texture);
    }
}
