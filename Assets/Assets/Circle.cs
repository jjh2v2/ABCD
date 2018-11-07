using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Circle : MonoBehaviour {

    public GameObject hitpos;
    public GameObject patical;

    public GameObject mCanvas;

    public GameObject HitObj;
    public GameObject HitBg;
    public GameObject CountText;
    public float speed = 1.0f;
    public float addspeed = 0.1f;

    public Color ChaingeColor;
    private Color OriginClor;

    private bool mTouch = false;
    private ulong mHitCount = 0;

    private Vector3 OrigineRot;

    void Awake()
    {
        if(patical)
            patical.GetComponent<ParticleSystem>().Stop();
    }

    // Use this for initialization
    void Start ()
    {
        //HitObj = this.GetComponentInChildren(GameObject)
        Image HitBgImg = HitBg.GetComponent<Image>();
        OriginClor = HitBgImg.color;
        CountText.GetComponent<Text>().text = "0";

        patical.GetComponent<ParticleSystem>().Stop();
        //patical.GetComponent<ParticleSystem>().playbackSpeed = 1.30f;

        Rotation(0.0f, 120.0f);

        //GameObject _game = new GameObject();
        //_game.AddComponent<RectTransform>();
        //_game.GetComponent<RectTransform>().sizeDelta = new Vector2(1.0f, 1.0f);
        //_game.name = "fx_0";
        //_game.transform.SetParent(mCanvas.transform);
        //_game.transform.localScale = new Vector3(100.0f, 100.0f, 100.0f);
        //_game.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        //Mesh _mesh = MakeFxMash.MakeMesh();
        //_game.AddComponent<MeshFilter>();
        //_game.GetComponent<MeshFilter>().mesh = _mesh;
        //_game.AddComponent<MeshRenderer>();
        //_game.GetComponent<MeshRenderer>().material.color = Color.green;

    }
	
	// Update is called once per frame
	void Update () {
        if (HitBg && HitObj)
        {
            //z += Time.deltaTime * 50;
            //HitObj.transform.rotation = Quaternion.Euler(0, 0, z);
            HitObj.transform.localRotation *= Quaternion.AngleAxis(speed, new Vector3(0.0f, 0.0f, 1.0f));

            Image HitBgImg = HitBg.GetComponent<Image>();
            Image HitObjImg = HitObj.GetComponent<Image>();

            //float dddd = HitObj.transform.eulerAngles.z;
            //Debug.Log("HitObj.transform.eulerAngles.z : " + dddd);

            float oZ = (OrigineRot.z);
            float fZ = (HitObj.transform.localEulerAngles.z);
            //Debug.Log("HitObj.transform.localRotation.z : " + fZ);
            if (Mathf.Abs(fZ - oZ) < 0.1f)
            {
                GameOver();
            }

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
        speed *= -1.0f;
        mHitCount++;
        CountText.GetComponent<Text>().text = "" + mHitCount;
        mCanvas.GetComponent<ShakeCamera>().shake = 0.09f;
        OrigineRot = HitObj.transform.localEulerAngles;
        HitBg.transform.localRotation *= Quaternion.AngleAxis(180.0f, new Vector3(0.0f, 0.0f, 1.0f));
        //MakeFxMash.MakeFxObject(HitBg);

        //Vector3 pos = 부모의 rotation값(쿼터니언) * 자식의 초기 위치값
        Vector3 pos = HitObj.transform.localRotation * hitpos.transform.localPosition;
        pos.z = patical.transform.localPosition.z;
        patical.transform.position = pos;
        patical.transform.localPosition = pos;
        patical.GetComponent<ParticleSystem>().Stop();
        patical.GetComponent<ParticleSystem>().Play();

        // 횟수가 중요한거니까 횟수가 올라갈수록 속도를 빠르게
        if (mHitCount%2 == 0)
        {
            if(speed > 0)
                speed += addspeed;
        }
        if (mHitCount % 2 == 0)
        {
            Image HitBgImg = HitBg.GetComponent<Image>();
            if (HitBgImg.fillAmount > 0.012f)
            {
                HitBgImg.fillAmount -= 0.01f;
            }
        }
    }

    // 0 ~ 360 -값은 들어가면 중돌 계산이 안된다
    private void Rotation(float PlayerR, float BackGroundR)
    {
        if (HitBg && HitObj)
        {
            HitObj.transform.localRotation *= Quaternion.AngleAxis(PlayerR, new Vector3(0.0f, 0.0f, 1.0f));
            HitBg.transform.localRotation *= Quaternion.AngleAxis(BackGroundR, new Vector3(0.0f, 0.0f, 1.0f));

            OrigineRot = HitObj.transform.localEulerAngles;
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
        HitObj.transform.localRotation *= Quaternion.AngleAxis(speed, new Vector3(0.0f, 0.0f, 1.0f));
        speed = 0.0f;
    }
}
