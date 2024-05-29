using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))] 
public class S_SpawnMedicalSupplies : MonoBehaviour
{

    // Hold the position of the AR Plane
    private ARRaycastManager RaycastManager;

    // Hold true if there is plane selected
    private bool PlaneSelected = false;

    // Hold the medical supplied prefab
    public GameObject MedicalSuppliesPrefab;

    // Hold the position of the AR Plane
    private Vector3 ARPlanePosition;

    // Hold the rotation of the AR Plane
    private Quaternion ARPlaneRotation;

    // Hold the instance of the supplies 
    private GameObject MedicalSuppliesInstance;

    /*
     * @brief Set the plane data
     * @param Vector3 Position the position of the ARPlane
     * Querternion Rotation the rotation of the ARPlane
     * @return void
     */
    public void SetARPlaneData(Vector3 Position, Quaternion Rotation)
    {

        // Set the AR plane position
        ARPlanePosition = Position;

        // Set the AR plane rotation
        ARPlaneRotation = Rotation;
    }

    /*
     * @brief Set the plane selected state
     * @param bool State the bool state if a plane is selected
     * @return void
     */
    public void SetPlaneSelected(bool State)
    {

        // Set the plane selected state
        PlaneSelected = State;
    }

    /*
     * @brief Spawn the medical supplies 
     * @param none
     * @return void
     */
    public void SpawnSupplies()
    {
        Debug.Log("SPAWNSUPPLIES HAS BEEN CALLED");
       // Check if there is a plane selected
       if(PlaneSelected)
        {
            Debug.Log("PLANE SELECTED");
            // Check if there is an instance of the medical supplies
            if (MedicalSuppliesInstance != null) Destroy(MedicalSuppliesInstance);
            // Spawn the medical supplies and set the isntance
            MedicalSuppliesInstance = Instantiate(MedicalSuppliesPrefab, new Vector3(0,0,0), Quaternion.identity);
        }
        MedicalSuppliesInstance = Instantiate(MedicalSuppliesPrefab, new Vector3(1, -0.25f, 0), Quaternion.identity);
    }
}

