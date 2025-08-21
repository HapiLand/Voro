using System;
using UnityEngine;

namespace DataTypes {
[Serializable]
// ToDo rename this class, this name no longer fits its purpose
// array of points from the json file
public class JsonPointArray {
    public JsonPoint[] points;
    public JsonConfig[] config;
}
}