using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraExtension
{
    public static Vector3 PlaceOutsideFrustum(this Camera camera, Bounds bounds, float distance, bool left)
    {
        var frustumPlanes = GeometryUtility.CalculateFrustumPlanes(camera);
        Ray ray;
        Plane plane;

        if (left)
        {
            // 1.
            ray = camera.ScreenPointToRay(new Vector3(0, camera.pixelHeight / 2f, 0));
            plane = frustumPlanes[0];
        }
        else
        {
            ray = camera.ScreenPointToRay(new Vector3(camera.pixelWidth - 1, camera.pixelHeight / 2f, 0));
            plane = frustumPlanes[1];
        }

        // 2.
        var borderPoint = ray.GetPoint(distance);

        // 3.
        var frustumOutside = -plane.normal;

        // 4.
        var halfDiameter = bounds.size.magnitude / 2f;
        return borderPoint + frustumOutside * halfDiameter;
    }
}
