using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public Dictionary<WorldPosition, Chunk> chunkDictionary = new Dictionary<WorldPosition, Chunk>();

    public GameObject chunk;

    // Start is called before the first frame update
    void Start()
    {
        for (int x = -4; x < 4; x++)
        {
            for (int y = -1; y < 3; y++)
            {
                for (int z = -4; z < 4; z++)
                {
                    CreateChunk(x * 16, y * 16, z * 16);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /**
     * Functie ce creaza un chunk bazat pe pozitia data si adauga blocurile.
     */
    public void CreateChunk(int x, int y, int z)
    {
        WorldPosition worldPos = new WorldPosition(x, y, z);

        GameObject newChunkObject = Instantiate(chunk, new Vector3(worldPos.x, worldPos.y, worldPos.z), Quaternion.Euler(Vector3.zero)) as GameObject;

        Chunk newChunk = newChunkObject.GetComponent<Chunk>();

        newChunk.pos = worldPos;
        newChunk.world = this;

        chunkDictionary.Add(worldPos, newChunk);

        TerrainGeneration terrainGen = new TerrainGeneration();
        newChunk = terrainGen.ChunkGen(newChunk);
        newChunk.SetBlocksUnmodified();
    }

    /**
     * Distruge un chunk.
     */
    public void DestroyChunk(int x, int y, int z)
    {
        Chunk chunk = null;
        if (chunkDictionary.TryGetValue(new WorldPosition(x, y, z), out chunk))
        {
            Object.Destroy(chunk.gameObject);
            chunkDictionary.Remove(new WorldPosition(x, y, z));
        }
    }

    /**
     * Functie ce returneaza un chunk bazat pe pozitia oferita.
     */
    public Chunk GetChunk(int x, int y, int z)
    {
        WorldPosition pos = new WorldPosition();
        float multiple = Chunk.chunkSize;
        pos.x = Mathf.FloorToInt(x / multiple) * Chunk.chunkSize;
        pos.y = Mathf.FloorToInt(y / multiple) * Chunk.chunkSize;
        pos.z = Mathf.FloorToInt(z / multiple) * Chunk.chunkSize;
        Chunk containerChunk = null;
        chunkDictionary.TryGetValue(pos, out containerChunk);

        return containerChunk;
    }

    /**
     * Functie ce returneaza un block dintr-un chunk bazat pe pozitia data.
     */
    public Block GetBlock(int x, int y, int z)
    {
        Chunk containerChunk = GetChunk(x, y, z);
        if (containerChunk != null)
        {
            Block block = containerChunk.GetBlock(x - containerChunk.pos.x, y - containerChunk.pos.y, z - containerChunk.pos.z);
            return block;
        }
        return new BlockAir();
    }

    /**
     * Adauga block in chunk-ul de pe pozitia x,y,z.
     * Deasemenea setam update = true pentru a updata mesh-ul chunkului.
     * 
     * Tot odata verificam daca chunk-urile adiacente au fost lovite.
     * Daca au fost lovite (pentru fiecare coordonata x,y,z verificam daca pozitia locala este inafara chunk-ului curent) updatam chunk-urile. 
     */
    public void SetBlock(int x, int y, int z, Block block)
    {
        Chunk chunk = GetChunk(x, y, z);

        if (chunk != null)
        {
            chunk.SetBlock(x - chunk.pos.x, y - chunk.pos.y, z - chunk.pos.z, block);
            chunk.update = true;

            UpdateIfEqual(x - chunk.pos.x, 0, new WorldPosition(x - 1, y, z));
            UpdateIfEqual(x - chunk.pos.x, Chunk.chunkSize - 1, new WorldPosition(x + 1, y, z));
            UpdateIfEqual(y - chunk.pos.y, 0, new WorldPosition(x, y - 1, z));
            UpdateIfEqual(y - chunk.pos.y, Chunk.chunkSize - 1, new WorldPosition(x, y + 1, z));
            UpdateIfEqual(z - chunk.pos.z, 0, new WorldPosition(x, y, z - 1));
            UpdateIfEqual(z - chunk.pos.z, Chunk.chunkSize - 1, new WorldPosition(x, y, z + 1));
        }
    }

    /**
     * Functie ce ia chunk-ul si il seteaza pentru updatare daca coordonatele sunt egale.
     */
    void UpdateIfEqual(int value1, int value2, WorldPosition pos)
    {
        if (value1 == value2)
        {
            Chunk chunk = GetChunk(pos.x, pos.y, pos.z);
            if (chunk != null)
                chunk.update = true;
        }
    }
}
