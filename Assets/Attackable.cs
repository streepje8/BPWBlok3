using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TileEntity))]
[RequireComponent(typeof(BoxCollider))]
public class Attackable : MonoBehaviour,IClickable
{
    private TileEntity myEntity;
    
    void Start()
    {
        myEntity = GetComponent<TileEntity>();
    }

    public void onClick()
    {
        if (PlayerStats.Instance.stamina > 0 && FaseManager.Instance.currentFase == GameFase.ATTACK)
        {
            myEntity.HP -= PlayerStats.Instance.BaseDamage + PlayerStats.Instance.AddedDamage;
            if (myEntity.HP <= 0) { myEntity.Die(); }
            PlayerStats.Instance.stamina -= 1;
        }
    }
}
