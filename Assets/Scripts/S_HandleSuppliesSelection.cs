using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class S_HandleSuppliesSelection : MonoBehaviour
{
    private S_SpawnAnchors SpawnAnchors;

    private void Start()
    {
        // Find the AR Session Origin GameObject
        GameObject ARSessionOrigin = GameObject.Find("AR Session Origin");

        if (ARSessionOrigin != null)
        {
            // Get the S_SpawnAnchors component attached to AR Session Origin
            SpawnAnchors = ARSessionOrigin.GetComponent<S_SpawnAnchors>();
        }
        else
        {
            Debug.LogError("AR Session Origin GameObject not found in the scene.");
        }
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (SpawnAnchors != null)
        {
            SpawnAnchors.SetCurrentObject(gameObject);
        }
        else
        {
            Debug.LogWarning("S_SpawnAnchors script not assigned or found.");
        }
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        if (SpawnAnchors != null)
        {
            // Set the current object to null when exiting
            SpawnAnchors.SetCurrentObject(null);
        }
        else
        {
            Debug.LogWarning("S_SpawnAnchors script not assigned or found.");
        }
    }
}
