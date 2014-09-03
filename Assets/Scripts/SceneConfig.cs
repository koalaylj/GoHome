using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Config
{


}

public class SceneConfig
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string ScenePrefabName { get; set; }

    public List<HurtConfig> Hurts { get; set; }
}

public class HurtConfig
{
    public int Id { get; set; }

    public float PosX { get; set; }

    public float PosY { get; set; }

    public float RotatX { get; set; }

    public float RotatY { get; set; }
}
