using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float DistanceFromPlayer = 3f;
    public float sensitifity = 5f;
    public FollowTarget followTarget;

    private Quaternion dollyRotation;
    private void Start()
    {
        dollyRotation = GameController.Instance.dollyParent.rotation;
    }

    void Update()
    {
        DistanceFromPlayer += Input.mouseScrollDelta.y;
        DistanceFromPlayer = Mathf.Clamp(DistanceFromPlayer, -30, 0);
        if (Input.GetMouseButton(1))
        {
            GameController.Instance.dollyParent.rotation = Quaternion.Slerp(GameController.Instance.dollyParent.rotation, dollyRotation, 10f * Time.deltaTime);
            dollyRotation *= Quaternion.Euler(Vector3.up * -Input.GetAxis("Mouse X") * Time.deltaTime * sensitifity);
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition,Vector3.forward * DistanceFromPlayer, 10f * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((followTarget.target.position - transform.position).normalized), 10f * Time.deltaTime);
    }
}
