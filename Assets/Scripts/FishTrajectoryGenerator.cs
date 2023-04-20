using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FishTrajectoryGenerator : MonoBehaviour
{
    public int numFish = 50;
    public float duration = 10f;
    public float maxSpeed = 5f;
    public float noiseFrequency = 1f;
    public float noiseMagnitude = 0.5f;

    void Start()
    {
        string filePath = Application.dataPath + "/Scripts/fish_trajectory.txt";
        StreamWriter writer = new StreamWriter(filePath);

        for (int i = 0; i < numFish; i++)
        {
            int id = i + 1;

            float startTime = Random.Range(0f, duration);
            Vector3 startPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            float startSpeed = Random.Range(0f, maxSpeed);
            Vector3 startDirection = Random.insideUnitSphere;

            for (float t = startTime; t < duration; t += 0.1f)
            {
                float noiseValue = Mathf.PerlinNoise(t * noiseFrequency, id);
                Vector3 noiseOffset = new Vector3(Mathf.Cos(noiseValue * 2f * Mathf.PI), Mathf.Sin(noiseValue * 2f * Mathf.PI), Mathf.Cos(noiseValue * 3f * Mathf.PI));
                Vector3 position = startPosition + startDirection * startSpeed * (t - startTime) + noiseOffset * noiseMagnitude;
                writer.WriteLine($"{id} {t} {position.x} {position.y} {position.z}");
            }
        }

        writer.Close();
        Debug.Log("Fish trajectory file saved to " + filePath);
    }
}
