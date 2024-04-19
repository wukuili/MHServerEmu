﻿using MHServerEmu.Core.Extensions;
using MHServerEmu.Core.Logging;
using MHServerEmu.Core.VectorMath;
using MHServerEmu.Games.GameData;
using MHServerEmu.Games.GameData.Prototypes;
using MHServerEmu.Games.Navi;
using MHServerEmu.Games.Regions;

namespace MHServerEmu.Games.Entities
{
    public class RegionLocation
    {
        public static readonly Logger Logger = LogManager.CreateLogger();

        private Region _region;
        public Region Region { get => _region; set { _region = value; Cell = null; } }
        public Cell Cell { get; set; }
        public Area Area { get => Cell?.Area; }
        public ulong RegionId { get => Region != null ? Region.Id : 0; }
        public uint AreaId { get => Area != null ? Area.Id : 0; }
        public uint CellId { get => Cell != null ? Cell.Id : 0; }
        public NaviMesh NaviMesh { get => Region?.NaviMesh; }
        public bool IsValid() => _region != null;

        private Vector3 _position;
        public Vector3 Position
        {
            get => IsValid() ? _position : Vector3.Zero;
            set 
            {
                if (!Vector3.IsFinite(value))
                {
                    Logger.Warn($"Non-finite position ({value}) given to region location: {ToString()}");
                    return;
                }
                if (_region == null) return;

                Cell oldCell = Cell;
                if (oldCell == null || !oldCell.IntersectsXY(value))
                {
                    Cell newCell = _region.GetCellAtPosition(value);
                    if (newCell == null) return;
                    else Cell = newCell;
                }
                _position = value;
            }
        }

        private Orientation _orientation;
        public Orientation Orientation
        {
            get => IsValid() ? _orientation : Orientation.Zero;
            set
            {
                if (Orientation.IsFinite(value)) _orientation = value;
            }
        }

        public static Vector3 ProjectToFloor(Cell cell, Vector3 regionPos)
        {
            if (cell == null || cell.RegionBounds.IntersectsXY(regionPos) == false) return regionPos;
            var cellProto = cell.CellProto;
            if (cellProto == null) return regionPos;

            short height;
            if (cellProto.HeightMap.HeightMapData.HasValue())
            {
                Vector3 cellPos = regionPos - cell.RegionBounds.Min;
                cellPos.X /= cellProto.BoundingBox.Width;
                cellPos.Y /= cellProto.BoundingBox.Length;
                int mapX = (int)cellProto.HeightMap.HeightMapSize.X;
                int mapY = (int)cellProto.HeightMap.HeightMapSize.Y;
                int x = Math.Clamp((int)(cellPos.X * mapX), 0, mapX - 1);
                int y = Math.Clamp((int)(cellPos.Y * mapY), 0, mapY - 1);
                height = cellProto.HeightMap.HeightMapData[y * mapX + x];
            }
            else
                height = short.MinValue;

            if (height > short.MinValue)
            {
                Vector3 resultPos = new(regionPos)
                {
                    Z = cell.RegionBounds.Center.Z + height
                };
                return resultPos;
            }
            else
            {
                var naviMesh = cell.GetRegion().NaviMesh;
                if (naviMesh.IsMeshValid)
                    return naviMesh.ProjectToMesh(regionPos);
                else
                    return regionPos;
            }
        }

        public static Vector3 ProjectToFloor(Region region, Vector3 regionPos)
        {
            if (region == null) return regionPos;
            Cell cell = region.GetCellAtPosition(regionPos);
            if (cell == null) return regionPos;
            return ProjectToFloor(cell, regionPos);
        }

        public static Vector3 ProjectToFloor(Region region, Cell cell, Vector3 regionPos)
        {
            if (cell != null && cell.IntersectsXY(regionPos))
                return ProjectToFloor(cell, regionPos);
            else
                return ProjectToFloor(region, regionPos);
        }

        public override string ToString()
        {
            return string.Format("rloc.pos={0}, rloc.rot={1}, rloc.region={2}, rloc.area={3}, rloc.cell={4}, rloc.entity={5}",
               _position,
               _orientation,
               _region != null ? _region.ToString() : "Unknown",
               Area != null ? Area : "Unknown",
               Cell != null ? Cell : "Unknown",
               "Unknown");
        }

        public void Initialize(WorldEntity worldEntity) { }

        public bool HasKeyword(KeywordPrototype keywordProto)
        {
            if (Region != null && Region.HasKeyword(keywordProto)) return true;
            Area area = Area;
            if (area != null && area.HasKeyword(keywordProto)) return true;
            return false;
        }
    }

    public class RegionLocationSafe
    {
        public PrototypeId AreaRef { get; private set; }
        public PrototypeId RegionRef { get; private set; }
        public PrototypeId CellRef { get; private set; }
        public ulong RegionId { get; private set; }
        public uint AreaId { get; private set; }
        public uint CellId { get; private set; }
        public Vector3 Position { get; private set; }
        public Orientation Orientation { get; private set; }

        public Area GetArea()
        {
            if (AreaId == 0) return null;
            Region region = GetRegion();
            Area area = region?.GetAreaById(AreaId);
            return area;
        }

        public Region GetRegion()
        {
            if (RegionId == 0) return null;
            Game game = Game.Current;
            RegionManager manager = game?.RegionManager;
            return manager?.GetRegion(RegionId);
        }

        public RegionLocationSafe Set(RegionLocation regionLocation)
        {
            Region region = regionLocation.Region;
            if (region != null)
            {
                RegionId = region.Id;
                RegionRef = region.PrototypeDataRef;
            }
            else
            {
                RegionId = 0;
                RegionRef = PrototypeId.Invalid;
            }

            Area area = regionLocation.Area;
            if (area != null)
            {
                AreaId = area.Id;
                AreaRef = area.PrototypeDataRef;
            }
            else
            {
                AreaId = 0;
                AreaRef = PrototypeId.Invalid;
            }

            Cell cell = regionLocation.Cell;
            if (cell != null)
            {
                CellId = cell.Id;
                CellRef = cell.PrototypeId;
            }
            else
            {
                CellId = 0;
                CellRef = PrototypeId.Invalid;
            }

            Position = new(regionLocation.Position);
            Orientation = new(regionLocation.Orientation);

            return this;
        }

        public bool HasKeyword(KeywordPrototype keywordProto)
        {
            Region region = GetRegion();
            if (region != null && region.HasKeyword(keywordProto)) return true;
            Area area = GetArea();
            if (area != null && area.HasKeyword(keywordProto)) return true;
            return false;
        }
    }
}