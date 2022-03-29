using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Chunk : MonoBehaviour
{
    Block[,,] blocks;
    public static int chunkSize = 16;
    public bool update = true;

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

        blocks = new Block[chunkSize, chunkSize, chunkSize];

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    blocks[x, y, z] = new BlockAir();
                }
            }
        }
        blocks[3, 5, 2] = new Block();
        blocks[4, 5, 2] = new BlockGrass();
        UpdateChunk();
    }

    /**
     * Returneaza blocul de pe pozitia (x,y,z).
     */
    public Block GetBlock(int x, int y, int z)
    {
        return blocks[x, y, z];
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
}
