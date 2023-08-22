using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Furniture_Placement_manager : MonoBehaviour
{
    public GameObject SpwanableFurniture;
    public ARSessionOrigin sessionOrigin;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                bool collision = raycastManager.Raycast(Input.GetTouch(0).position, raycastHits, TrackableType.PlaneWithinPolygon);
                
                if (collision)
                {
                    GameObject _object = Instantiate(SpwanableFurniture);
                    _object.transform.position = raycastHits[0].pose.position;
                    _object.transform.rotation = raycastHits[0].pose.rotation;
                }
            }

            foreach (var planes in planeManager.trackables)
            {
                planes.gameObject.SetActive(false); // 'GameObject' should be lowercase
            }

            planeManager.enabled = false;
        }
    }

    public void SwitchFurniture(GameObject furniture)
    {
        SpwanableFurniture = furniture;
    }
}
