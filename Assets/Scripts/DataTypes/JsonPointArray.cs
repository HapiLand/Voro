using System;
using UnityEngine;

namespace DataTypes {
[Serializable]
// array of points from the json file
public class JsonPointArray {
    public JsonPoint[] points;
    public JsonConfig[] config;
}
}