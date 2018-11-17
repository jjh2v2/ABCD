using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour {

    public bool m_bShowFPS = true;
    public UnityEngine.UI.Text LabelFPS;
    private int frameCount = 0;
    private float nextFrameUpdate = 0.0f;
    private float fps = 0.0f;
    private float frameUpdateRate = 4.0f; // 4 per second
    private int frameCounter;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        DisplayFPS();
    }

    private void DisplayFPS()
    {
        if (LabelFPS != null && m_bShowFPS)
        {
            frameCount++;
            if (Time.time > nextFrameUpdate)//0.25초마다 갱신
            {
                nextFrameUpdate += (1.0f / frameUpdateRate);
                fps = (int)Mathf.Floor((float)frameCount * frameUpdateRate);//0.25초간 누적된 프레임에 4를 곱해 FPS 출력
                LabelFPS.text = "FPS: " + fps;
                frameCount = 0;
            }
        }
    }
}
