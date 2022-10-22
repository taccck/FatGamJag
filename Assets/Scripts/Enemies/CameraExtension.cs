using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraExtension
{
    public static Vector3 PlaceOutsideFrustum(this Camera camera, Bounds bounds, float distance)
    {
        var frustumPlanes = GeometryUtility.CalculateFrustumPlanes(camera);
        Ray ray;
        Plane plane;

        if (Random.value < 0.5f) // horizontal or vertical
        {
            if(Random.value < 0.5f)
                ray = camera.ScreenPointToRay(new Vector3(camera.pixelWidth * Random.value, camera.pixelHeight, 0));
            else
                ray = camera.ScreenPointToRay(new Vector3(camera.pixelWidth * Random.value, 0, 0));
        }
        else
        {
            if(Random.value < 0.5f)
                ray = camera.ScreenPointToRay(new Vector3(camera.pixelWidth, camera.pixelHeight * Random.value, 0));
            else
                ray = camera.ScreenPointToRay(new Vector3(0, camera.pixelHeight * Random.value, 0));
        }
        
        plane = frustumPlanes[0];

        // 2.
        var borderPoint = ray.GetPoint(distance);

        // 3.
        var frustumOutside = -plane.normal;

        // 4.
        var halfDiameter = bounds.size.magnitude / 2f;
        return borderPoint + frustumOutside * halfDiameter;
    }
}
