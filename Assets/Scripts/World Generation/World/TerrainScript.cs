using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TerrainScript
{
    /**
     * Returneaza pozitia unui block bazat pe un vector3.
     */
    public static WorldPosition GetBlockPos(Vector3 pos)
    {
        WorldPosition blockPos = new WorldPosition(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));

        return blockPos;
    }

    /**
     * Overload la functia de mai sus.
     * Ia pozitia unui block bazat pe un raycast.
     * Deasemenea returneaza block-ul lovit de raycast sau blocul adiacent bazat pe booleanul adiacent.
     */
    public static WorldPosition GetBlockPos(RaycastHit hit, bool adiacent = false)
    {
        Vector3 pos = new Vector3(MoveWithinBlock(hit.point.x, hit.normal.x, adiacent), MoveWithinBlock(hit.point.y, hit.normal.y, adiacent), MoveWithinBlock(hit.point.z, hit.normal.z, adiacent));

        return GetBlockPos(pos);
    }

    /**
     * Functie ajutatoare ce returneaza un float cu coordonata blocului.
     * Functia returneaza pozitia coordonata adiacent daca adiacent este true.
     */
    static float MoveWithinBlock(float pos, float norm, bool adiacent = false)
    {
        if (pos - (int)pos == 0.5f || pos - (int)pos == -0.5f)
        {
            if (adiacent)
            {
                pos += (norm / 2);
            }
            else
            {
                pos -= (norm / 2);
            }
        }

        return (float)pos;
    }

    /**
     * Functie ce setaeaza un block in chunk-ul lovit de raycast.
     * Deasemenea putem lua blockul adiacent setand adiacent true.
     */
    public static bool SetBlock(RaycastHit hit, Block block, bool adiacent = false)
    {
        Chunk chunk = hit.collider.GetComponent<Chunk>();
        if (chunk == null)
            return false;

        WorldPosition pos = GetBlockPos(hit, adiacent);

        chunk.world.SetBlock(pos.x, pos.y, pos.z, block);

        return true;
    }

    /**
     * Asemanator cu SetBlock doar ca returneaza un block din chunk-ul lovit de raycast.
     */
    public static Block GetBlock(RaycastHit hit, bool adiacent = false)
    {
        Chunk chunk = hit.collider.GetComponent<Chunk>();
        if (chunk == null)
            return null;

        WorldPosition pos = GetBlockPos(hit, adiacent);

        Block block = chunk.world.GetBlock(pos.x, pos.y, pos.z);

        return block;
    }
}
