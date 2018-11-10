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
    public GameObject NewBestScoreText;
    public float speed = 2.0f;

    public Color ChaingeColor;
    private Color OriginClor;

    private bool mTouch = false;
    private ulong mHitCount = 0;
    private ulong mStageCurrentCount = 0;
    private int mBestScore = 0;
    private Vector3 PreRot;

    void Awake()
    {
        if(patical)
            patical.GetComponent<ParticleSystem>().Stop();
    }

    // Use this for initialization
    void Start ()
    {
        ////HitObj = this.GetComponentInChildren(GameObject)
        //Image HitBgImg = HitBg.GetComponent<Image>();
        //OriginClor = HitBgImg.color;
        //CountText.GetComponent<Text>().text = "0";

        //patical.GetComponent<ParticleSystem>().Stop();
        //patical.GetComponent<ParticleSystem>().playbackSpeed = 1.30f;

        Reset();

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
            ////z += Time.deltaTime * 50;
            ////HitObj.transform.rotation = Quaternion.Euler(0, 0, z);
            HitObj.transform.localRotation *= Quaternion.AngleAxis(speed, new Vector3(0.0f, 0.0f, 1.0f));

            float oZ = (PreRot.z);
            float fZ = (HitObj.transform.localEulerAngles.z);
            if (Mathf.Abs(fZ - oZ) < 0.1f)
            {
                GameOver();
            }

            Image HitBgImg = HitBg.GetComponent<Image>();
            Image HitObjImg = HitObj.GetComponent<Image>();

            float HitBgImgPosOut = HitBg.transform.eulerAngles.z - (HitBgImg.fillAmount * 360);
            float HitBgImgPosIn = HitBg.transform.eulerAngles.z;
            float HitObjImgPosOut = HitObj.transform.eulerAngles.z;// + (HitObjImg.fillAmount * 360);
            float HitObjImgPosIn = HitObj.transform.eulerAngles.z - (HitObjImg.fillAmount * 360);


            bool isHit = false;
            if (HitBgImgPosOut < 0.0f)
            {
                //// 0도에 걸쳐있을때
                float HitBgImgPosOutTemp = 360.0f + HitBgImgPosOut;
                if (360.0f >= HitObjImgPosIn && HitBgImgPosOutTemp <= HitObjImgPosOut)
                {
                    // 충돌
                    isHit = true;
                }
            }
            if (HitBgImgPosIn >= HitObjImgPosIn && HitBgImgPosOut <= HitObjImgPosOut)
            {
                // 충돌
                isHit = true;
            }
            if (isHit)
            {
                HitBgImg.color = ChaingeColor;
                if (true/*mTouch*/)
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
    }

    private void Opposition()
    {
        //HitBg
        // 충돌 카운트 처리
        mHitCount++;
        CountText.GetComponent<Text>().text = "" + mHitCount;

        // 충돌 배경 위치 선정
        float ran = Random.Range(90.0f, 270.0f);
        HitBg.transform.localRotation *= Quaternion.AngleAxis(ran, new Vector3(0.0f, 0.0f, 1.0f));

        // 충돌하고 바운드 설정을 위해 데이터 가져와서 가공하기
        uint _hitCurrent = 0;
        for (uint i = 0; i < mStageCurrentCount + 1; i++)
        {
            _hitCurrent += GameDataBase.StageHitCount[i];
        }
        if (mHitCount == _hitCurrent && mStageCurrentCount < GameDataBase.StagetotalCount)
        {
            mStageCurrentCount++;
        }
       if (mStageCurrentCount >= GameDataBase.StagetotalCount)
       {
            mStageCurrentCount = 19;
       }

        // 충돌 바운드 설정
        Image HitBgImg = HitBg.GetComponent<Image>();
        float RangeMin = GameDataBase.RangeMinMaxData[mStageCurrentCount, 0];
        float RangeMax = GameDataBase.RangeMinMaxData[mStageCurrentCount, 1];
        float yyyy = Random.Range(RangeMin, RangeMax);
        HitBgImg.fillAmount = yyyy;
        //Debug.Log("yyyy : " + yyyy);

        // 스피드 설정
        int iii = Random.Range(0, 1);
        float _Speed = GameDataBase.SpeedMinMaxData[mStageCurrentCount, iii];
        if (speed > 0.0f)
        {
            speed += _Speed;
        }
        else
        {
            speed -= _Speed;
        }
        if (mStageCurrentCount == GameDataBase.StagetotalCount - 1)
        {
            if (speed < 0.0f)
            {
                _Speed = 5.5f;
            }
            else
            {
                _Speed = -5.5f;
            }
        }
        speed *= -1.0f;

        Debug.Log("speed : " + speed);

        // 충돌하지 않고 한바퀴 돌았을때 게임종료를 위해서 이전 충돌 위치 저장
        PreRot = HitObj.transform.localEulerAngles;

        // 배경 흔들기
        mCanvas.GetComponent<ShakeCamera>().shake = 0.09f;

        // 이펙트 처리
        //Vector3 pos = 부모의 rotation값(쿼터니언) * 자식의 초기 위치값
        Vector3 pos = HitObj.transform.localRotation * hitpos.transform.localPosition;
        pos.z = patical.transform.localPosition.z;
        patical.transform.position = pos;
        patical.transform.localPosition = pos;
        patical.GetComponent<ParticleSystem>().Stop();
        patical.GetComponent<ParticleSystem>().Play();

        PlayerPrefs.SetInt("bestScore", (int)mHitCount);
        PlayerPrefs.Save();
        //if (PlayerPrefs.HasKey("bestScore"))
        //{
        //    // 해당 키가 있으면 mBestScore
        //    PlayerPrefs.GetInt("bestScore", mBestScore);
        //}
        PlayerPrefs.GetInt("bestScore", mBestScore);
        NewBestScoreText.GetComponent<Text>().text = "" + mBestScore;

        mTouch = false;
    }

    // 0 ~ 360 -값은 들어가면 중돌 계산이 안된다
    private void Reset()
    {
        if (HitBg && HitObj)
        {
            mBestScore = 0;
            if (PlayerPrefs.HasKey("bestScore"))
            {
                // 해당 키가 있으면 mBestScore
                PlayerPrefs.GetInt("bestScore", mBestScore);
            }
            else
            {
                PlayerPrefs.SetInt("bestScore",0);
                PlayerPrefs.Save();
            }

            NewBestScoreText.GetComponent<Text>().text = "" + mBestScore;

            Image HitBgImg = HitBg.GetComponent<Image>();
            OriginClor = HitBgImg.color;
            CountText.GetComponent<Text>().text = "0";

            patical.GetComponent<ParticleSystem>().Stop();

            float PlayerR = 0.0f;
            float BackGroundR = Random.Range(90.0f, 270.0f);

            HitObj.transform.localRotation *= Quaternion.AngleAxis(PlayerR, new Vector3(0.0f, 0.0f, 1.0f));
            HitBg.transform.localRotation *= Quaternion.AngleAxis(BackGroundR, new Vector3(0.0f, 0.0f, 1.0f));

            PreRot = HitObj.transform.localEulerAngles;

            mStageCurrentCount = 0;
            mHitCount = 0;

            float RangeMin = GameDataBase.RangeMinMaxData[mStageCurrentCount, 0];
            float RangeMax = GameDataBase.RangeMinMaxData[mStageCurrentCount, 1];
            float yyyy = Random.Range(RangeMin, RangeMax);
            HitBgImg.fillAmount = yyyy;

            int iii = Random.Range(0, 1);
            float _Speed = GameDataBase.SpeedMinMaxData[mStageCurrentCount, iii];
            if (mStageCurrentCount == GameDataBase.StagetotalCount - 1)
            {
                _Speed = Mathf.Abs(6.0f - speed);
            }
            speed += _Speed;
            speed *= -1.0f;

            mTouch = false;
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
