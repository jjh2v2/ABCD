using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{

    public GameObject mGame;
    bool IsInputAllowed = true;

    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {

    }

    IEnumerator CoroutineInputDelay()
    {
        IsInputAllowed = false;
        yield return new WaitForSeconds(0.1f);
        IsInputAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        //int touchCount = Input.touchCount;
        //if (count == 0) return; //할 일이 없다.
        //for (int i = 0; i < count; i++)
        //{
        //    Touch touch = Input.GetTouch(i);
        //    Debug.Log("id:" + touch.fingerId + ",phase:" + touch.phase);
        //}

#if UNITY_ANDROID || UNITY_IPHONE

#else
        if (Input.GetMouseButtonDown(0))   //마우스 좌측 버튼을 누름.
        {
            //if (IsInputAllowed == false)
            //    return;
            //터치 시 내용 처리.. 
            //Touch touch = Input.GetTouch(0);
            //Debug.Log("BackGround GetMouseButtonDown:");
            mGame.GetComponent<Circle>().Touch(true);
            StartCoroutine("CoroutineInputDelay");
        }
        else
        {
            mGame.GetComponent<Circle>().Touch(false);
        }
#endif
#if UNITY_EDITOR

#endif
#if UNITY_ANDROID || UNITY_IPHONE
        
        if (!mGame.GetComponent<Circle>().GetGameStatus())
            return;

        int touchCount = Input.touchCount;
        if (touchCount > 0)
        {
            TouchPhase myTouch = Input.GetTouch(0).phase;
            if (Input.touchCount > 0 && myTouch == TouchPhase.Began)
            {
                mGame.GetComponent<Circle>().Touch(true);
            }
            else
            {
                mGame.GetComponent<Circle>().Touch(false);
            }
        }
        else
        {
            //윈도우용 로직
            if (Input.GetMouseButtonDown(0))   //마우스 좌측 버튼을 누름.
            {
                //if (IsInputAllowed == false)
                //    return;
                //터치 시 내용 처리.. 
                //Touch touch = Input.GetTouch(0);
                //Debug.Log("BackGround GetMouseButtonDown:");
                mGame.GetComponent<Circle>().Touch(true);
                StartCoroutine("CoroutineInputDelay");
            }
            else
            {
                mGame.GetComponent<Circle>().Touch(false);
            }
        }
#endif
    }

}