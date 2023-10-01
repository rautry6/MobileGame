using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class ARSlimeMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject slime;


    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CheckPositions();
    }

    public void CheckPositions()
    {
        float newY = float.MaxValue;

        if (raycastManager.Raycast(slime.transform.position, hits, TrackableType.PlaneWithinPolygon))
        {
            foreach (ARRaycastHit hit in hits)
            {
                if (planeManager.GetPlane(hit.trackableId).alignment == PlaneAlignment.HorizontalUp)
                {
                    Pose pose = hit.pose;

                    if (pose.position.y +0.7f < newY)
                    {
                        newY = pose.position.y + 0.7f;
                    }
                }
            }
        }
    }
}
