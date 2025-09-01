using System;
using System.Linq;
using Internal;
using Internal.Configuration;
using Newtonsoft.Json.Linq;

namespace DataTypes {
[Serializable]
public class JsonConfig {
    public IConfig[] EffectData;

    public JsonConfig(string fileName) {
        // get 
        var root = JObject.Parse(ResourceHelper.LoadVoroConfig(fileName));
        var configArr = (JArray)root["config"];

        // the json contains the IConfig objects, json is converted to this array
        EffectData = new IConfig[configArr.Count];

        for (var i = 0; i < configArr.Count; i++) {
            // json stores the config objects in an array IConfig[]
            // these objects need to be extracted to be given to
            // the JsonConfig class
            var obj = configArr[i] as JObject;

            // the property is each IConfig object
            // these will configure the instructions for how
            // to set the height for each point in the voro
            var prop = obj.Properties().First();

            // not an ideal way to do this
            //      "if it works, it works"

            // the json will hold any number of IConfig objects
            // each object needs to create one of the Cfg structs
            // to choose the right struct, look at the name each object uses
            // ToDo replace this switch with the better design
            switch (prop.Name) {
            case "slope":
                EffectData[i] = prop.Value.ToObject<SlopeCfg>();
                break;
            case "noise":
                EffectData[i] = prop.Value.ToObject<NoiseCfg>();
                break;
            case "terrace":
                EffectData[i] = prop.Value.ToObject<TerraceCfg>();
                break;
            case "null":
                EffectData[i] = prop.Value.ToObject<NullCfg>();
                break;
            case "setGroup":
                EffectData[i] = prop.Value.ToObject<SetGroupCfg>();
                break;
            }
            // doing it this way is probably not a bad approach
            // performance isnt a huge concern now as there arent many types of IConfig
            // hypothetically if this library had 100 different IConfig structs
            // then it might suck to maintain a gigantic wall of switch cases
        }
    }
}
}