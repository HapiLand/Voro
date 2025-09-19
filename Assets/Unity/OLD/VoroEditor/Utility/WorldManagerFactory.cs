namespace OLD.VoroEditor.Utility {
/// <summary>
///     utility to help creating a world manager for the game world
/// </summary>
public static class WorldManagerFactory {
    public static WorldManager GetWorldManager() {
        return WorldManager.Instance;
    }
}
}