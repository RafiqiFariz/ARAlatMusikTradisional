using System.Collections.Generic;
using UnityEngine.XR.ARFoundation;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ImageTracker : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabsToSpawn;
    private ARTrackedImageManager arTrackedImageManager;
    private Dictionary<string, GameObject> arObjects;
    
    public ImageData[] imageData;
    public Vector3 scaleFactor = new Vector3(0.5f, 0.5f, 0.5f);
    public TextMeshProUGUI txtDescription;
    public RawImage image;
    public GameObject clue;

    [System.Serializable]
    public struct ImageData
    {
        public string title;
        public string description;
        public Texture imageInstruction;
    }
    
    void Awake()
    {
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        arObjects = new Dictionary<string, GameObject>();

        foreach (var prefab in prefabsToSpawn)
        {
            GameObject newARObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newARObject.name = prefab.name;
            newARObject.gameObject.SetActive(false);
            arObjects.Add(prefab.name, newARObject);
        }
    }

    void OnEnable() => arTrackedImageManager.trackedImagesChanged += OnChanged;

    void OnDisable() => arTrackedImageManager.trackedImagesChanged -= OnChanged;

    private void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            UpdateARImage(newImage);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            UpdateARImage(updatedImage);
        }

        foreach (var removedImage in eventArgs.removed)
        {
            clue.SetActive(true);
            txtDescription.text = "";
            image.texture = null;
            arObjects[removedImage.referenceImage.name].SetActive(false);
        }
    }

    private void UpdateARImage(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState is TrackingState.Limited or TrackingState.None)
        {
            clue.SetActive(true);
            txtDescription.text = "";
            image.texture = null;
            arObjects[trackedImage.referenceImage.name].SetActive(false);
            return;
        }
        
        var matchingImageData = imageData.FirstOrDefault(data => data.title == trackedImage.referenceImage.name);
        if (matchingImageData.title != null)
        {
            txtDescription.text = matchingImageData.description;
            image.texture = matchingImageData.imageInstruction;
        }

        // Assign and place game object
        AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform);

        Debug.Log($"Tracked Reference Image Name: {trackedImage.referenceImage.name}");
    }

    private void AssignGameObject(string name, Transform transform)
    {
        if (prefabsToSpawn == null) return;

        clue.SetActive(false);
        var arObject = arObjects[name];
        arObject.SetActive(true);
        arObject.transform.position = transform.position;
        arObject.transform.localRotation = transform.localRotation;
        // arObject.transform.localScale = scaleFactor;
    }
}