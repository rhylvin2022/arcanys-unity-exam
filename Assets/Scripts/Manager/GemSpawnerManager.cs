using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawnerManager : Singleton<GemSpawnerManager> 
{
    public List<GameObject> gems;
    public List<Transform> gemLocations;
    public List<Transform> gemExtraLocations;

    private void Awake()
    {
        SpawnGem(25);
    }

    public void SpawnGem(int count )
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randomPosition = gemLocations[i].position;
            GemHandler gemHandler = Instantiate(gems[Random.Range(0,10)], randomPosition, Quaternion.identity).GetComponent<GemHandler>();
            gemHandler.gemSpawnerLocation = gemLocations[i];
        }
    }

    public void SpawnGem(Transform lastGemLocation)
    {
        int gemIndex = Random.Range(0, gemExtraLocations.Count);
        Transform randomTransform = gemExtraLocations[gemIndex];
        Vector3 randomPosition = randomTransform.position;
        GemHandler gemHandler = Instantiate(gems[Random.Range(0, 10)], randomPosition, Quaternion.identity).GetComponent<GemHandler>();
        gemHandler.gemSpawnerLocation = randomTransform;
        gemExtraLocations.RemoveAt(gemIndex);
        gemExtraLocations.Add(lastGemLocation);
    }
}
