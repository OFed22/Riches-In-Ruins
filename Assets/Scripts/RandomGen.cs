using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGen : MonoBehaviour
{
    [Header("Room Prefabs")]
    public GameObject[] roomPrefabs; // Array of 2D room prefabs

    [Header("Corridor Settings")]
    public float corridorWidth = 2.0f; // Width of the corridor
    public float minCorridorLength = 5.0f; // Minimum corridor length
    public float maxCorridorLength = 15.0f; // Maximum corridor length
    public Color corridorColor = Color.gray; // Color of the corridor

    [Header("Generation Settings")]
    public int numberOfRooms = 5; // Number of rooms to generate

    private Vector3 currentExitPosition; // Tracks the last exit position
    private List<GameObject> generatedRooms = new List<GameObject>();

    void Start()
    {
        GenerateRooms();
    }

    void GenerateRooms()
    {
        for (int i = 0; i < numberOfRooms; i++)
        {
            // Choose a random room prefab
            GameObject roomPrefab = roomPrefabs[Random.Range(0, roomPrefabs.Length)];
            GameObject newRoom = Instantiate(roomPrefab, transform);

            Transform entrance = newRoom.transform.Find("Entrance");
            Transform exit = newRoom.transform.Find("Exit");

            if (entrance != null && exit != null)
            {
                if (i == 0)
                {
                    // Place the first room at the origin
                    newRoom.transform.position = Vector3.zero;
                    currentExitPosition = exit.position;
                }
                else
                {
                    // Align the new room's entrance with the previous room's exit
                    Vector3 offset = currentExitPosition - entrance.position;
                    newRoom.transform.position += offset;

                    // Generate a random corridor between rooms
                    GenerateCorridor(currentExitPosition, entrance.position);
                    currentExitPosition = exit.position;
                }

                generatedRooms.Add(newRoom);
            }
            else
            {
                Debug.LogWarning($"Room prefab '{roomPrefab.name}' is missing Entrance or Exit.");
            }
        }
    }

    void GenerateCorridor(Vector3 start, Vector3 end)
    {
        // Calculate direction and distance
        Vector3 direction = (end - start).normalized;
        float distance = Vector3.Distance(start, end);

        // Decide the random length of the corridor
        float corridorLength = Random.Range(minCorridorLength, Mathf.Min(maxCorridorLength, distance));

        // Create a new GameObject for the corridor
        GameObject corridor = new GameObject("Corridor");
        corridor.transform.position = start + direction * (corridorLength / 2); // Center it
        corridor.transform.rotation = Quaternion.identity; // Keep it flat (2D)

        // Add a SpriteRenderer and define its size and color
        SpriteRenderer sr = corridor.AddComponent<SpriteRenderer>();
        sr.color = corridorColor;

        // Generate a procedural corridor sprite
        Texture2D texture = GenerateCorridorTexture(corridorWidth, corridorLength);
        Sprite corridorSprite = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f),
            100 // Pixels per unit
        );

        sr.sprite = corridorSprite;

        // Scale the corridor appropriately
        corridor.transform.localScale = new Vector3(corridorLength, corridorWidth, 1);
    }

    Texture2D GenerateCorridorTexture(float width, float length)
    {
        // Create a simple solid color texture for the corridor
        int texWidth = Mathf.CeilToInt(width * 100); // 100 pixels per unit
        int texLength = Mathf.CeilToInt(length * 100); // 100 pixels per unit
        Texture2D texture = new Texture2D(texLength, texWidth);

        Color[] pixels = new Color[texWidth * texLength];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = corridorColor;
        }

        texture.SetPixels(pixels);
        texture.Apply();

        return texture;
    }
}