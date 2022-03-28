using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public enum Direction { north, east, south, west, up, down };

    /**
     * Constructorul clasei.
     */
    public Block()
    {
    }

    /**
     * Returneaza mesh-ul blocului curent in functie de soliditatea fiecarei fete.
     */
    public virtual MeshData Blockdata(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        if (!chunk.GetBlock(x, y + 1, z).IsSolid(Direction.down))
        {
            meshData = FaceUp(chunk, x, y, z, meshData);
        }

        if (!chunk.GetBlock(x, y - 1, z).IsSolid(Direction.up))
        {
            meshData = FaceDown(chunk, x, y, z, meshData);
        }

        if (!chunk.GetBlock(x, y, z + 1).IsSolid(Direction.south))
        {
            meshData = FaceNorth(chunk, x, y, z, meshData);
        }

        if (!chunk.GetBlock(x, y, z - 1).IsSolid(Direction.north))
        {
            meshData = FaceSouth(chunk, x, y, z, meshData);
        }

        if (!chunk.GetBlock(x + 1, y, z).IsSolid(Direction.west))
        {
            meshData = FaceEast(chunk, x, y, z, meshData);
        }

        if (!chunk.GetBlock(x - 1, y, z).IsSolid(Direction.east))
        {
            meshData = FaceWest(chunk, x, y, z, meshData);
        }

        return meshData;
    }

    /**
     * Returneaza true in functie de directie. Asa stim ce fete sa randam si ce sa nu.
     */
    public virtual bool IsSolid(Direction direction)
    {
        switch (direction)
        {
            case Direction.north:
                return true;
            case Direction.east:
                return true;
            case Direction.south:
                return true;
            case Direction.west:
                return true;
            case Direction.up:
                return true;
            case Direction.down:
                return true;
        }
        return false;
    }

    /**
     * Functie ajutatoare ce adauga varfurile si triunghiurile pentru fata de sus a cubului la mesh.
     */
    protected virtual MeshData FaceUp(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));

        meshData.AddQuadTriangles();

        return meshData;
    }

    /**
     * Functie ajutatoare ce adauga varfurile si triunghiurile pentru fata de jos a cubului la mesh.
     */
    protected virtual MeshData FaceDown(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

        meshData.AddQuadTriangles();
        return meshData;
    }

    /**
     * Functie ajutatoare ce adauga varfurile si triunghiurile pentru fata de nord.
     */
    protected virtual MeshData FaceNorth(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

        meshData.AddQuadTriangles();
        return meshData;
    }

    /**
     * Functie ajutatoare ce adauga varfurile si triunghiurile pentru fata de est.
     */
    protected virtual MeshData FaceEast(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));

        meshData.AddQuadTriangles();
        return meshData;
    }

    /**
     * Functie ajutatoare ce adauga varfurile si triunghiurile pentru fata de sud.
     */
    protected virtual MeshData FaceSouth(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
        meshData.vertices.Add(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.vertices.Add(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));

        meshData.AddQuadTriangles();
        return meshData;
    }

    /**
     * Functie ajutatoare ce adauga varfurile si triunghiurile pentru fata de Vest.
     */
    protected virtual MeshData FaceWest(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
        meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.vertices.Add(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
        meshData.vertices.Add(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));

        meshData.AddQuadTriangles();
        return meshData;
    }
}
