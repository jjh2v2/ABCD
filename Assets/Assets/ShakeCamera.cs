using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour {

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shake = 0.0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.07f;
    public float decreaseFactor = 0.7f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    public void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shake > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0f;
            camTransform.localPosition = originalPos;
        }
    }


 //   Vector3 originPos;

 //   // Use this for initialization
 //   void Start () {
 //       originPos = transform.localPosition;
 //   }
	
	//// Update is called once per frame
	//void Update () {
		
	//}
 //   public IEnumerator Shake(float _amount, float _duration)
 //   {
 //       float timer = 0;
 //       while (timer <= _duration)
 //       {
 //           transform.localPosition = (Vector3)Random.insideUnitCircle * _amount + originPos;

 //           timer += Time.deltaTime;
 //           yield return null;
 //       }
 //       transform.localPosition = originPos;

 //   }
}
