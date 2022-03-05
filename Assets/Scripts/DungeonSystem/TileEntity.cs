using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileEntity : MonoBehaviour
{
    public abstract void SpawnAt(int x, int y);
}
