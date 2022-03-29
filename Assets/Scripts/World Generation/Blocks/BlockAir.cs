using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAir : Block
{
    public BlockAir() : base()
    {
    }

    /**
     * Da override la functia base. Fiind un bloc de aer, nu avem nevoie de meshdata.
     */
    public override MeshData Blockdata(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        return meshData;
    }
    /**
     *  Da override la functia base. Fiind un bloc de aer, nu avem nevoie sa fie solid.
     */
    public override bool IsSolid(Block.Direction direction)
    {
        return false;
    }
}
