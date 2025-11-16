using UnityEngine;

namespace VoroSystem.Generation.ComputeSystem {
public class OLightingController : MonoBehaviour {
    static readonly float fog_base = .2f;
    static readonly float changeSpeed_ambientColor = 5f;
    static readonly float changeSpeed_fog = 5f;

    static readonly float period = .1f;
    static float updateTime;

    #region Serialized Fields

    public Camera MainCamera;
    public Material skyMat;
    public Light mainLight;

    public Gradient tempGradient;
    public Gradient wetnessGradient;

    #endregion

    Color ambientColor, fogColor;
    float elevation;
    float fog;
    float height;
    float temperature;
    float wetness;

    #region Event Functions

    void Start() {
        Init();
    }


    // Update is called once per frame
    void FixedUpdate() {
        if (OBiome.initialized) {
            if (Time.fixedTime >= updateTime) {
                UpdateLighting();
                updateTime = Time.fixedTime + period;
            }
        }
    }

    #endregion

    void Init() {
        updateTime = Time.fixedTime + period;
    }

    void UpdateLighting() {
        AreaConditions.GetAreaConditions(MainCamera.transform.position);
        height = AreaConditions.Height;
        temperature = AreaConditions.Temperature;
        wetness = AreaConditions.Humidity;
        elevation = AreaConditions.Elevation;
        //SetLightingColors(temperature, wetness);
        SetFogDensity(temperature, wetness);
    }


    Color CalculateAmbientColor(float temp, float wetness) {
        var c = Color.Lerp(tempGradient.Evaluate(temp), wetnessGradient.Evaluate(wetness), .2f);
        return c;
    }

    Color CalculateFogColor(float temp, float wetness) {
        var c = Color.Lerp(tempGradient.Evaluate(temp), wetnessGradient.Evaluate(wetness), 1f);
        return c;
    }

    void SetLightingColors(float temp, float wetness) {
        //Color a = CalculateAmbientColor(temp, wetness);
        //ambientColor = Color.Lerp(ambientColor, a, changeSpeed_ambientColor * Time.deltaTime);
        //RenderSettings.ambientLight = ambientColor;

        var f = CalculateFogColor(temp, wetness);
        fogColor = Color.Lerp(fogColor, f, changeSpeed_ambientColor * Time.deltaTime);
        RenderSettings.fogColor = fogColor;
    }

    void SetFogDensity(float temp, float wetness) {
        wetness = Mathf.Clamp01(wetness - .6f);
        temp = Mathf.Clamp01(temp - .6f);
        var f = (wetness + temp) / 2f * fog_base;
        fog = Mathf.Lerp(fog, f, changeSpeed_fog * Time.deltaTime);
        RenderSettings.fogDensity = fog;
        //skyMat.SetFloat("_FogHeight", Mathf.Clamp01(fog/fog_base - .05f));
    }
}
}