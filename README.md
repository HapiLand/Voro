# Voro

Voro is my approach for a terrain generation tool. Which is intended as a way to create an environment for a hypothetical game I have imagined (alas, **game** development is not my forte).

[Voro](Assets/Scripts/Internal/Voro.cs) works by parsing data from two .json files to generate a procedural terrain object. [The first .json](Assets/Resources/Points/DemoTable.json), is used like a dictionary, with <id,vector2> being used to create the [Points](Assets/Scripts/DataTypes/Point.cs) and [Geometry](Assets/Scripts/DataTypes/Geometry.cs) for the terrain itself. [The second .json](Assets/Resources/Configs/MyConfig.json) contains a [configuration](Assets/Scripts/DataTypes/JsonConfig.cs) for the [terrains elevation](Assets/Scripts/Internal/VoroHeight.cs). The configuration is used to control the elevation that the terrain has, as determined by [a number of instructions](Assets/Scripts/Internal/Instructions).

The configurations are user-generated in an [config editor tool](Assets/ConfigEditor), designed so the user can decide how they want their games world to look. My aim is to simplify the typical steps for designing terrain: If a user wants the terrain to have rivers, imagine if all it took was a button called "Make Rivers". The time spent figuring out how to generate rivers (especially when they dont care **HOW** to get rivers, they only care to **HAVE** rivers), can be put to better use for real development.

I aim to extend it later on to make it more useful :)

## Notes

If Unity loads with an empty scene, either load [Scenes/DemoScene] or add to the scene, [Resources/Prefabs/VoroDemo].
1) In the scene, find the object "Voro Demo - Select Either Child" which contains the Voros for the demo
2) Set "HQ Version" as active or "Debug Version" as active

Voro is using Unity version 6.2, I haven't tested it with non-version6 but I dont think it will have any problems in version 2022.

At the moment the tool is non interactive, created only on Start. It is not game ready. :(

## Usage
See [VoroDemo](Assets/Scripts/UnityComponents/VoroDemo.cs) for the detailed steps needed to create a Voro, 

```c#
Start()

// 1) read the Config to use for the Voro
//    a Config produces the terrains elevation
var configName = "MyConfig"

// 2) create a Voro at some position + its configuration
//   (Voro = a chunk)
new Voro(configName, position)

// 3) create objects that make up the terrain
//    VERY W.I.P!
InstanceMesh(voro.Geometry[], voro.Points[])
```
This is a simple overview for how it works, the specifics of it all are certain to change

## Contributing

Pull requests are welcome! Hopefully my code isnt too hard to navigate :)

To aid in the long-term development of Voro, it would really help me out if you could spare a little something. One day I hope to turn Voro into more complete terrain generation tool, to easily create procedural environments. Which can be quite tough for some devs to create themselves.

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/hapiland)

## License

[MIT](https://choosealicense.com/licenses/mit/)
