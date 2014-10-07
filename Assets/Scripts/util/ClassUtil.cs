using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 创建类相关的工具函数
/// 作者：于小懒
/// </summary>
public class ClassUtil {
    public static object LoadClass(string cn) {
        System.Type type = System.Reflection.Assembly.GetExecutingAssembly().GetType(cn);
        return LoadClass(type);
    }

    public static object LoadClass(System.Type type) {
        return System.Activator.CreateInstance(type);
    }

    public static Type GetType(string type) {
        return System.Reflection.Assembly.GetExecutingAssembly().GetType(type);
    }
}