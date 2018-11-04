using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circle : MonoBehaviour {

    public GameObject mCamera;

    public GameObject HitObj;
    public GameObject HitBg;
    public GameObject CountText;
    public float tttt = 1.0f;

    public Color ChaingeColor;
    private Color OriginClor;

    private bool mTouch = false;
    private ulong mHitCount = 0; 

    void Awake()
    {

    }

    // Use this for initialization
    void Start ()
    {
        //HitObj = this.GetComponentInChildren(GameObject)
        Image HitBgImg = HitBg.GetComponent<Image>();
        OriginClor = HitBgImg.color;
        CountText.GetComponent<Text>().text = "0";

        Rotation(0.0f, 120.0f);
    }
	
	// Update is called once per frame
	void Update () {
        if (HitBg && HitObj)
        {
            //z += Time.deltaTime * 50;
            //HitObj.transform.rotation = Quaternion.Euler(0, 0, z);
            HitObj.transform.localRotation *= Quaternion.AngleAxis(tttt, new Vector3(0.0f, 0.0f, 1.0f));

            Image HitBgImg = HitBg.GetComponent<Image>();
            Image HitObjImg = HitObj.GetComponent<Image>();

            float dddd = HitObj.transform.eulerAngles.z;
            //Debug.Log("HitObj.transform.eulerAngles.z : " + dddd);

            float HitBgImgOverPos = HitBg.transform.eulerAngles.z - (HitBgImg.fillAmount * 360);
            float HitObjImgOverPos = HitObj.transform.eulerAngles.z - (HitObjImg.fillAmount * 360);
            if (HitBg.transform.eulerAngles.z > HitObj.transform.eulerAngles.z)
            {
                if (HitBgImgOverPos < HitObjImgOverPos)
                {
                    // 충돌
                    HitBgImg.color = ChaingeColor;
                    if (mTouch)
                    {
                        Opposition();
                    }
                }
                else
                {
                    HitBgImg.color = OriginClor;
                    if (mTouch)
                    {
                        GameOver();
                    }
                }
            }
            else if (HitBg.transform.eulerAngles.z < HitObjImgOverPos)
            {
                HitBgImg.color = OriginClor;
                if (mTouch)
                {
                    GameOver();
                }
            }
        }
    }

    private void Opposition()
    {
        //HitBg
        tttt *= -1.0f;
        HitBg.transform.localRotation *= Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 0.0f, 1.0f));
        mHitCount++;
        CountText.GetComponent<Text>().text = "" + mHitCount;
        mCamera.GetComponent<ShakeCamera>().shake = 0.09f;
    }

    // 0 ~ 360 -값은 들어가면 중돌 계산이 안된다
    private void Rotation(float PlayerR, float BackGroundR)
    {
        if (HitBg && HitObj)
        {
            HitObj.transform.localRotation *= Quaternion.AngleAxis(PlayerR, new Vector3(0.0f, 0.0f, 1.0f));
            HitBg.transform.localRotation *= Quaternion.AngleAxis(BackGroundR, new Vector3(0.0f, 0.0f, 1.0f));
        }
    }

    public void Touch(bool _is)
    {
        mTouch = _is;
    }

    void GameOver()
    {
        // 게임끝
        Debug.Log("GameOver");
    }
}
