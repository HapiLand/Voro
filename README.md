# Voro

Voro is my approach for a terrain generation tool. Which is intended as a way to create an environment for a hypothetical game I have imagined (alas, **game** development is not my forte), 

I aim to extend it later on to make it more useful :)

## Notes

If Unity loads with an empty scene, either load [Scenes/DemoScene] or add to the scene, [Resources/Prefabs/VoroDemo]

Voro is using Unity version 6.2, I haven't tested it with non-version6 but I dont think it will have any problems in version 2022

At the moment the tool is non interactive, created only on Start :(

## Usage
See VoroDemo.cs for the detailed steps needed to create a Voro, 

```c#
// 1) read point[] and config[]
//    Table.json
var data = LoadResource("Points/Table")

// 2) parse data to generate a default Voro
//   (a chunk)
BuildVoro(data, out voro)

// 3) apply the config, setting the point height
//    VoroHeight.cs
//    this step is optional, giving you flat terrain
voro.ConfigurePointHeight(pos)

// 4) create objects that make up the terrain
//    W.I.P!
InstanceMesh(voro.Geometry[], voro.Points[])
```

## Contributing

Pull requests are welcome! Hopefully my code isnt too hard to navigate :)

To aid in the long-term development of Voro, it would really help me out if you could spare a little something. One day I hope to turn Voro into more complete terrain generation tool, to easily create procedural environments. Which can be quite tough for some devs to create themselves.

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/hapiland)

## License

[MIT](https://choosealicense.com/licenses/mit/)
