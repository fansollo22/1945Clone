using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    public enum Direction { north, east, south, west, up, down };

    public struct Tile { public int x; public int y; }

    const float tileSize = 0.25f;

    public bool changed = true;

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
        meshData.hasColision = true;

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
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));

        meshData.AddQuadTriangles();

        meshData.uv.AddRange(UVs(Direction.up));

        return meshData;
    }

    /**
     * Functie ajutatoare ce adauga varfurile si triunghiurile pentru fata de jos a cubului la mesh.
     */
    protected virtual MeshData FaceDown(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

        meshData.AddQuadTriangles();

        meshData.uv.AddRange(UVs(Direction.down));

        return meshData;
    }

    /**
     * Functie ajutatoare ce adauga varfurile si triunghiurile pentru fata de nord.
     */
    protected virtual MeshData FaceNorth(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

        meshData.AddQuadTriangles();

        meshData.uv.AddRange(UVs(Direction.north));

        return meshData;
    }

    /**
     * Functie ajutatoare ce adauga varfurile si triunghiurile pentru fata de est.
     */
    protected virtual MeshData FaceEast(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));

        meshData.AddQuadTriangles();

        meshData.uv.AddRange(UVs(Direction.east));

        return meshData;
    }

    /**
     * Functie ajutatoare ce adauga varfurile si triunghiurile pentru fata de sud.
     */
    protected virtual MeshData FaceSouth(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));

        meshData.AddQuadTriangles();

        meshData.uv.AddRange(UVs(Direction.south));

        return meshData;
    }

    /**
     * Functie ajutatoare ce adauga varfurile si triunghiurile pentru fata de Vest.
     */
    protected virtual MeshData FaceWest(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));

        meshData.AddQuadTriangles();

        meshData.uv.AddRange(UVs(Direction.west));

        return meshData;
    }

    /**
     * Returneaza pozitia unei anumite texturi din tilesheet-ul assignat obiectului.
     */
    public virtual Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();
        tile.x = 0;
        tile.y = 0;
        return tile;
    }

    /**
     * Returneaza UV-urile pentru fata curenta.
     * Face asta pentru fiecare vertex si triunghi.
     */
    public virtual Vector2[] UVs(Direction direction)
    {
        Vector2[] UVs = new Vector2[4];
        Tile tilePos = TexturePosition(direction);
        UVs[0] = new Vector2(tileSize * tilePos.x + tileSize, tileSize * tilePos.y);
        UVs[1] = new Vector2(tileSize * tilePos.x + tileSize, tileSize * tilePos.y + tileSize);
        UVs[2] = new Vector2(tileSize * tilePos.x, tileSize * tilePos.y + tileSize);
        UVs[3] = new Vector2(tileSize * tilePos.x, tileSize * tilePos.y);
        return UVs;
    }
}
