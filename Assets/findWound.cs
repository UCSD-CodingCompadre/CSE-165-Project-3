using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findWound : MonoBehaviour
{
    public GameObject[] patchList;
    public GameObject wound;
    public GameObject woundPrefab;
    public bool isCreated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Find all GameObjects with the tag 'wounds'
        GameObject[] woundList = GameObject.FindGameObjectsWithTag("wounds");

        if (woundList.Length > 0)
        {
            // Get the first wound from the list
            wound = woundList[0];

            // Check if the woundPrefab is set
            if (woundPrefab != null)
            {
                // Instantiate the prefab at the wound's position and rotation
                if(!isCreated)
                {
                    GameObject instantiatedPrefab = Instantiate(woundPrefab, wound.transform.position, wound.transform.rotation);
                    isCreated = true;
                }
            }
            else
            {
                Debug.LogError("Wound prefab is not set.");
            }
        }
        else
        {
            Debug.LogWarning("No wound found with the tag 'wounds'.");
        }
    }
}