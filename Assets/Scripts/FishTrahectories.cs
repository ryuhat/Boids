using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FishTrajectories : MonoBehaviour
{
    public GameObject fishPrefab;  // The fish model to use
    public string dataFileName;   // The name of the text file containing the fish trajectory data
    public float lineSegmentLength = 0.1f;  // The length of each dotted line segment

    private Dictionary<int, List<Vector3>> trajectories; // A dictionary mapping fish IDs to their trajectories

    // Start is called before the first frame update
    void Start()
    {
        // Read the fish trajectory data from the text file
        TextAsset textAsset = Resources.Load<TextAsset>(dataFileName);
        string[] lines = textAsset.text.Split('\n');
        trajectories = new Dictionary<int, List<Vector3>>();
        foreach (string line in lines)
        {
            if (line.Trim() == "") continue;
            string[] data = line.Split(' ');
            int fishId = int.Parse(data[0]);
            float time = float.Parse(data[1]);
            float x = float.Parse(data[2]);
            float y = float.Parse(data[3]);
            float z = float.Parse(data[4]);
            Vector3 position = new Vector3(x, y, z);
            if (!trajectories.ContainsKey(fishId))
            {
                trajectories[fishId] = new List<Vector3>();
            }
            trajectories[fishId].Add(position);
        }

        // Render the fish trajectories with dotted lines
        foreach (var trajectory in trajectories)
        {
            GameObject fish = Instantiate(fishPrefab);
            LineRenderer lineRenderer = fish.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.white;
            lineRenderer.endColor = Color.white;
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.positionCount = trajectory.Value.Count;
            for (int i = 0; i < trajectory.Value.Count; i++)
            {
                lineRenderer.SetPosition(i, trajectory.Value[i]);
                if (i > 0 && Vector3.Distance(trajectory.Value[i], trajectory.Value[i-1]) > lineSegmentLength)
                {
                    int numSegments = Mathf.FloorToInt(Vector3.Distance(trajectory.Value[i], trajectory.Value[i-1]) / lineSegmentLength);
                    for (int j = 1; j < numSegments; j++)
                    {
                        Vector3 segmentStart = Vector3.Lerp(trajectory.Value[i-1], trajectory.Value[i], (float)j / numSegments);
                        Vector3 segmentEnd = Vector3.Lerp(trajectory.Value[i-1], trajectory.Value[i], (float)(j+1) / numSegments);
                        lineRenderer.positionCount += 1;
                        lineRenderer.SetPosition(lineRenderer.positionCount-1, segmentStart);
                        lineRenderer.positionCount += 1;
                        lineRenderer.SetPosition(lineRenderer.positionCount-1, segmentEnd);
                    }
                }
            }
        }
    }
}

