using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Light2D;

public class DayNightCycle : MonoBehaviour {
    public Sprite AmbientSprite;
    public Sprite SunSprite;
    public Material SunMaterial;
    public float SunScale = 5f;
    public Vector2 SunOffset = new Vector2(300f, 25f);

    LightSprite SunLight;
    SpriteRenderer AmbientLight;

    public static float Light = 1f;
    float LightCap = 0.8f;

    [SerializeField] float CycleDuration = 300f, CycleProgress = 100f;

    private void OnValidate() {
        if (SunScale < 0) SunScale = 0;
    }

    void FixedUpdate () {
        CycleProgress += 1f*Time.fixedDeltaTime;

        if (CycleProgress/CycleDuration < 0.25f) {
            Light = 0f;
        } else if (CycleProgress/CycleDuration < 0.5f) {
            Light = 4f*(CycleProgress/CycleDuration-0.25f);
            if (Light > 1f) Light = 1f;
        } else if (CycleProgress/CycleDuration < 0.75f) {
            Light = 1f;
        } else {
            Light = 4f*(1f-CycleProgress/CycleDuration);
        }

        if (SunLight) {
            Vector3 Position = SunLight.gameObject.transform.position;
            Vector3 LocalPosition = SunLight.gameObject.transform.localPosition;
            SunLight.gameObject.transform.position = new Vector3((CycleProgress/CycleDuration-0.5f)*SunOffset.x, Camera.main.transform.position.y+SunOffset.y, Position.z);
            
            Color Color = SunLight.Color;
            SunLight.Color = new Color(Color.r, Color.g, Color.b, Light);
        }

        if (CycleProgress >= CycleDuration) {
            CycleProgress = 0f;
        }
    }

    private void Update() {
        if (!SunLight) {
            GameObject Sun = new GameObject("Sun");
            if (Sun) {
                Sun.layer = LayerMask.NameToLayer("Light Sources");
                Sun.transform.parent = transform;

                SunLight = Sun.AddComponent<LightSprite>();
            }
        }

        if (SunLight) {
            SunLight.gameObject.transform.localScale = Vector3.one*SunScale;
            SunLight.Sprite = SunSprite;
            SunLight.Material = SunMaterial;
        }

        if (!AmbientLight) {
            AmbientLight = gameObject.AddComponent<SpriteRenderer>();
            gameObject.layer = LayerMask.NameToLayer("Ambient Light");
        }

        if (AmbientLight) {
            Color Color = AmbientLight.color;
            AmbientLight.color = new Color(Color.r, Color.g, Color.b, Light*LightCap);
            AmbientLight.sprite = AmbientSprite;

            Camera Camera = Camera.main;
            if (Camera) {
                transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, transform.position.z);

                //Calculate Camera Bounds
                float ScreenAspect = (float)Screen.width/(float)Screen.height;
                float CameraHeight = Camera.orthographicSize*2;
                Bounds Bounds = new Bounds(Vector3.zero, new Vector3(CameraHeight*ScreenAspect, CameraHeight, Camera.transform.localScale.z));
                
                transform.localScale = 2.25f*Bounds.extents;
            }
        }
    }
}
