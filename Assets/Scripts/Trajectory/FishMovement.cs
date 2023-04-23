using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    private List<Vector3> trajectory;
    private int currentIndex = 0;
    private float startTime;
    private float journeyLength;
    private float speed = 1.0f;

    private void Update()
    {
        if (trajectory == null || trajectory.Count == 0)
        {
            return;
        }

        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(trajectory[currentIndex], trajectory[currentIndex + 1], fracJourney);

        if (fracJourney >= 1.0f)
        {
            currentIndex++;
            if (currentIndex >= trajectory.Count - 1)
            {
                Destroy(gameObject);
                return;
            }
            startTime = Time.time;
            journeyLength = Vector3.Distance(trajectory[currentIndex], trajectory[currentIndex + 1]);
        }
    }

    public void SetTrajectory(List<Vector3> trajectory)
    {
        this.trajectory = trajectory;
        if (trajectory.Count < 2)
        {
            Destroy(gameObject);
            return;
        }
        startTime = Time.time;
        journeyLength = Vector3.Distance(trajectory[0], trajectory[1]);
    }
}
