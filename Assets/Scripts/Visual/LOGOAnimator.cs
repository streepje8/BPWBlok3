using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOGOAnimator : MonoBehaviour
{
    public float yoffset = 1f;
    public float rotationoffset = 1;
    public float scaleoffset = 1;

    private Vector3 originalPos;
    private void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        transform.localPosition = originalPos + Vector3.up * Mathf.Sin(Time.time) * yoffset;
        transform.localRotation = Quaternion.Euler(0,0,Mathf.Sin(Time.time * 2) * rotationoffset);
        transform.localScale = Vector3.one * ((1 + Mathf.Sin(Time.time) + scaleoffset) / 2);
    }
}
