using UnityEngine;

public static class Utilities 
{
    public static Vector3 GetWorldMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }

    public static Vector3 GetInvisiblePoint()
    {
        Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.transform.position.z));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.transform.position.z));
        float randomX;
        float randomY = topLeft.y; 
        int side = Random.Range(0, 3);
        switch (side)
        {
            case 0:
                randomX = topLeft.x - Random.Range(0.5f, 1f);
                break;
            case 1:
                randomX = topRight.x + Random.Range(0.5f, 1f);
                break;
            case 2:
                randomX = Random.Range(topLeft.x, topRight.x);
                break;
            default:
                randomX = Random.Range(topLeft.x, topRight.x);
                break;
        }
        return new Vector3(randomX, randomY, 0);
    }
}
