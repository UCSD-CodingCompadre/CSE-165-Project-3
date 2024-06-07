using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class S_SpawnAnchor : MonoBehaviour
{

    // Hold the position of the AR Plane
    private ARRaycastManager RaycastManager;

    // Hold the medical supplied prefab
    public GameObject DebugCube;

    // Hold a reference to the ARAnchorManager
    public ARAnchorManager AnchorManager;

    // Camera
    public Camera camera;

    // Distance object is spawned away from camera
    public float distanceFromCamera = 1f;
    /*
     * @brief Spawn anchor 
     * @param none
     * @return void
     */
    // Spawn the anchor
    public void SpawnAnchor()
    {
        Debug.Log("CSE165 SPAWNANCHOR HAS BEEN CALLED");
        // Check if the anchor manager is null
        if (AnchorManager == null)
        {
            Debug.LogError("ARAnchorManager is not initialized.");
            return;
        }

        Pose pose = new Pose(camera.transform.position + (camera.transform.forward * distanceFromCamera), camera.transform.rotation);

        // Create a new anchor at the current object's position
        ARAnchor anchor = AnchorManager.AddAnchor(pose);

        // Check if the anchor creation was successful
        if (anchor == null)
        {
            Debug.LogError("Failed to spawn anchor.");
            return;
        }
        Instantiate(DebugCube, pose.position, pose.rotation);
        // Anchor creation successful
        Debug.Log("Anchor spawned successfully.");
    }
}
