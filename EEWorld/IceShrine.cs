using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EEMod.Tiles;
using Terraria.ModLoader;
using System.Threading.Tasks;
using Terraria.ID;
namespace EEMod.EEWorld
{
public partial class EEWorld
{
public static int[,,] IceShrine = new int[,,]
{
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.IceBlock,0,0,2,0,0,0,0,144,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBrick,0,0,0,0,0,0,0,36,54},{TileID.SnowBrick,0,0,1,0,0,0,0,90,54},{TileID.IceBlock,0,0,1,0,0,0,0,108,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBrick,0,0,0,0,0,0,0,0,36},{TileID.SnowBrick,0,0,0,0,0,0,0,54,54},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBlock,0,0,0,0,0,0,0,0,18},{TileID.SnowBrick,0,0,0,0,0,0,0,18,18},{TileID.SnowBrick,0,0,0,0,0,0,0,72,36},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.IceBrick,0,0,0,0,0,0,0,72,54},{TileID.IceBlock,0,0,0,0,0,0,0,54,54},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBrick,0,0,0,0,0,0,0,36,54},{TileID.SnowBrick,0,0,0,0,0,0,0,18,18},{TileID.SnowBrick,0,0,0,0,0,0,0,72,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBrick,0,0,0,0,0,0,0,0,0},{TileID.SnowBrick,0,0,0,0,0,0,0,36,18},{TileID.SnowBrick,0,0,0,0,0,0,0,18,18},{TileID.SnowBrick,0,0,1,0,0,0,0,18,54},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.IceBlock,0,0,0,0,0,0,0,0,54},{TileID.IceBlock,0,0,0,0,0,0,0,18,54},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.IceBlock,0,0,0,0,0,0,0,0,0},{TileID.IceBlock,0,0,0,0,0,0,0,18,18},{TileID.IceBlock,0,0,0,0,0,0,0,54,0},{TileID.IceBlock,0,0,0,0,0,0,0,216,18},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBrick,0,0,0,0,0,0,0,0,18},{TileID.SnowBlock,0,0,0,0,0,0,0,54,18},{TileID.SnowBlock,0,0,0,0,0,0,0,18,18},{TileID.SnowBlock,0,0,1,0,0,0,0,90,54},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.IceBlock,0,0,0,0,0,0,0,0,36},{TileID.IceBlock,0,0,0,0,0,0,0,144,162},{TileID.SnowBlock,0,0,0,0,0,0,0,18,18},{TileID.SnowBrick,0,0,0,0,0,0,0,72,36},{0,0,0,0,0,0,0,0,0,0},{TileID.IceBlock,0,0,0,0,0,0,0,162,18},{TileID.IceBrick,0,0,0,0,0,0,0,54,0},{TileID.IceBlock,0,0,0,0,0,0,0,54,18},{TileID.IceBlock,0,0,0,0,0,0,0,72,18},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.Stalactite,0,0,0,0,0,0,0,0,72},{TileID.IceBrick,0,0,0,0,0,0,0,0,36},{TileID.IceBlock,0,0,0,0,0,0,0,18,18},{TileID.IceBlock,0,0,1,0,0,0,0,90,54},{TileID.SnowBlock,0,0,0,0,0,0,0,72,54},{TileID.SnowBlock,0,0,0,0,0,0,0,18,18},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.SnowBlock,WallID.SnowBrick,0,0,26,0,0,0,36,36},{TileID.SnowBlock,0,0,0,0,0,0,0,54,36},{TileID.SnowBlock,0,0,1,0,0,0,0,216,36},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBlock,0,0,4,0,0,0,0,144,54},{TileID.IceBlock,0,0,0,0,0,0,0,108,54},{TileID.SnowBlock,0,0,0,0,0,0,0,0,0},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.SnowBlock,0,0,0,0,0,0,0,54,54},{TileID.IceBlock,0,0,2,0,0,0,0,0,54},{TileID.IceBlock,0,0,0,0,0,0,0,18,18},{TileID.IceBlock,0,0,0,0,0,0,0,72,36},{TileID.Stalactite,0,0,0,0,0,0,0,0,72},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.IceBlock,0,0,0,0,0,0,0,108,54},{TileID.IceBlock,0,0,4,0,0,0,0,0,72},{TileID.IceBlock,0,0,0,0,0,0,0,18,90},{TileID.IceBlock,0,0,0,0,0,0,0,54,126},{TileID.SnowBlock,0,0,0,0,0,0,0,18,18},{TileID.SnowBlock,0,0,0,0,0,0,0,54,72},{TileID.Stalactite,0,0,0,0,0,0,0,36,72},{TileID.WoodenBeam,0,26,0,0,0,0,0,90,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,WallID.SnowBrick,0,0,0,0,0,0,0,0},{0,WallID.SnowBrick,0,0,0,0,0,0,0,0},{TileID.HangingLanterns,0,26,0,0,0,0,0,0,108},{TileID.SnowBlock,WallID.IceFloeWallpaper,0,0,0,0,0,0,36,72},{TileID.SnowBlock,0,0,0,0,0,0,0,54,36},{TileID.IceBlock,0,0,0,0,0,0,0,36,126},{TileID.IceBrick,0,0,0,0,0,0,0,54,18},{TileID.IceBlock,0,0,3,0,0,0,0,18,72},{TileID.IceBlock,0,0,0,0,0,0,0,90,18},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBlock,0,0,0,0,0,0,0,36,54},{TileID.IceBlock,0,0,0,0,0,0,0,36,180},{TileID.IceBrick,0,0,0,0,0,0,0,54,18},{TileID.IceBlock,0,0,0,0,0,0,0,90,144},{0,WallID.SnowBrick,0,0,0,0,0,0,0,0},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{TileID.WoodenBeam,0,26,0,0,0,0,0,90,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,WallID.SnowBrick,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.HangingLanterns,0,26,0,0,0,0,0,0,126},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{TileID.IceBrick,WallID.IceFloeWallpaper,0,2,0,0,0,0,72,54},{TileID.IceBlock,0,0,0,0,0,0,0,0,90},{TileID.IceBlock,0,0,0,0,0,0,0,54,144},{TileID.SnowBlock,0,0,0,0,0,0,0,18,54},{TileID.Stalactite,0,0,0,0,0,0,0,36,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBlock,0,0,0,0,0,0,0,162,36},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.SnowBlock,0,0,0,0,0,0,0,18,18},{TileID.IceBlock,0,0,0,0,0,0,0,0,216},{TileID.IceBlock,0,0,0,0,0,0,0,36,18},{TileID.IceBlock,WallID.SnowBrick,0,1,0,0,0,0,90,54},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{TileID.WoodenBeam,0,26,0,0,0,0,0,90,18},{0,WallID.SnowBrick,0,0,0,0,0,0,0,0},{0,WallID.SnowBrick,0,0,0,0,0,0,0,0},{0,WallID.SnowBrick,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.IceBlock,0,0,2,0,0,0,0,0,54},{TileID.IceBlock,0,0,0,0,0,0,0,18,18},{TileID.IceBlock,0,0,3,0,0,0,0,18,72},{TileID.SnowBlock,0,0,0,0,0,0,0,0,18},{TileID.SnowBlock,0,0,0,0,0,0,0,72,0},{TileID.Stalactite,0,0,0,0,0,0,0,36,18},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.IceBlock,0,0,0,0,0,0,0,162,18},{TileID.IceBlock,0,0,0,0,0,0,0,162,180},{TileID.IceBlock,0,0,0,0,0,0,0,90,144},{0,WallID.SnowBrick,0,0,0,0,0,0,0,0},{TileID.IceBrick,WallID.SnowBrick,0,0,0,0,0,0,72,72},{TileID.IceBlock,0,0,0,0,0,0,0,18,72},{0,0,0,0,0,0,0,0,0,0},{TileID.WoodenBeam,0,26,0,0,0,0,0,90,18},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,WallID.SnowBrick,0,0,0,0,0,0,0,0},{0,WallID.SnowBrick,0,0,0,0,0,0,0,0},{0,WallID.SnowBrick,0,0,0,0,0,0,0,0},{TileID.IceBlock,0,0,0,0,0,0,0,0,36},{TileID.IceBlock,WallID.MudstoneBrick,0,0,0,0,0,0,54,72},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{TileID.SnowBlock,0,0,0,0,0,0,0,72,72},{TileID.SnowBlock,0,0,0,0,0,0,0,18,36},{TileID.SnowBlock,0,0,0,0,0,0,0,54,54},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBlock,0,0,0,0,0,0,0,72,54},{TileID.SnowBlock,0,0,0,0,0,0,0,72,36},{TileID.Stalactite,0,0,0,0,0,0,0,18,0},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{TileID.WoodenBeam,0,26,0,0,0,0,0,90,18},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,WallID.SnowBrick,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.Stalactite,0,0,0,0,0,0,0,18,72},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{TileID.Stalactite,0,0,0,0,0,0,0,0,72},{TileID.IceBlock,0,0,0,0,0,0,0,108,0},{TileID.SnowBlock,0,0,0,0,0,0,0,90,18},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBlock,0,0,0,0,0,0,0,72,54},{TileID.SnowBlock,0,0,0,0,0,0,0,18,18},{TileID.SnowBlock,0,0,0,0,0,0,0,54,72},{TileID.Stalactite,0,0,0,0,0,0,0,18,18},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{TileID.WoodenBeam,0,26,0,0,0,0,0,90,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{TileID.IceBlock,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,36},{TileID.IceBlock,0,0,0,0,0,0,0,90,144},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBlock,0,0,2,0,0,0,0,72,54},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.SnowBlock,0,0,0,0,0,0,0,90,72},{TileID.Stalactite,WallID.MudstoneBrick,0,0,26,0,0,0,0,72},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,WallID.SnowBrick,0,0,0,0,0,0,0,0},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{TileID.WoodenBeam,0,26,0,0,0,0,0,90,18},{0,0,0,0,0,0,0,0,0,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{TileID.Stalactite,WallID.IceFloeWallpaper,0,0,0,0,0,0,36,72},{TileID.IceBlock,0,0,0,0,0,0,0,0,36},{TileID.IceBlock,0,0,0,0,0,0,0,18,54},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBlock,0,0,0,0,0,0,0,0,0},{TileID.SnowBlock,0,0,0,0,0,0,0,72,18},{TileID.IceBlock,0,0,0,0,0,0,0,108,0},{TileID.Torches,0,0,0,0,0,0,0,22,198},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{0,WallID.SnowBrick,0,0,0,0,0,0,0,0},{0,WallID.SnowBrick,0,0,0,0,0,0,0,0},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{TileID.WoodenBeam,0,26,0,0,0,0,0,90,36},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.IceBlock,0,0,0,0,0,0,0,0,18},{TileID.SnowBrick,0,0,0,0,0,0,0,72,36},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBrick,0,0,2,0,0,0,0,0,54},{TileID.SnowBlock,0,0,0,0,0,0,0,54,18},{TileID.IceBlock,0,0,0,0,0,0,0,36,162},{TileID.IceBlock,0,0,0,0,0,0,0,72,0},{0,WallID.SnowBrick,0,0,0,0,0,0,0,0},{TileID.IceBlock,WallID.SnowBrick,0,0,0,0,0,0,162,36},{TileID.IceBlock,WallID.IceFloeWallpaper,0,0,0,0,0,0,54,54},{0,WallID.SnowBrick,0,0,0,0,0,0,0,0},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.WoodenBeam,WallID.MudstoneBrick,26,0,26,0,0,0,90,36},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{TileID.IceBlock,WallID.SnowBrick,0,0,0,0,0,0,108,108},{0,0,0,0,0,0,0,0,0,0},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{0,WallID.IceFloeWallpaper,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBrick,WallID.MudstoneBrick,0,0,26,0,0,0,126,0},{0,WallID.MudstoneBrick,0,0,26,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.IceBlock,0,0,0,0,0,0,0,0,36},{TileID.SnowBrick,0,0,0,0,0,0,0,18,18},{TileID.SnowBlock,0,0,1,0,0,0,0,54,54},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBrick,0,0,0,0,0,0,0,0,18},{TileID.SnowBlock,0,0,0,0,0,0,0,54,18},{TileID.SnowBrick,0,0,0,0,0,0,0,36,18},{TileID.SnowBrick,0,0,0,0,0,0,0,54,18},{TileID.SnowBrick,0,0,0,0,0,0,0,36,0},{TileID.SnowBlock,0,0,0,0,0,0,0,54,18},{TileID.IceBrick,0,0,0,0,0,0,0,18,18},{TileID.IceBrick,0,0,0,0,0,0,0,36,0},{TileID.IceBrick,0,0,0,0,0,0,0,54,0},{TileID.SnowBlock,0,0,0,0,0,0,0,90,54},{TileID.IceBlock,0,0,0,0,0,0,0,108,0},{TileID.SnowBlock,0,0,0,0,0,0,0,72,54},{TileID.SnowBlock,0,0,0,0,0,0,0,108,18},{TileID.IceBlock,0,0,0,0,0,0,0,0,198},{TileID.IceBlock,0,0,0,0,0,0,0,270,0},{TileID.IceBlock,0,0,0,0,0,0,0,216,0},{TileID.SnowBlock,0,0,0,0,0,0,0,0,54},{TileID.SnowBrick,0,0,0,0,0,0,0,126,18},{TileID.SnowBrick,0,0,0,0,0,0,0,54,0},{TileID.SnowBrick,WallID.MudstoneBrick,0,0,26,0,0,0,36,0},{TileID.IceBrick,WallID.MudstoneBrick,0,0,26,0,0,0,18,0},{TileID.IceBrick,0,0,0,0,0,0,0,18,18},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.SnowBlock,0,0,0,0,0,0,0,72,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBlock,0,0,4,0,0,0,0,0,72},{TileID.SnowBlock,0,0,0,0,0,0,0,36,36},{TileID.SnowBlock,0,0,3,0,0,0,0,54,72},{TileID.SnowBlock,0,0,4,0,0,0,0,72,72},{TileID.SnowBlock,0,0,0,0,0,0,0,18,36},{TileID.SnowBlock,0,0,0,0,0,0,0,54,18},{TileID.SnowBlock,0,0,0,0,0,0,0,18,18},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.SnowBlock,0,0,0,0,0,0,0,18,18},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.IceBlock,0,0,0,0,0,0,0,180,144},{TileID.SnowBlock,0,0,0,0,0,0,0,54,18},{TileID.IceBlock,0,0,0,0,0,0,0,216,108},{TileID.IceBlock,0,0,0,0,0,0,0,54,144},{TileID.SnowBlock,0,0,0,0,0,0,0,54,18},{TileID.SnowBlock,0,0,0,0,0,0,0,54,0},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.SnowBrick,0,0,0,0,0,0,0,54,18},{TileID.SnowBlock,0,0,0,0,0,0,0,18,18},{TileID.SnowBlock,0,0,0,0,0,0,0,36,36},{TileID.SnowBlock,0,0,0,0,0,0,0,54,18},{TileID.SnowBlock,0,0,0,0,0,0,0,54,18},{TileID.SnowBlock,0,0,0,0,0,0,0,54,36},{TileID.SnowBlock,0,0,3,0,0,0,0,90,72},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.Stalactite,0,0,0,0,0,0,0,36,72},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBrick,0,0,0,0,0,0,0,0,72},{TileID.SnowBrick,0,0,0,0,0,0,0,54,18},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.IceBlock,0,0,0,0,0,0,0,36,162},{TileID.IceBlock,0,0,0,0,0,0,0,54,180},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.SnowBlock,0,0,0,0,0,0,0,54,18},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.SnowBlock,0,0,0,0,0,0,0,18,18},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.IceBlock,0,0,0,0,0,0,0,36,126},{TileID.IceBlock,0,0,0,0,0,0,0,144,126},{TileID.SnowBlock,0,0,0,0,0,0,0,18,72},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBlock,0,0,4,0,0,0,0,0,72},{TileID.SnowBlock,0,0,0,0,0,0,0,90,72},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBlock,0,0,0,0,0,0,0,0,72},{TileID.SnowBlock,0,0,0,0,0,0,0,36,36},{TileID.IceBlock,0,0,0,0,0,0,0,36,162},{TileID.IceBlock,0,0,0,0,0,0,0,144,144},{TileID.SnowBlock,0,0,0,0,0,0,0,18,18},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.SnowBrick,0,0,0,0,0,0,0,36,18},{TileID.SnowBrick,0,0,0,0,0,0,0,18,18},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.IceBlock,0,0,0,0,0,0,0,36,108},{TileID.IceBlock,0,0,0,0,0,0,0,18,126},{TileID.IceBlock,0,0,1,0,0,0,0,54,54},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.Stalactite,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.Stalactite,0,0,0,0,0,0,0,18,0},{0,0,0,0,0,0,0,0,0,0},{TileID.IceBlock,0,0,0,0,0,0,0,0,0},{TileID.IceBlock,0,0,0,0,0,0,0,144,126},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.SnowBlock,0,0,0,0,0,0,0,54,18},{TileID.SnowBlock,0,0,0,0,0,0,0,18,18},{TileID.IceBlock,0,0,0,0,0,0,0,162,144},{TileID.IceBlock,0,0,0,0,0,0,0,54,90},{TileID.SnowBlock,0,0,0,0,0,0,0,36,18},{TileID.SnowBlock,0,0,0,0,0,0,0,54,36},{TileID.IceBlock,0,0,0,0,0,0,0,162,126},{TileID.IceBlock,0,0,0,0,0,0,0,36,18},{TileID.IceBlock,0,0,1,0,0,0,0,90,54},{0,0,0,0,0,0,0,0,0,0},{TileID.Stalactite,0,0,0,0,0,0,0,0,18},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.Stalactite,0,0,0,0,0,0,0,18,18},{TileID.IceBlock,0,0,2,0,0,0,0,36,54},{TileID.IceBlock,0,0,0,0,0,0,0,18,18},{TileID.IceBlock,0,0,3,0,0,0,0,54,72},{TileID.SnowBlock,0,0,0,0,0,0,0,0,72},{TileID.SnowBlock,0,0,0,0,0,0,0,54,18},{TileID.IceBrick,0,0,0,0,0,0,0,54,18},{TileID.IceBrick,0,0,0,0,0,0,0,54,18},{TileID.IceBlock,0,0,0,0,0,0,0,144,162},{TileID.SnowBlock,0,0,0,0,0,0,0,90,72},{0,0,0,0,0,0,0,0,0,0},{TileID.Stalactite,0,0,0,0,0,0,0,18,72},{TileID.IceBlock,0,0,4,0,0,0,0,72,72},{TileID.IceBlock,0,0,3,0,0,0,0,90,72},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.IceBlock,0,0,4,0,0,0,0,0,72},{TileID.IceBlock,0,0,0,0,0,0,0,72,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.SnowBlock,0,0,0,0,0,0,0,36,72},{TileID.IceBrick,0,0,0,0,0,0,0,54,36},{TileID.SnowBlock,0,0,0,0,0,0,0,18,36},{TileID.SnowBrick,0,0,0,0,0,0,0,18,72},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
{{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.Stalactite,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{TileID.Stalactite,0,0,0,0,0,0,0,36,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0,0,0}},
};
}
}
