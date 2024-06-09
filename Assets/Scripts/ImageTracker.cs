using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    private ARTrackedImageManager trackedImages;
    public GameObject[] ArPrefabs;
    public ImageData[] imageDatas;

    [System.Serializable]
    public struct ImageData
    {
        public string title;
        public string description;
    }

    public TextMeshProUGUI publicText;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        trackedImages = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImages.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImages.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    // Event Handler
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // Handle added and updated images
        foreach (var trackedImage in eventArgs.added)
        {
            UpdatePrefab(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            UpdatePrefab(trackedImage);
        }

        // Handle removed images
        foreach (var trackedImage in eventArgs.removed)
        {
            if (spawnedPrefabs.TryGetValue(trackedImage.referenceImage.name, out GameObject prefab))
            {
                prefab.SetActive(false);
            }
        }
    }

    private void UpdatePrefab(ARTrackedImage trackedImage)
    {
        if (!spawnedPrefabs.TryGetValue(trackedImage.referenceImage.name, out GameObject prefab))
        {
            foreach (var arPrefab in ArPrefabs)
            {
                if (trackedImage.referenceImage.name == arPrefab.name)
                {
                    prefab = Instantiate(arPrefab, trackedImage.transform);
                    spawnedPrefabs[trackedImage.referenceImage.name] = prefab;
                    break;
                }
            }
        }

        if (prefab != null)
        {
            prefab.SetActive(trackedImage.trackingState == TrackingState.Tracking);

            // Set text based on title and description
            foreach (var imageData in imageDatas)
            {
                if (trackedImage.referenceImage.name == imageData.title)
                {
                    publicText.text = imageData.description;
                    break;
                }
            }
        }
    }
}
