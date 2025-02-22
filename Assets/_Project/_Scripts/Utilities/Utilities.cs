using UnityEngine;

namespace  HelpUtilities
{
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
            randomX = side switch
            {
                0 => topLeft.x - Random.Range(0.5f, 1f),
                1 => topRight.x + Random.Range(0.5f, 1f),
                2 => Random.Range(topLeft.x, topRight.x),
                _ => Random.Range(topLeft.x, topRight.x)
            };
            return new Vector3(randomX, randomY, 0);
        }
    }
}
