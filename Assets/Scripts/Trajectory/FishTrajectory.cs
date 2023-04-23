using System.Collections.Generic;
using UnityEngine;

public class FishTrajectory : MonoBehaviour
{
    public GameObject fishPrefab; // assign the fish prefab to this field in the Inspector
    public string dataFileName; // the name of the text file in the "Resources" folder

    private Dictionary<int, List<Vector3>> fishTrajectories = new Dictionary<int, List<Vector3>>();

    private void Start()
    {
        // Load trajectory data from text file using Resources.Load
        TextAsset textAsset = Resources.Load<TextAsset>(dataFileName);
        string[] lines = textAsset.text.Split('\n');
        foreach (string line in lines)
        {
            string[] values = line.Split(' ');
            int id = int.Parse(values[0]);
            float time = float.Parse(values[1]);
            float x = float.Parse(values[2]);
            float y = float.Parse(values[3]);
            float z = float.Parse(values[4]);
            Vector3 position = new Vector3(x, y, z);
            if (!fishTrajectories.ContainsKey(id))
            {
                fishTrajectories[id] = new List<Vector3>();
            }
            fishTrajectories[id].Add(position);
        }

        // Create fish objects that follow the trajectories
        foreach (int id in fishTrajectories.Keys)
        {
            List<Vector3> trajectory = fishTrajectories[id];
            GameObject fish = Instantiate(fishPrefab);
            FishMovement fishMovement = fish.GetComponent<FishMovement>();
            fishMovement.SetTrajectory(trajectory);
        }
    }
}

