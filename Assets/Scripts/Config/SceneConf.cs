using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoundsConfig : ConfigBase {
}

public class PlaceHolderConfig : ConfigBase {
}

public class SurfaceCellPlane : ConfigBase {
    public List<string> Materials { get; set; }
    public double CellWidth { get; set; }
    public double CellHeight { get; set; }
}

public class SurfaceCellPlaneWithCollider : ConfigBase {
    public SurfaceCellPlane Plane { get; set; }
    public List<SurfaceCellCollider> Colliders { get; set; }
}

public class SurfaceCellCollider : ConfigBase {
    public List<double> Center { get; set; }
    public List<double> Size { get; set; }
    public bool IsTrigger { get; set; }
}

public class SurfaceConf : ConfigBase {
    public List<SurfaceCellCollider> Colliders { get; set; }
    public List<SurfaceCellPlaneWithCollider> PlaneWithColliders { get; set; }
    public List<SurfaceCellPlane> Planes { get; set; }
}


public class GroundCell : ConfigBase {
    public List<string> Materials { get; set; }
}

public class GroundConf : ConfigBase {
    public double CellWidth { get; set; }
    public double CellHeight { get; set; }
    public List<GroundCell> GroundCells { get; set; }
}

public class MapConf : ConfigBase {
    public GroundConf Ground { get; set; }
    public SurfaceConf Surface { get; set; }
}

public class PrefabConf : ConfigBase {
    public string PrefabName { get; set; }
}

public class SceneConf : ConfigBase {
    public MapConf Map { get; set; }
    public PrefabConf UI { get; set; }
    public PrefabConf Light { get; set; }
    public List<BoundsConfig> Bounds { get; set; }
    public List<PlaceHolderConfig> PlaceHolder { get; set; }
}