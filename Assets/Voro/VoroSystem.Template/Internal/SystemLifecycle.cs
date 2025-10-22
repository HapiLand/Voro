namespace Voro.VoroSystem.Template.Internal {
public abstract class SystemLifecycle {
    public void Run() {
        Initialize();
        // Creation();
        // Production();
        // Construction();
    }

    protected abstract void Initialize();
    protected abstract void Creation();
    protected abstract void Production();
    protected abstract void Construction();
}
}