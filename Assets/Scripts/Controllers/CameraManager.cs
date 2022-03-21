using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float DistanceFromPlayer = 3f;
    public float sensitifity = 5f;
    public FollowTarget followTarget;
    public float hightOffset = 2f;

    private Quaternion dollyRotation;
    private void Start()
    {
        dollyRotation = GameController.Instance.dollyParent.rotation;
    }

    void Update()
    {
        DistanceFromPlayer += Input.mouseScrollDelta.y;
        DistanceFromPlayer = Mathf.Clamp(DistanceFromPlayer, -30, 0);
        followTarget.distanceOfPlayer = Mathf.Clamp(Mathf.Log10(-DistanceFromPlayer) * 10,3,20);
        if (Input.GetMouseButton(1))
        {
            GameController.Instance.dollyParent.rotation = Quaternion.Slerp(GameController.Instance.dollyParent.rotation, dollyRotation, 10f * Time.deltaTime);
            dollyRotation *= Quaternion.Euler(Vector3.up * -Input.GetAxis("Mouse X") * Time.deltaTime * sensitifity);
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition,Vector3.forward * DistanceFromPlayer, 10f * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((followTarget.target.position - transform.position + (Vector3.up * hightOffset)).normalized), 10f * Time.deltaTime);
    }
}
