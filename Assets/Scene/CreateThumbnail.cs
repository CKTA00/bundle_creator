using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateThumbnail : MonoBehaviour
{
    public Camera mainCamera;
    public bool autoPositionCamera = true;
    public float distance = 5;
    public float cameraViewScaleFactor = 1;

    void Start()
    {
        if (!Directory.Exists("Assets/Thumbnails/"))
        {
            Directory.CreateDirectory("Assets/Thumbnails/");
        }

        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);
        if(rootObjects.Count > 2)
        {
            Debug.LogError("Creating of thumbnail aborted, because there are multiple top level objects.\n" +
                "Remove objects excpet \"Scene\" object so that there are only 2 present (Secee and your object with correct prefab name).");
            return;
        }

        foreach (GameObject userObject in rootObjects)
        {
            if (userObject == gameObject)
            {
                continue;
            }
            Debug.Log(userObject.name);
            BoxCollider box = userObject.GetComponent<BoxCollider>();
            if(box == null)
            {
                Debug.LogError("Your object must contain BoxCollider component!");
                Debug.LogError("Creating of thumbnail aborted.");
                return;
            }

            if(autoPositionCamera)
            {
                float scale = Mathf.Max(box.bounds.size.x, box.bounds.size.y, box.bounds.size.z) * cameraViewScaleFactor;
                mainCamera.gameObject.transform.position =
                    new Vector3(
                        distance + box.bounds.center.x,
                        distance * Mathf.Sqrt(2) + box.bounds.center.y,
                        distance + box.bounds.center.z
                    );
                mainCamera.orthographicSize = scale;
            }

            string assetPath = "Assets/Thumbnails/" + userObject.name + ".png";
            ScreenCapture.CaptureScreenshot(assetPath);
        }
    }
}
