using UnityEngine;

public class RotateCannon : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButton(0)) // Detect touch or mouse click
        {
            RotateToTouchPoint();
        }
    }

    private void RotateToTouchPoint()
    {
        // Get the touch position in screen coordinates
        Vector3 touchPosition = Input.mousePosition;

        // Convert screen position to world position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

        // Calculate the direction vector from the cannon's position to the touch position
        Vector3 direction = worldPosition - transform.position;
        direction.z = 0; // For 2D, ignore the z-axis

        // Calculate the rotation angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the cannon towards the angle (can optionally use smooth rotation)
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
