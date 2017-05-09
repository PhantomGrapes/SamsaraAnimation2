using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    [Header("Normal Camera Control")]
    public float m_XDampTime = 0.2f;                 // Approximate time for the camera to refocus.
    public float m_YUpDampTime = 0.2f;
    public float m_YDownDampTime = 0.2f;
    public float m_ZDampTime = 0.2f;
    public float m_ZoomDampTime = 0.2f;
    public float m_ScreenEdgeBuffer = 4f;           // Space between the top/bottom most target and the screen edge.
    public float m_MinSize = 6.5f;                  // The smallest orthographic size the camera can be.
    public float x_Offset = 0f;
    public float y_Offset = 5f;
    [HideInInspector] public List<Transform> m_Targets; // All the targets the camera needs to encompass.


    private Camera m_Camera;                        // Used for referencing the camera.
    private float m_ZoomSpeed;                      // Reference speed for the smooth damping of the orthographic size.
    private Vector3 m_MoveVelocity;                 // Reference velocity for the smooth damping of the position.
    private Vector3 m_DesiredPosition;              // The position the camera is moving towards.

    [Header("Camera Shake")]
    private Vector3 originPosition;
    private Vector2 shake_decay;                       // The decrement of shake after each shake
    private Vector2 shake_intensity;                   // Current shake intensity


    private void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();
        m_Targets.Add(FindObjectOfType<PlayerController>().GetComponent<Transform>());
        SetStartPositionAndSize();
    }



    private void FixedUpdate()
    {
        if (shake_intensity.x > 0 || shake_intensity.y > 0)
            // Shake
            Shake();
        else {
            // Move the camera towards a desired position.
            Move();

            // Change the size of the camera based.
            Zoom();
        } 
    }


    private void Move()
    {
        // Find the average position of the targets.
        FindAveragePosition();

        // Smoothly transition to that position.
        Vector3 newPosition = Vector3.zero;
        newPosition.x = Mathf.SmoothDamp(transform.position.x, m_DesiredPosition.x, ref m_MoveVelocity.x, m_XDampTime);
        newPosition.y = Mathf.SmoothDamp(transform.position.y, m_DesiredPosition.y, ref m_MoveVelocity.y, m_DesiredPosition.y>transform.position.y?m_YUpDampTime:m_YDownDampTime);
        newPosition.z = Mathf.SmoothDamp(transform.position.z, m_DesiredPosition.z, ref m_MoveVelocity.z, m_ZDampTime);
        transform.position = newPosition;
    }


    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;

        // Go through all the targets and add their positions together.
        for (int i = 0; i < m_Targets.Count; i++)
        {
            // If the target isn't active, go on to the next one.
            if (!m_Targets[i].gameObject.activeSelf)
                continue;

            // Add to the average and increment the number of targets in the average.
            averagePos += m_Targets[i].position;
            numTargets++;
        }
        // If there are targets divide the sum of the positions by the number of them to find the average.
        if (numTargets > 0)
            averagePos /= numTargets;
        

        // Keep the same z value.
        averagePos.z = transform.position.z;

        // Add the x and y offset
        averagePos.x += x_Offset;
        averagePos.y += y_Offset;

        // The desired position is the average position;
        m_DesiredPosition = averagePos;
    }


    private void Zoom()
    {
        // Find the required size based on the desired position and smoothly transition to that size.
        float requiredSize = FindRequiredSize();
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_ZoomDampTime);
    }


    private float FindRequiredSize()
    {
        // Find the position the camera rig is moving towards in its local space.
        Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);

        // Start the camera's size calculation at zero.
        float size = 0f;

        // Go through all the targets...
        for (int i = 0; i < m_Targets.Count; i++)
        {
            // ... and if they aren't active continue on to the next target.
            if (!m_Targets[i].gameObject.activeSelf)
                continue;

            // Otherwise, find the position of the target in the camera's local space.
            Vector3 targetLocalPos = transform.InverseTransformPoint(m_Targets[i].position);

            // Find the position of the target from the desired position of the camera's local space.
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            // Choose the largest out of the current size and the distance of the tank 'up' or 'down' from the camera.
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            // Choose the largest out of the current size and the calculated size based on the tank being to the left or right of the camera.
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
        }

        // Add the edge buffer to the size.
        size += m_ScreenEdgeBuffer;

        // Make sure the camera's size isn't below the minimum.
        size = Mathf.Max(size, m_MinSize);

        return size;
    }


    public void SetStartPositionAndSize()
    {
        // Find the desired position.
        FindAveragePosition();

        // Set the camera's position to the desired position without damping.
        transform.position = m_DesiredPosition;

        // Find and set the required size of the camera.
        m_Camera.orthographicSize = FindRequiredSize();
    }

    public void Shake()
    {
        transform.position = originPosition + new Vector3(Random.value * shake_intensity.x, Random.value * shake_intensity.y, 0);
        shake_intensity.x = shake_intensity.x - shake_decay.x > 0 ? shake_intensity.x - shake_decay.x : 0;
        shake_intensity.y = shake_intensity.y - shake_decay.y > 0 ? shake_intensity.y - shake_decay.y : 0;
    }

    public void StartShake(Vector2 shake_intensity, Vector2 shake_decay)
    {
        originPosition = transform.position;
        this.shake_intensity = shake_intensity;
        this.shake_decay = shake_decay;
    }

    public void StopShake()
    {
        shake_intensity = Vector2.zero;
    }
}
