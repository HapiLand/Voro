using UnityEngine;

namespace Voro.World.Internal {
public class ChunkConfiguration {
    Configuration _config;

    public ChunkConfiguration(TextAsset asset) {
        ToConfiguration();
    }

    void ToConfiguration() {
        _config = new Configuration();
    }
}
}