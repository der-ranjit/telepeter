using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FloatingSpotLight : MonoBehaviour
{

    private Light2D spotLight;
    // Start is called before the first frame update
    void Start()
    {
        Light2D light = GetComponentInChildren<Light2D>();
        if (light.lightType == Light2D.LightType.Point) {
            spotLight = light;
        } else {
            Debug.Log("[FloatingSpotLight] does not have a Light2D with type spotlight as child");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spotLight != null) {
        }
    }
}
