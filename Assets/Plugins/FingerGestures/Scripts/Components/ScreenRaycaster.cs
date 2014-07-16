using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("FingerGestures/Components/Screen Raycaster")]
public class ScreenRaycaster : MonoBehaviour
{
    /// <summary>
    /// List of cameras to use for each raycast. Each camera will be considered in the order specified in this list,
    /// and the Raycast method will continue until a hit is detected.
    /// </summary>
    public Camera[] Cameras;

    /// <summary>
    /// Layers to ignore when raycasting
    /// </summary>
    public LayerMask IgnoreLayerMask;

    /// <summary>
    /// Thickness of the ray. 
    /// Setting rayThickness to 0 will use a normal Physics.Raycast()
    /// Setting rayThickness to > 0 will use Physics.SphereCast() of radius equal to half rayThickness
    ///  ** IMPORTANT NOTE ** According to Unity's documentation, Physics.SphereCast() doesn't work on colliders setup as triggers
    /// </summary>
    public float RayThickness = 0;

    /// <summary>
    /// Property used while in the editor only. 
    /// Toggles the visualization of the raycasts as red lines for misses, and green lines for hits (visible in scene view only)
    /// </summary>
    public bool VisualizeRaycasts = true;

    void Start()
    {
        // if no cameras were explicitely provided, use the current main camera
        if (Cameras == null || Cameras.Length == 0)
            Cameras = new Camera[] { Camera.main };
    }

    public bool Raycast(Vector2 screenPos, out RaycastHit2D hit)//ylj 
    {
        foreach (Camera cam in Cameras)
        {
            if (Raycast(cam, screenPos, out hit))
                return true;
        }

        hit = new RaycastHit2D();//ylj 
        return false;
    }

    bool Raycast(Camera cam, Vector2 screenPos, out RaycastHit2D hit) //ylj 
    {
        Ray ray = cam.ScreenPointToRay(screenPos);
        bool didHit = false;

        //ylj 
        //if( RayThickness > 0 )
        //    didHit = Physics2D.SphereCast( ray, 0.5f * RayThickness, out hit, Mathf.Infinity, ~IgnoreLayerMask );
        //else
        // didHit = Physics2D.Raycast( ray, out hit, Mathf.Infinity, ~IgnoreLayerMask );
        //hit = Physics2D.Raycast(screenPos, ray.direction);
        hit = Physics2D.Raycast(cam.ScreenToWorldPoint(screenPos), Vector2.zero);
        didHit = hit.collider != null;
        // vizualise ray
#if UNITY_EDITOR
        if (VisualizeRaycasts)
        {
            if (didHit)
                Debug.DrawLine(ray.origin, hit.point, Color.green, 0.5f);
            else
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 9999.0f, Color.red, 0.5f);
        }
#endif

        return didHit;
    }
}
