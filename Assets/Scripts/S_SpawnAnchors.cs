using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class S_SpawnAnchors : MonoBehaviour
{

    // Hold the current game object
    private GameObject CurrentObject;

    // Hold a reference to the ARAnchorManager
    public ARAnchorManager AnchorManager;

    // Public function to set the current game object
    public void SetCurrentObject(GameObject obj)
    {
        CurrentObject = obj;
    }

    // Spawn the anchor
    public void SpawnAnchor()
    {
        // Check if the anchor manager is null
        if (AnchorManager == null)
        {
            Debug.LogError("ARAnchorManager is not initialized.");
            return;
        }

        Pose pose = new Pose(CurrentObject.transform.position, CurrentObject.transform.rotation);

        // Create a new anchor at the current object's position
        ARAnchor anchor = AnchorManager.AddAnchor(pose);

        // Check if the anchor creation was successful
        if (anchor == null)
        {
            Debug.LogError("Failed to spawn anchor.");
            return;
        }

        // Anchor creation successful
        Debug.Log("Anchor spawned successfully.");

        // Set the object's parent to be the anchor 
        CurrentObject.transform.parent = anchor.transform;

    }

}
