using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour {

    public GameObject mGame;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int touchCount = Input.touchCount;
        //if (count == 0) return; //할 일이 없다.
        //for (int i = 0; i < count; i++)
        //{
        //    Touch touch = Input.GetTouch(i);
        //    Debug.Log("id:" + touch.fingerId + ",phase:" + touch.phase);
        //}

        if (Input.GetMouseButtonDown(0) || touchCount > 0)   //마우스 좌측 버튼을 누름.
        {
            //터치 시 내용 처리.. 
            //Touch touch = Input.GetTouch(0);
            //Debug.Log("BackGround GetMouseButtonDown:");
            mGame.GetComponent<Circle>().Touch(true);
        }
        else
        {
            mGame.GetComponent<Circle>().Touch(false);
        }
    }

}
