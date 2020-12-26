using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public GameObject spawnTreeObject;
    private Terrain CurrentTerrain;

    public void Correct()
    {
        CurrentTerrain = GetComponent<Terrain>();

        for (int i = 0; i < CurrentTerrain.terrainData.treeInstances.Length; i++)
        {
            Vector3 WorldTreePos = Vector3.Scale(CurrentTerrain.terrainData.treeInstances[i].position, CurrentTerrain.terrainData.size) + CurrentTerrain.transform.position;
            GameObject a = Instantiate(spawnTreeObject, WorldTreePos, Quaternion.identity);
            a.name = a.name.Substring(0, a.name.LastIndexOf("(") - 1);
            a.transform.SetParent(transform);
        }

        CurrentTerrain.terrainData.treeInstances = new TreeInstance[0];
        Debug.Log("Trees had been spawned.");
    }

    public void Delete()
    {
        CurrentTerrain = GetComponent<Terrain>();
        for (int i = 0; i < CurrentTerrain.transform.childCount; i++) DestroyImmediate(CurrentTerrain.transform.GetChild(0).gameObject);
        Debug.Log("Trees had been deleted.");
    }
}
