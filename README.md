
# Voro
Voro is a GUI tool for real-time 3D procedural terrain generation in Unity.

Planned features -
- GUI editor for designing preset patterns that generate different types of worlds. (in development)
- Expand feature set of terrain generation, allowing generation to aid in implementing game mechanics. (in development)
- Infinite world generation. (in development)
- Improve ease-of-use and overall quality of GUI. (in development)
- Preset Layers.
- Object scattering, eg trees and rocks.
- Implement Mesh generation in the form of a grass layer.
- Increased quality of Cell Mesh library.
- Address performance, aim to optimise realtime performance.
- Procedural Textures.
- Streams / Water flow.
( not a comprehensive list, most likely to be developped, mostly ordered by priority )

---

[`Voro`](Assets/Scripts/Internal/Voro.cs) procedurally generates terrain by parsing data from two JSON files.

1. **Point and Geometry Data**
   [`DemoTable.json`](Assets/Resources/Points/DemoTable.json) acts as a dictionary `<ID, Vector2>`. This data is used to construct [`Cells`](Assets/Scripts/DataTypes/Cell.cs), this produces the geometry of the world terrain.

2. **Elevation**
   [`MyConfig.json`](Assets/Resources/Configs/MyConfig.json) defines the height generation via [`JsonConfig`](Assets/Scripts/DataTypes/JsonConfig.cs). This preset is interpreted by [`VoroHeight`](Assets/Scripts/Internal/VoroHeight.cs) to compute the vertical displacement of the terrain. Elevation is shaped according to a set of rules that were set from the [Configuration Editor](Assets/ConfigEditor).

My aim is to make the process of designing terrain have a greater ease of use, simplyfying the steps taken to achieve good results. Imagine if one wanted rivers in their world and all it took was pressing a "Make Rivers" button. While Voro isnt that simple, I hope that it could do the general principle of that.

I aim to extend it later on to make it more useful :)

## Notes

If Unity loads with an empty scene, either load [Scenes/DemoScene] or add to the scene, [Resources/Prefabs/VoroDemo].
1) In the scene, find the object "Voro Demo - Select Either Child" which contains the Voros for the demo
2) Set "HQ Version" as active or "Debug Version" as active

Voro is using Unity version 6.2, I haven't tested it with non-version6 but I dont think it will have any problems in version 2022.

At the moment the tool is non interactive, created only on Start. It is not game ready. :(

## Usage
Make sure to open the [ConfigEditor Scene](Assets/ConfigEditor/EditorScene.unity) first, begin the scene and press the 'Export' button.

You can also add three effects to create different styles of terrain. Make sure to press Export to view any changes. A real-time editor is planned.

Open [the Demo Scene](Assets/Scenes/DemoScene.unity) to see your configuration used to produce a 3x3 grid of Voros. 

A Voro is created with two easy steps
```c#
// 1) choose a configuration file that will create the terrains form
//    ( "MyConfig" is the only valid name )
var configName = "MyConfig"

// 2) create a new Voro using the config name
//    and the position the voro is created at
//  ( Voro = a chunk )
new Voro(configName, position)
```

## Contributing

Pull requests are welcome! Hopefully my code isnt too hard to navigate :)

To aid in the long-term development of Voro, it would really help me out if you could spare a little something. One day I hope to turn Voro into more complete terrain generation tool, to easily create procedural environments. Which can be quite tough for some devs to create themselves.

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/hapiland)

## License

[MIT](https://choosealicense.com/licenses/mit/)
