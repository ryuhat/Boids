using System.IO;
using UnityEngine;

public class GenerateFishTrajectory : MonoBehaviour
{
    public int numFish = 50;
    public float totalTime = 10f;
    public float maxSpeed = 5f;
    public float minDepth = -5f;
    public float maxDepth = 5f;

    private StreamWriter writer;

    void Start()
    {
        writer = new StreamWriter("fish_trajectory.txt");
        GenerateTrajectory();
        writer.Close();
    }

    void GenerateTrajectory()
    {
        for (int i = 0; i < numFish; i++)
        {
            int id = i;
            float time = i * (totalTime / numFish);
            float x = Random.Range(-10f, 10f);
            float y = Random.Range(minDepth, maxDepth);
            float z = Random.Range(-10f, 10f);
            Vector3 position = new Vector3(x, y, z);
            float speed = Random.Range(0.5f, maxSpeed);
            Vector3 velocity = Random.insideUnitSphere * speed;

            writer.WriteLine(id + " " + time + " " + x + " " + y + " " + z);

            while (time < (i + 1) * (totalTime / numFish))
            {
                time += 0.1f;
                position += velocity * 0.1f;

                if (position.y < minDepth || position.y > maxDepth)
                {
                    velocity.y *= -1f;
                }

                writer.WriteLine(id + " " + time + " " + position.x + " " + position.y + " " + position.z);
            }
        }
    }
}

