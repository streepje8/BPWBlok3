using Openverse.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : TileEntity
{
    public List<ItemType> drops = new List<ItemType>();
    public List<int> dropAmounts = new List<int>();
    public Vector2IntReference lastClickedTile;

    private void Start()
    {
        RegisterSelf();
    }

    public void chestTileClickedEvent()
    {
        if(lastClickedTile.Value.x == transform.position.x && lastClickedTile.Value.y == transform.position.z)
        {
            onClickedEvent();
        }
    }

    public override void SpawnAt(int x, int y)
    {
        //Do nothing
    }

    void onClickedEvent()
    {
        if (FaseManager.Instance.currentFase == GameFase.INTERACT)
        {
            if (Vector3.Distance(transform.position, GameController.Instance.player.transform.position) < 2f)
            {
                int theItem = Random.Range(0, drops.Count);
                drops[theItem].Drop(new ItemStack(drops[theItem], dropAmounts[theItem]), transform.position);
                Destroy(gameObject);
            }
        }
    }
}
