using System;
using System.Linq;
using Internal;
using Internal.Configuration;
using Newtonsoft.Json.Linq;

namespace DataTypes {
[Serializable]
public class JsonConfiguration {
    public IConfiguration[] EffectData;

    public JsonConfiguration(string fileName) {
        // get 
        var root = JObject.Parse(ResourceHelper.LoadVoroConfig(fileName));
        var propertiesArray = (JArray)root["config"];

        // the json contains the IConfig objects, json is converted to this array
        EffectData = new IConfiguration[propertiesArray.Count];

        for (var i = 0; i < propertiesArray.Count; i++) {
            // json stores the config objects in an array IConfig[]
            // these objects need to be extracted to be given to
            // the JsonConfig class
            var obj = propertiesArray[i] as JObject;

            // the property is each IConfig object
            // these will configure the instructions for how
            // to set the height for each point in the voro
            var prop = obj.Properties().First();

            // not an ideal way to do this
            //      "if it works, it works"

            // the json will hold any number of IConfig objects
            // each object needs to create one of the Cfg structs
            // to choose the right struct, look at the name each object uses

            EffectData[i] = prop.Name switch
            {
                "slope" => prop.Value.ToObject<SlopeCfg>(),
                "noise" => prop.Value.ToObject<NoiseCfg>(),
                "terrace" => prop.Value.ToObject<TerraceCfg>(),
                "null" => prop.Value.ToObject<NullCfg>(),
                "setGroup" => prop.Value.ToObject<SetGroupCfg>()
            };
        }
    }
}
}