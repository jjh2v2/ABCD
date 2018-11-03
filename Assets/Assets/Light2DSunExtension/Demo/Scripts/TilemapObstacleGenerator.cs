using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapObstacleGenerator : MonoBehaviour {
    public Color Color = Color.black;

    private void Update() {
        if (GetComponent<TilemapRenderer>()) {
            GameObject Obstacle = Instantiate(gameObject, transform);
            if (Obstacle) {
                Obstacle.layer = LayerMask.NameToLayer("Light Obstacles");
                Obstacle.transform.localPosition = Vector2.zero;
                Obstacle.transform.localRotation = Quaternion.identity;
                Obstacle.name = name + " Light Obstacle";

                Component[] Components = Obstacle.gameObject.GetComponents<Component>();
                foreach (var Component in Components) {
                    if (Component.GetType() != typeof(TilemapRenderer) && Component.GetType() != typeof(Tilemap) && Component.GetType() != typeof(Transform)) {
                        Destroy(Component);
                    }
                }

                Tilemap Tilemap = Obstacle.GetComponent<Tilemap>();
                if (Tilemap) {
                    Tilemap.color = Color;
                }
            }

            Destroy(this);
        }
    }
}
