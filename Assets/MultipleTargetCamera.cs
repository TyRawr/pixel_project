using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour {


    public List<Transform> targets;
    public Vector3 offset;
    public float smoothTime = .5f;
    public float minZoom = 60f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    private Vector3 velocity;
    private Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    public void LateUpdate()
    {
        if (targets.Count == 0) return;
        Move();
        Zoom();
    }

    float GetGreatestDistance()
    {
        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom,  GetGreatestDistance() / zoomLimiter);
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, newZoom, Time.deltaTime);
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        if(targets.Count <= 1)
        {
            return targets[0].position;
        }

        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        for( int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
}
