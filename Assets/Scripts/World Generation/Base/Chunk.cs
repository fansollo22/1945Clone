using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Chunk : MonoBehaviour
{
    public Block[,,] blocks = new Block[chunkSize, chunkSize, chunkSize];
    public static int chunkSize = 16;
    public bool update = true;

    public WorldGeneration world;
    public WorldPosition pos;

    MeshFilter filter;
    MeshCollider col;

    /**
     * Functie ce ruleaza la pornirea jocului.
     * Creaza chunk-uri si blocuri bazat pe atributele clasei.
     */
    void Start()
    {
        filter = gameObject.GetComponent<MeshFilter>();
        col = gameObject.GetComponent<MeshCollider>();
    }

    /**
     * Functie default de Unity ce este chemata odata la fiecare frame.
     */
    void Update()
    {
        if (update)
        {
            update = false;
            UpdateChunk();
        }
    }

    /**
     * Updateaza chunk-ul si blocurile din interior.
     */
    void UpdateChunk()
    {
        MeshData meshData = new MeshData();
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    meshData = blocks[x, y, z].Blockdata(this, x, y, z, meshData);
                }
            }
        }
        RenderMesh(meshData);
    }

    /**
     * Cauta block-ul recursiv. Daca nu este in chunk-ul de pe pozitia x,y,z
     * Chemam din nou functia sarind la urmatorul chunk.
     */
    public Block GetBlock(int x, int y, int z)
    {
        if (InRange(x) && InRange(y) && InRange(z))
            return blocks[x, y, z];

        return world.GetBlock(pos.x + x, pos.y + y, pos.z + z);
    }

    /**
     * Functie ce adauga block-uri in chunk-ul curent bazat pe coordonatele x,y,z.
     * Daca coordonatele nu sunt in range, trecem la chunk-ul urmator.
     */
    public void SetBlock(int x, int y, int z, Block block)
    {
        if (InRange(x) && InRange(y) && InRange(z))
        {
            blocks[x, y, z] = block;
        }
        else
        {
            world.SetBlock(pos.x + x, pos.y + y, pos.z + z, block);
        }
    }

    /**
     * Functie ajutatoare ce ne spune daca o coordonata este in range-ul chunk-ului.
     */
    public static bool InRange(int index)
    {
        if (index < 0 || index >= chunkSize)
            return false;

        return true;
    }

    /**
     * Assigneaza noul mesh generat la obiect impreuna cu coliziune.
     */
    void RenderMesh(MeshData meshData)
    {
        filter.mesh.Clear();
        filter.mesh.vertices = meshData.vertices.ToArray();
        filter.mesh.triangles = meshData.triangles.ToArray();

        filter.mesh.uv = meshData.uv.ToArray();
        filter.mesh.RecalculateNormals();

        col.sharedMesh = null;
        Mesh mesh = new Mesh();
        mesh.vertices = meshData.colVertices.ToArray();
        mesh.triangles = meshData.colTriangles.ToArray();
        mesh.RecalculateNormals();

        col.sharedMesh = mesh;
    }

    public void SetBlocksUnmodified()
    {
        foreach (Block block in blocks)
        {
            block.changed = false;
        }
    }
}
