using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;

        //lerp는 스무스하게 이동하게 하는 함수
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);


    }
}
