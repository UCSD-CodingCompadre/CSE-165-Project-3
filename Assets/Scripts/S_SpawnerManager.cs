using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class S_SpawnerManager : MonoBehaviour
{

    // Hold a reference to the AR Session Origin 
    private GameObject ARSessionOrigin;

    // Hold a reference to the ModeManager Script
    private S_ModeManager ModeManager;

    /*
     * @brief On start locate the AR Session Origin
     * @param none 
     * @return void
     */
    void Start()
    {
        // Find the AR Session Origin GameObject by name
        ARSessionOrigin = GameObject.Find("AR Session Origin");

        // Check if there is an AR Session Origin
        if (ARSessionOrigin != null)
        {

            // Get the S_ModeManager component attached to AR Session Origin
            ModeManager = ARSessionOrigin.GetComponent<S_ModeManager>();
        }

        // Else print a debug statement
        else
        {

            // Log out the error
            Debug.LogError("AR Session Origin GameObject not found in the scene.");
        }
    }

    /*
     * @brief On select spawn the correct object using the mode
     * @param SelectEnterEventArgs args the SelectEnter arguments
     * @return void
     */
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Check the current mode and perform the corresponding functionality
        switch (ModeManager.GetCurrentMode())
        {

            // Check if the Mode is PlaneSelection
            case Mode.PlaneSelection:

                // Check if there is an AR Session Origin
                if (ARSessionOrigin != null)
                {

                    // Hold the spawn medical supplies script
                    S_SpawnMedicalSupplies SpawnMedicalSupplies = ARSessionOrigin.GetComponent<S_SpawnMedicalSupplies>();
                    
                    // Check if there script exists
                    if (SpawnMedicalSupplies != null)
                    {

                        // Spawn the supplies
                        SpawnMedicalSupplies.SpawnSupplies();
                    }

                    // Else it doesn't
                    else
                    {

                        // Log out the warning
                        Debug.LogWarning("S_SpawnMedicalSupplies script not found on AR Session Origin.");
                    }
                }

                // Else there is no session
                else
                {

                    // Log out the warning
                    Debug.LogWarning("AR Session Origin not found.");
                }
                break;
            case Mode.AnchorPlacement:
                // Check if there is an AR Session Origin
                if (ARSessionOrigin != null)
                {
                    // Get the S_SpawnAnchors component attached to the AR Session Origin
                    S_SpawnAnchors spawnAnchors = ARSessionOrigin.GetComponent<S_SpawnAnchors>();

                    // Check if the script exists
                    if (spawnAnchors != null)
                    {
                        // Spawn the anchor
                        spawnAnchors.SpawnAnchor();
                    }
                    else
                    {
                        // Log a warning if the script is not found
                        Debug.LogWarning("S_SpawnAnchors script not found on AR Session Origin.");
                    }
                }
                else
                {
                    // Log an error if the AR Session Origin game object is not found
                    Debug.LogError("AR Session Origin GameObject not found in the scene.");
                }
                break;
            case Mode.WoundPlacement:
                // Functionality for wound placement mode
                break;
            default:
                break;
        }
    }
}

