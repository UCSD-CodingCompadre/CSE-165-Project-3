using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public enum Mode
{
    PlaneSelection,
    AnchorPlacement,
    WoundPlacement
}

public class S_ModeManager : MonoBehaviour
{

    // Hold a reference to the ARRaycastManager
    private ARRaycastManager RaycastManager;

    // Hold a reference to the Selected Plane
    private ARPlane SelectedPlane;

    // Hold a reference to the current mode
    private Mode CurrentMode = Mode.PlaneSelection;

    // Hold a reference to the AR Session Origin 
    private GameObject ARSessionOrigin;

    /*
     * @brief Switch the mode
     * @param Mode newMode
     * @return void
     */
    public void SwitchMode(Mode newMode)
    {
        CurrentMode = newMode;
    }

    /*
     * @brief Get current mode
     * @param none
     * @return Mode
     */
    public Mode GetCurrentMode()
    {
        return CurrentMode;
    }

    /*
     * @brief On start get the raycast manager
     * @param none
     * @return void
     */
    void Start()
    {

        // Get the ARRaycastManager component
        RaycastManager = FindObjectOfType<ARRaycastManager>();

        // Find the AR Session Origin GameObject by name
        ARSessionOrigin = GameObject.Find("AR Session Origin");
    }

    /*
     * @brief on Update Handle the right mode
     * @param none
     * @return void
     */
    void Update()
    {
        switch (CurrentMode)
        {
            case Mode.PlaneSelection:
                HandlePlaneSelection();
                break;
            case Mode.AnchorPlacement:
                HandleAnchorPlacement();
                break;
            case Mode.WoundPlacement:
                HandleWoundPlacement();
                break;
        }
    }

    private void HandlePlaneSelection()
    {
        // Check if the user touches the screen
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Perform a raycast from the touch position
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // Check if the ray hits an AR plane
            if (Physics.Raycast(ray, out hit))
            {
                ARPlane ARPlane = hit.collider.GetComponent<ARPlane>();

                if (ARPlane != null)
                {
                    Debug.Log("CSE 165 Plane hit");
                    // Save the selected AR plane
                    SelectedPlane = ARPlane;

                    S_SpawnMedicalSupplies MedicalSuppliesScript = ARSessionOrigin.GetComponent<S_SpawnMedicalSupplies>();
                    if (MedicalSuppliesScript != null)
                    {
                        // Set the data and state
                        MedicalSuppliesScript.SetARPlaneData(SelectedPlane.transform.position, SelectedPlane.transform.rotation);
                        MedicalSuppliesScript.SetPlaneSelected(true);
                    }
                    else
                    {
                        Debug.LogError("S_SpawnMedicalSupplies script component not found on GameObject.");
                    }
                }
            }
        }
    }

    private void HandleAnchorPlacement()
    {
        // Check if the user touches the screen
        /*if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Perform a raycast from the touch position
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            // Check if the ray hits an AR plane
            if (Physics.Raycast(ray, out hit))
            {
                ARPlane ARPlane = hit.collider.GetComponent<ARPlane>();

                if (ARPlane != null)
                {
                    Debug.Log("CSE 165 Plane hit (anchor)");
                    // Save the selected AR plane
                    SelectedPlane = ARPlane;

                    S_SpawnAnchor AnchorScript = ARSessionOrigin.GetComponent<S_SpawnAnchor>();
                    if (AnchorScript != null)
                    {
                        // Set the data and state
                        AnchorScript.SetARPlaneData(SelectedPlane.transform.position, SelectedPlane.transform.rotation);
                        AnchorScript.SetPlaneSelected(true);
                    }
                    else
                    {
                        Debug.LogError("S_SpawnAnchor script component not found on GameObject.");
                    }
                }
            }
        }*/
    }

    private void HandleWoundPlacement()
    {
        // Handle wound placement logic here
        // Example: Allow the user to place wounds on selected anchor points
    }
}



