using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public Dictionary<WorldPosition, Chunk> chunkDictionary = new Dictionary<WorldPosition, Chunk>();

    public GameObject chunk;


    // Start is called before the first frame update
    void Start()
    {
        for (int x = -2; x < 2; x++)
        {
            for (int y = -1; y < 1; y++)
            {
                for (int z = -1; z < 1; z++)
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

        for (int xi = 0; xi < 16; xi++)
        {
            for (int yi = 0; yi < 16; yi++)
            {
                for (int zi = 0; zi < 16; zi++)
                {
                    if (yi <= 7)
                    {
                        SetBlock(x + xi, y + yi, z + zi, new BlockGrass());
                    }
                    else
                    {
                        SetBlock(x + xi, y + yi, z + zi, new BlockAir());
                    }
                }
            }
        }
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
     */
    public void SetBlock(int x, int y, int z, Block block)
    {
        Chunk chunk = GetChunk(x, y, z);

        if (chunk != null)
        {
            chunk.SetBlock(x - chunk.pos.x, y - chunk.pos.y, z - chunk.pos.z, block);
            chunk.update = true;
        }
    }
}
