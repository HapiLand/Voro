using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VoroSystem.Generation.ComputeSystem {
public class OBiome : MonoBehaviour {
    #region BiomeType enum

    public enum BiomeType {
        Desert,
        Chaparral,
        Jungle,
        Savannah,
        Plains,
        Tundra,
        Forest,
        Taiga,
        SnowyTaiga,
        Ocean
    }

    #endregion

    public static bool initialized;


    // [temperature, humidity]
    static int[][] BiomeTable;
    public static GameObject[][] TreePool;
    public static GameObject[][] FeaturePool;
    public static GameObject[][] WaterFeaturePool;


    public static float MaxTemp_Snow = 4f / 11f;

    public static void Init() {
        BiomeTable = new[]
        {
            new[]
            {
                (int)BiomeType.Tundra, (int)BiomeType.Tundra, (int)BiomeType.Tundra, (int)BiomeType.Tundra,
                (int)BiomeType.Tundra, (int)BiomeType.Tundra, (int)BiomeType.SnowyTaiga, (int)BiomeType.SnowyTaiga,
                (int)BiomeType.SnowyTaiga, (int)BiomeType.SnowyTaiga, (int)BiomeType.SnowyTaiga
            },
            new[]
            {
                (int)BiomeType.Tundra, (int)BiomeType.Tundra, (int)BiomeType.Tundra, (int)BiomeType.Tundra,
                (int)BiomeType.Tundra, (int)BiomeType.Tundra, (int)BiomeType.SnowyTaiga, (int)BiomeType.SnowyTaiga,
                (int)BiomeType.SnowyTaiga, (int)BiomeType.SnowyTaiga, (int)BiomeType.SnowyTaiga
            },
            new[]
            {
                (int)BiomeType.Tundra, (int)BiomeType.Tundra, (int)BiomeType.Tundra, (int)BiomeType.Tundra,
                (int)BiomeType.Tundra, (int)BiomeType.Tundra, (int)BiomeType.SnowyTaiga, (int)BiomeType.SnowyTaiga,
                (int)BiomeType.SnowyTaiga, (int)BiomeType.SnowyTaiga, (int)BiomeType.SnowyTaiga
            },
            new[]
            {
                (int)BiomeType.Plains, (int)BiomeType.Plains, (int)BiomeType.Plains, (int)BiomeType.Plains,
                (int)BiomeType.Plains, (int)BiomeType.Plains, (int)BiomeType.Taiga, (int)BiomeType.Taiga,
                (int)BiomeType.Taiga, (int)BiomeType.Taiga, (int)BiomeType.Taiga
            },
            new[]
            {
                (int)BiomeType.Plains, (int)BiomeType.Plains, (int)BiomeType.Plains, (int)BiomeType.Plains,
                (int)BiomeType.Plains, (int)BiomeType.Plains, (int)BiomeType.Taiga, (int)BiomeType.Taiga,
                (int)BiomeType.Taiga, (int)BiomeType.Taiga, (int)BiomeType.Taiga
            },
            new[]
            {
                (int)BiomeType.Plains, (int)BiomeType.Plains, (int)BiomeType.Plains, (int)BiomeType.Plains,
                (int)BiomeType.Plains, (int)BiomeType.Plains, (int)BiomeType.Forest, (int)BiomeType.Forest,
                (int)BiomeType.Forest, (int)BiomeType.Forest, (int)BiomeType.Forest
            },
            new[]
            {
                (int)BiomeType.Plains, (int)BiomeType.Plains, (int)BiomeType.Plains, (int)BiomeType.Plains,
                (int)BiomeType.Plains, (int)BiomeType.Plains, (int)BiomeType.Forest, (int)BiomeType.Forest,
                (int)BiomeType.Forest, (int)BiomeType.Forest, (int)BiomeType.Forest
            },
            new[]
            {
                (int)BiomeType.Desert, (int)BiomeType.Desert, (int)BiomeType.Desert, (int)BiomeType.Chaparral,
                (int)BiomeType.Chaparral, (int)BiomeType.Savannah, (int)BiomeType.Savannah, (int)BiomeType.Savannah,
                (int)BiomeType.Savannah, (int)BiomeType.Savannah, (int)BiomeType.Savannah
            },
            new[]
            {
                (int)BiomeType.Desert, (int)BiomeType.Desert, (int)BiomeType.Desert, (int)BiomeType.Chaparral,
                (int)BiomeType.Savannah, (int)BiomeType.Savannah, (int)BiomeType.Savannah, (int)BiomeType.Savannah,
                (int)BiomeType.Jungle, (int)BiomeType.Jungle, (int)BiomeType.Jungle
            },
            new[]
            {
                (int)BiomeType.Desert, (int)BiomeType.Desert, (int)BiomeType.Desert, (int)BiomeType.Desert,
                (int)BiomeType.Savannah, (int)BiomeType.Savannah, (int)BiomeType.Jungle, (int)BiomeType.Jungle,
                (int)BiomeType.Jungle, (int)BiomeType.Jungle, (int)BiomeType.Jungle
            },
            new[]
            {
                (int)BiomeType.Desert, (int)BiomeType.Desert, (int)BiomeType.Desert, (int)BiomeType.Desert,
                (int)BiomeType.Savannah, (int)BiomeType.Savannah, (int)BiomeType.Jungle, (int)BiomeType.Jungle,
                (int)BiomeType.Jungle, (int)BiomeType.Jungle, (int)BiomeType.Jungle
            }
        };


        string biomeName;
        string path;
        TreePool = new GameObject[10][];
        FeaturePool = new GameObject[10][];
        WaterFeaturePool = new GameObject[10][];
        for (var i = 0; i < TreePool.Length; i++) {
            biomeName = ((BiomeType)i).ToString();

            // trees
            path = "Terrain/" + biomeName + "/Trees";
            TreePool[i] = Resources.LoadAll<GameObject>(path);

            // features
            path = "Terrain/" + biomeName + "/Features";
            FeaturePool[i] = Resources.LoadAll<GameObject>(path);

            // water features
            path = "Terrain/" + biomeName + "/Features Water";
            WaterFeaturePool[i] = Resources.LoadAll<GameObject>(path);


            //Debug.Log(TreePool[i].Length);
        }

        //Debug.Log("initialized");
        initialized = true;
    }


    public static int GetBiome(float temp, float humid) {
        var temperature = (int)(temp * 10f + 0.5f);
        var humidity = (int)(humid * 10f + 0.5f);
        //Debug.Log(temp);
        var biome = BiomeTable[temperature][humidity];

        return biome;
    }

    public static Tuple<GameObject, Tuple<float, float, float, float, float>> GetTree(int biomeType, float wetness,
        float fw) {
        //Debug.Log(((BiomeType)biomeType).ToString());
        var trees = TreePool[biomeType];
        if (trees.Length > 0) {
            var tree = trees[Random.Range(0, trees.Length)];
            return Tuple.Create(tree, OTreeInfo.GetPlacementParameters(tree.name, wetness, fw));
        }

        return null;
    }


    public static Tuple<GameObject, Tuple<float, float, float, float, float>> GetFeature(int biomeType, float wetness,
        float fw, bool onWater) {
        GameObject[] features;
        if (onWater) {
            features = WaterFeaturePool[biomeType];
        }
        else {
            features = FeaturePool[biomeType];
        }

        if (features.Length > 0) {
            var feature = features[Random.Range(0, features.Length)];
            return Tuple.Create(feature, OTreeInfo.GetPlacementParameters(feature.name, wetness, fw));
        }

        return null;
    }
}
}