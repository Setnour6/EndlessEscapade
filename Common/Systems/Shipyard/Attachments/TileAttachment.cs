﻿using Terraria;
using Terraria.DataStructures;

namespace EndlessEscapade.Common.Systems.Shipyard.Attachments;

public abstract class TileAttachment : IAttachment
{
    public readonly int Type;

    public TileAttachment(int type) {
        Type = type;
    }

    public abstract Point16 Offset { get; }

    public virtual bool Generate(int x, int y) {
        var mod = EndlessEscapade.Instance;
        var origin = new Point16(x, y) + Offset;

        return WorldGen.PlaceTile(origin.X, origin.Y, Type, true, true);
    }
}
