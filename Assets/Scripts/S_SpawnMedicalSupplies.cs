using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))] 
public class S_SpawnMedicalSupplies : MonoBehaviour
{

    // Hold the AR Raycast Manager component
    private ARRaycastManager RaycastManager;

    // Hold the reference to the spawned game object
    private GameObject SpawnedObject;

    // Hold the reference to the prefab being spawned
    [SerializeField] 
    private GameObject PlaceablePrefab;

    // Hold a list of the raycast hits
    static List<ARRaycastHit> Hits = new List<ARRaycastHit>(); 

    /*
     * @brief Get the ARRaycastManager component
     * @param none
     * @return void
     */
    private void Awake()
    {

        // Set the Raycast Manager component
        RaycastManager = GetComponent<ARRaycastManager>(); 
    }

    /*
     * @brief Check if the user touched the screen and return the position
     * @param out Vector2 TouchPosition the position of the touch on the screen
     * @return bool 
     */
    bool TryGetTouchPosition(out Vector2 TouchPosition)
    {

        // Check if the user is touching the screen
        if (Input.touchCount > 0)
        {

            // Set the touch position
            TouchPosition = Input.GetTouch(0).position;

            // Return true
            return true;
        }

        // Set the touch position to default
        TouchPosition = default;

        // Return false
        return false;
    }

    /*
     * @brief Spawn the medical supplies 
     * @param none
     * @return void
     */
    public void SpawnSupplies()
    {

        // Check if the user touched the screen
        if(TryGetTouchPosition(out Vector2 TouchPosition))
        {

            // Return
            return;
        }

        // Check if the raycast is hitting a plane
        if(RaycastManager.Raycast(TouchPosition, Hits, TrackableType.PlaneWithinPolygon))
        {

            // Hold the hit position
            var HitResult = Hits[0].pose;

            // Check if the object has not spawned
            if(SpawnedObject == null)
            {

                // Spawn the object
                SpawnedObject = Instantiate(PlaceablePrefab, HitResult.position, HitResult.rotation);
            }
        }
    }
}

