using Openverse.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ClickRaycaster : MonoBehaviour
{
    public LayerMask hittable;
    public LayerMask clickable;
    public Vector2IntReference lastClickedTile;
    public Vector2IntReference currentlyHoveredTile;

    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
    }


    void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 50f, clickable))
            {
                Transform objectHit = hit.transform;
                IClickable clickable = objectHit.GetComponent<IClickable>();
                clickable?.onClick();
            }

            if (Physics.Raycast(ray, out hit, 50f, hittable))
            {
                Transform objectHit = hit.transform;
                GameTileController controller = objectHit.GetComponent<GameTileController>();
                lastClickedTile.Value = new Vector2Int(Mathf.FloorToInt(objectHit.position.x),Mathf.FloorToInt(objectHit.position.z));
                controller?.type?.clickOnTileEvent?.Raise();
            }
        } else
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 50f, hittable))
            {
                Transform objectHit = hit.collider.transform;
                currentlyHoveredTile.Value = new Vector2Int(Mathf.FloorToInt(objectHit.position.x), Mathf.FloorToInt(objectHit.position.z));
            }
        }
    }
}
