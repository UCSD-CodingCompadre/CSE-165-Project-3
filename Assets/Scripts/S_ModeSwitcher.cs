using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class S_ModeSwitcher : MonoBehaviour
{
    // Hold a reference to the AR Session Origin 
    private GameObject ARSessionOrigin;

    // Hold a reference to the ModeManager Script
    private S_ModeManager ModeManager;

    // Hold a reference to TextMeshPro for mode switch text
    public TextMeshProUGUI ModeSwitchText;

    // Hold a reference to TextMeshPro for spawner text
    public TextMeshProUGUI SpawnerText;

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
     * @brief On select switch to the next mode
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

                    // Switch the mode
                    ModeManager.SwitchMode(Mode.AnchorPlacement);

                    // Switch the mode switch text
                    ModeSwitchText.text = "Anchor Placement Mode";

                    // Switch the spawner text
                    SpawnerText.text = "Spawn Anchor";

                    // Hold the spawn medical supplies script
                    S_SpawnMedicalSupplies MedicalSuppliesScript = ARSessionOrigin.GetComponent<S_SpawnMedicalSupplies>();

                    // Check if there is no script
                    if (MedicalSuppliesScript != null)
                    {

                        // Set the plane selected state
                        MedicalSuppliesScript.SetPlaneSelected(false);
                    }

                    // Else no script
                    else
                    {

                        // Log out error
                        Debug.LogError("S_SpawnMedicalSupplies script component not found on GameObject.");
                    }
                }

                // Else there is no session
                else
                {

                    // Log out the warning
                    Debug.LogWarning("AR Session Origin not found.");
                }
                break;

            // Check if the Mode is AnchorPlacement
            case Mode.AnchorPlacement:

                // Check if there is an AR Session Origin
                if (ARSessionOrigin != null)
                {

                    // Switch the mode
                    ModeManager.SwitchMode(Mode.WoundPlacement);

                    // Switch the mode switch text
                    ModeSwitchText.text = "Wound Placement Mode";

                    // Switch the spawner text
                    SpawnerText.text = "Spawn Wound";
                }

                // Else there is no session
                else
                {

                    // Log out the warning
                    Debug.LogWarning("AR Session Origin not found.");
                }
                break;

            // Check if the Mode is WoundPlacement
            case Mode.WoundPlacement:

                // Check if there is an AR Session Origin
                if (ARSessionOrigin != null)
                {

                    // Switch the mode
                    ModeManager.SwitchMode(Mode.AnchorPlacement);

                    // Switch the mode switch text
                    ModeSwitchText.text = "Plane Selection Mode";

                    // Switch the spawner text
                    SpawnerText.text = "Spawn Supplies";
                }

                // Else there is no session
                else
                {

                    // Log out the warning
                    Debug.LogWarning("AR Session Origin not found.");
                }
                break;
            default:
                break;
        }
    }
}
