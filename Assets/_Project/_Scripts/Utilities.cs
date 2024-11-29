using UnityEngine;

public static class Utilities 
{
    public static Vector3 GetWorldMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}
