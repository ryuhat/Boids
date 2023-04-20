using System.IO;
using UnityEngine;

public class FishTrajectoryLoader : MonoBehaviour
{
    public TextAsset trajectoryFile;
    public GameObject fishPrefab;
    public float timeScale = 1f;
    public float dotSpacing = 0.1f;

    void Start()
    {
        string[] lines = trajectoryFile.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(' ');

            int id = int.Parse(values[0]);
            float time = float.Parse(values[1]);
            float x = float.Parse(values[2]);
            float y = float.Parse(values[3]);
            float z = float.Parse(values[4]);

            GameObject fish = Instantiate(fishPrefab, new Vector3(x, y, z), Quaternion.identity);
            fish.transform.parent = transform;

            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(x, y, z));

            if (i % Mathf.CeilToInt(dotSpacing / timeScale) == 0)
            {
                GameObject dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                dot.transform.position = new Vector3(x, y, z);
                dot.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                dot.transform.parent = transform;
            }

            if (i < lines.Length - 1)
            {
                string[] nextValues = lines[i + 1].Split(' ');
                float nextTime = float.Parse(nextValues[1]);
                float deltaTime = (nextTime - time) * timeScale;
                float distance = Vector3.Distance(new Vector3(x, y, z), new Vector3(float.Parse(nextValues[2]), float.Parse(nextValues[3]), float.Parse(nextValues[4])));
                float speed = distance / deltaTime;

                fish.GetComponent<Rigidbody>().velocity = (new Vector3(float.Parse(nextValues[2]), float.Parse(nextValues[3]), float.Parse(nextValues[4])) - fish.transform.position).normalized * speed;
            }
        }
    }
}

