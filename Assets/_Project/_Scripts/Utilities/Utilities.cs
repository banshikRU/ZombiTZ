using UnityEngine;

public static class Utilities 
{
    public static Vector3 GetWorldMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }

    public static bool IsPointVisible(Vector2 point)
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(new Vector3(point.x, point.y, 0));
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1;
    }
}
