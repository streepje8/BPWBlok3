using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTileController : MonoBehaviour
{
    public GameTile type;
    public byte NeighborCode;
    private Material myMaterial;

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
