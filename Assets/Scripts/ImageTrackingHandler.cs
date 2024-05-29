using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using TMPro;

public class ImageTrackingHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Text trackingStateText; // UI Text to display tracking state
    [SerializeField]
    private TMP_Text positionText;      // UI Text to display position
    [SerializeField]
    private GameObject virtualWoundPrefab; // Prefab to instantiate for the virtual wound

    private ARTrackedImageManager trackedImageManager;
    private GameObject virtualWoundInstance;

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            UpdateUI(trackedImage);
            if (virtualWoundInstance == null)
            {
                virtualWoundInstance = Instantiate(virtualWoundPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
            }
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateUI(trackedImage);
            if (virtualWoundInstance != null)
            {
                virtualWoundInstance.transform.position = trackedImage.transform.position;
                virtualWoundInstance.transform.rotation = trackedImage.transform.rotation;
            }
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            UpdateUI(trackedImage, true);
            if (virtualWoundInstance != null)
            {
                Destroy(virtualWoundInstance);
                virtualWoundInstance = null;
            }
        }
    }

    private void UpdateUI(ARTrackedImage trackedImage, bool isRemoved = false)
    {
        if (isRemoved)
        {
            trackingStateText.text = "Tracking State: Removed";
            positionText.text = "Position: N/A";
        }
        else
        {
            trackingStateText.text = $"Tracking State: {trackedImage.trackingState}";
            positionText.text = $"Position: {trackedImage.transform.position}";
        }
    }
}
