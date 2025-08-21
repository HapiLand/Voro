using System;
using UnityEngine;

namespace DataTypes {
[Serializable]
// array of points from the json file
public class JsonPointArray {
    [field: SerializeField] public JsonPoint[] points { get; set; }
}
}