using UnityEngine;
public class textureMovement : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // Adjust the speed of scrolling

    private Terrain terrain;
    private TerrainData terrainData;
    private int textureIndex = 0; // The index of the texture you want to scroll

    private void Start()
    {
        // Get reference to the terrain component
        terrain = GetComponent<Terrain>();
        terrainData = terrain.terrainData;
    }

    private void Update()
    {
        // Calculate texture offset based on time and scroll speed
        float offset = Time.time * scrollSpeed;

        // Apply the offset to the terrain texture
        Vector2 textureOffset = new Vector2(offset, 0);
        terrainData.terrainLayers[textureIndex].tileOffset = textureOffset;
    }
}
