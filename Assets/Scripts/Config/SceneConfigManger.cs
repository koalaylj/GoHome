using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;


public class SceneConfigManger : MonoBehaviour
{
    private List<Vector3> _cur_scene_bounds = new List<Vector3>();

    /// <summary>
    /// 当前场景地图边界
    /// </summary>
    public List<Vector3> MapBounds
    {
        get { return _cur_scene_bounds; }
    }

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="fileName">相对于 Application.streamingAssetsPath 的路径  (/Scene/Scene_1_1.json)</param>
    ///// <returns></returns>
    //public string LoadJson(string fileName) {
    //    string path = Application.streamingAssetsPath + fileName;
    //    if (!File.Exists(path)) {
    //        Debug.LogError("文件不存在：" + path);
    //    }

    //    //加载配置文件
    //    return File.ReadAllText(path);
    //}

    /// <summary>
    /// 加载场景 
    /// </summary>
    /// <param name="sceneName">例如： Scene_1_1 酱紫 </param>
    public GameObject LoadScene(string sceneName)
    {
        string json = IOTool.LoadJson("Scene/" + sceneName + ".json");
        SceneConf conf_scene = JsonMapper.ToObject<SceneConf>(json);
        if (conf_scene == null)
        {
            Debug.LogError("Load Json ERROR!");
            return null;
        }
        if (conf_scene.Bounds == null)
        {
            Debug.LogError("Load Json Bounds NULL!");
            LoadScene(conf_scene);
            return null;
        }
        // 应该在外部工具存储的时候转为世界坐标存储
        GameObject scene = LoadScene(conf_scene);
        Transform parent = scene.transform.FindChild("Map/Bounds");
        foreach (var item in conf_scene.Bounds)
        {
            // 相对坐标转世界坐标
            _cur_scene_bounds.Add(parent.localToWorldMatrix.MultiplyPoint(item.GetPosition()));
        }
        return scene;
    }

    /// <summary>
    /// 加载场景 
    /// </summary>
    /// <param name="sceneName">例如： Scene_1_1 酱紫 </param>
    public GameObject LoadScene(SceneConf conf_scene) {

        GameObject scene_go = GameObject.Find("Scene");

        if (scene_go == null) {
            Debug.LogError("导入场景数据出错，场景中无Scene节点！");
            return null;
        }

        //create light
        Object light_prefab = Resources.Load("UI/Prefabs/" + conf_scene.Light.PrefabName);
        GameObject light_go = GameObject.Instantiate(light_prefab) as GameObject;
        light_go.name = conf_scene.Light.Name;
        light_go.transform.localPosition = conf_scene.Light.GetPosition();
        light_go.transform.localRotation = conf_scene.Light.GetRotation();
        light_go.transform.localScale = conf_scene.Light.GetScale();
        light_go.transform.parent = scene_go.transform;

        //create camera
        Object ui_prefab = Resources.Load("UI/" + conf_scene.UI.PrefabName);
        GameObject ui_go = GameObject.Instantiate(ui_prefab) as GameObject;
        ui_go.name = conf_scene.UI.PrefabName;
        GameObject camera_go = ui_go.transform.FindChild("CameraScene").gameObject;
        camera_go.transform.localPosition = conf_scene.UI.GetPosition();
        camera_go.transform.localRotation = conf_scene.UI.GetRotation();
        camera_go.transform.localScale = conf_scene.UI.GetScale();
        ui_go.transform.parent = scene_go.transform;


        GameObject camera_ui = ui_go.transform.FindChild("Camera").gameObject;
        camera_ui.transform.localRotation = conf_scene.UI.GetRotation();

        /*
        //create scene
        scene_go = new GameObject("Scene");
        scene_go.name = conf_scene.Name;
        scene_go.transform.localPosition = conf_scene.GetPosition();
        scene_go.transform.localRotation = conf_scene.GetRotation();
        scene_go.transform.localScale = conf_scene.GetScale();
        */
        //create map
        GameObject map_go = new GameObject(conf_scene.Map.Name);
        map_go.transform.parent = scene_go.transform;
        map_go.transform.localPosition = conf_scene.Map.GetPosition();
        map_go.transform.localRotation = conf_scene.Map.GetRotation();
        map_go.transform.localScale = conf_scene.Map.GetScale();

        //create bounds
        GameObject bounds_root_go = new GameObject("Bounds");
        Object point_prefab = Resources.Load("UI/Prefabs/Point_10");
        bounds_root_go.transform.parent = map_go.transform;
        bounds_root_go.transform.localRotation = Quaternion.identity;
        bounds_root_go.transform.localScale = Vector3.one;
        bounds_root_go.transform.localPosition = Vector3.zero;


        if (conf_scene.Bounds != null)
        {
            foreach (var item in conf_scene.Bounds)
            {
                GameObject point_go = GameObject.Instantiate(point_prefab) as GameObject;
                point_go.name = item.Name;
                point_go.transform.parent = bounds_root_go.transform;
                point_go.transform.localPosition = item.GetPosition();
                point_go.transform.localRotation = item.GetRotation();
                point_go.transform.localScale = item.GetScale();
            }
        }

        //create ground
        GameObject ground_go = new GameObject(conf_scene.Map.Ground.Name);
        ground_go.transform.parent = map_go.transform;
        ground_go.transform.localPosition = conf_scene.Map.Ground.GetPosition();
        ground_go.transform.localRotation = conf_scene.Map.Ground.GetRotation();
        ground_go.transform.localScale = conf_scene.Map.Ground.GetScale();

        if (conf_scene.PlaceHolder != null)
        {
            foreach (var info in conf_scene.PlaceHolder)
            {
                //Debug.Log("位置点信息： " + info.GetPosition());
                //todo: FightScene.s_InstanceThis.PushPos(info.GetPosition());
            }
        }
        else
        {
            Debug.Log("位置点信息为空");
        }
        
        Mesh ground_mesh = PlaneInfo.CreateMesh((float)conf_scene.Map.Ground.CellWidth * 0.5f, (float)conf_scene.Map.Ground.CellHeight * 0.5f);
        foreach (var cell in conf_scene.Map.Ground.GroundCells)
        {
            GameObject cell_go;
            Object res = Resources.Load("Materials/Map/" + cell.Materials[0]);
            if(res != null)
            {
                Material matX = res as Material;
                cell_go = PlaneInfo.CreatePlaneWithMesh(cell.GetPosition(), ground_mesh, cell.Name, ground_go.transform, matX);
            }
            else
            {
                Debug.LogError("NotFound " + cell.Materials[0]);
                cell_go = PlaneInfo.CreatePlaneWithMesh(cell.GetPosition(), ground_mesh, cell.Name, ground_go.transform, null);
                if (cell.Materials.Count > 0)
                {
                   //todo: FightScene.s_InstanceThis.m_MBM.AddMaterial(cell_go, cell.Materials[0]);
                }
            }
            cell_go.transform.localRotation = cell.GetRotation();
            cell_go.transform.localScale = cell.GetScale();
        }

        //create surface
        GameObject surface_go = new GameObject(conf_scene.Map.Surface.Name);
        surface_go.transform.parent = map_go.transform;
        surface_go.transform.localPosition = conf_scene.Map.Surface.GetPosition();
        surface_go.transform.localRotation = conf_scene.Map.Surface.GetRotation();
        surface_go.transform.localScale = conf_scene.Map.Surface.GetScale();

        if (conf_scene.Map.Surface.Planes != null) {
            foreach (var item in conf_scene.Map.Surface.Planes)
            {
                GameObject item_go;
                Object res = Resources.Load("Materials/Map/" + item.Materials[0]);
                if(res != null)
                {
                    Material mat = res as Material;
                    item_go = PlaneInfo.CreatePlane((float)item.CellWidth * 0.5f, (float)item.CellHeight * 0.5f, item.Name, surface_go.transform, mat);
                }
                else
                {
                    Debug.LogError("NotFound " + item.Materials[0]);
                    item_go = PlaneInfo.CreatePlane((float)item.CellWidth * 0.5f, (float)item.CellHeight * 0.5f, item.Name, surface_go.transform, null);
                    if (item.Materials.Count > 0)
                    {
                        //todo: FightScene.s_InstanceThis.m_MBM.AddMaterial(item_go, item.Materials[0]);
                    }
                }
                item_go.transform.localPosition = item.GetPosition();
                item_go.transform.localRotation = item.GetRotation();
                item_go.transform.localScale = item.GetScale();
            }
        }

        if (conf_scene.Map.Surface.Colliders != null) {
            foreach (var item in conf_scene.Map.Surface.Colliders) {
                GameObject go = new GameObject(item.Name);
                go.transform.parent = surface_go.transform;
                go.transform.localPosition = item.GetPosition();
                go.transform.localRotation = item.GetRotation();
                go.transform.localScale = item.GetScale();
                BoxCollider comp = go.AddComponent<BoxCollider>();
                comp.center = item.ListToVector3(item.Center);
                comp.size = item.ListToVector3(item.Size);
            }
        }

        if (conf_scene.Map.Surface.PlaneWithColliders != null) {
            foreach (var item in conf_scene.Map.Surface.PlaneWithColliders)
            {
                GameObject item_go;
                Object res = Resources.Load("Materials/Map/" + item.Plane.Materials[0]);
                if (res != null)
                {
                    Material mat = res as Material;
                    item_go = PlaneInfo.CreatePlane((float)item.Plane.CellWidth * 0.5f, (float)item.Plane.CellHeight * 0.5f, item.Name, surface_go.transform, mat);
                }
                else
                {
                    Debug.LogError("NotFound " + item.Plane.Materials[0]);
                    item_go = PlaneInfo.CreatePlane((float)item.Plane.CellWidth * 0.5f, (float)item.Plane.CellHeight * 0.5f, item.Name, surface_go.transform, null);
                    if (item.Plane.Materials.Count > 0)
                    {
                       //todo: FightScene.s_InstanceThis.m_MBM.AddMaterial(item_go, item.Plane.Materials[0]);
                    }
                }
                item_go.transform.localPosition = item.GetPosition();
                item_go.transform.localRotation = item.GetRotation();
                item_go.transform.localScale = item.GetScale();

                foreach (var item_collider in item.Colliders) {
                    GameObject go = new GameObject(item_collider.Name);
                    go.transform.parent = item_go.transform;
                    go.transform.localPosition = item_collider.GetPosition();
                    go.transform.localRotation = item_collider.GetRotation();
                    go.transform.localScale = item_collider.GetScale();
                    BoxCollider comp = go.AddComponent<BoxCollider>();
                    comp.center = item_collider.ListToVector3(item_collider.Center);
                    comp.size = item_collider.ListToVector3(item_collider.Size);
                }
            }
        }
        // Create Over
        return scene_go;
    }
}