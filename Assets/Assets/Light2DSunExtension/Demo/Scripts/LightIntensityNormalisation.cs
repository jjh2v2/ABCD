using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Light2D;

public class LightIntensityNormalisation : MonoBehaviour {
    public bool ReverseAmbient = true;
    public float Maximum = 0.7f;
    public float Minimum = 0.05f;

	// Update is called once per frame
	void Update () {
        LightSprite LightSprite = GetComponent<LightSprite>();
        if (LightSprite) {
            float Light = DayNightCycle.Light;

            Color Color = LightSprite.Color;

            if (Light < 0f) Light = 0f;

            //A/(B+C*x)
            //float Min = 1/(Const+Mod2);
            //float Max = Mod1/Const-Min;
            //float Alpha = (Mod1/(Const+Mod2*Light)-Min)/Max;
            //Alpha *= Maximum;

            float Alpha = 1f-Light+Light*Minimum;

            Alpha *= Maximum;

            if (!ReverseAmbient) Alpha = 1f-Alpha;
            
            LightSprite.Color = new Color(Color.r, Color.g, Color.b, Alpha);
        }
    }
}
