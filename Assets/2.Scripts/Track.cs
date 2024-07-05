using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Track : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    public List<GameObject> list1 = new List<GameObject>();
    private Dictionary<string, GameObject> dict1 = new Dictionary<string, GameObject>();
    [SerializeField] private float canvasDistanceFromMarker = 0.2f;
    [SerializeField] private float hideDelay = 0.5f;

    private Dictionary<string, float> lastSeenTime = new Dictionary<string, float>();

    private void Start()
    {
        foreach (GameObject o in list1)
            dict1.Add(o.name, o);
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void Update()
    {
        foreach (var entry in new Dictionary<string, float>(lastSeenTime))
        {
            if (Time.time - entry.Value > hideDelay)
            {
                lastSeenTime.Remove(entry.Key);
                if (dict1.ContainsKey(entry.Key))
                {
                    dict1[entry.Key].SetActive(false);
                }
            }
        }
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateCanvasPosition(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateCanvasPosition(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            lastSeenTime.Remove(trackedImage.referenceImage.name);
            if (dict1.ContainsKey(trackedImage.referenceImage.name))
            {
                dict1[trackedImage.referenceImage.name].SetActive(false);
            }
        }
    }

    private void UpdateCanvasPosition(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            GameObject o = dict1[name];
            o.transform.position = trackedImage.transform.position/* + trackedImage.transform.forward * canvasDistanceFromMarker*/;
            o.transform.rotation = trackedImage.transform.rotation;
            o.SetActive(true);
            lastSeenTime[name] = Time.time;
            GameManager.Instance.CheckAnswer(name);
        }
        else
        {
            lastSeenTime[name] = Time.time;
            if (dict1.ContainsKey(name))
            {
                dict1[name].SetActive(false);
            }
        }
    }
}
