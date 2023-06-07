using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingMainCamera : MonoBehaviour
{
    private Volume _postProcessVolume;
    private ColorAdjustments _colorAdjustments;

    private void Start()
    {
        _postProcessVolume = FindObjectOfType<Volume>();
        _postProcessVolume.profile.TryGet(out _colorAdjustments);
    }

    public void SetColorFilter(bool Day)
    {
        StartCoroutine(TimeLightCo(Day));
        //if (on) //³·À¸·Î
        //{
        //    //_colorAdjustments.colorFilter.Interp(new Color(0.6320754f, 0.5635012f, 0.5635012f, 0), new Color(1, 1, 1 , 0), 10f);
        //    _colorAdjustments.postExposure.Interp( 0.39f, -0.9f, 10f * Time.deltaTime);
        //}
        //else //¹ãÀ¸·Î
        //{
        //    //_colorAdjustments.colorFilter.Interp(new Color(1, 1, 1, 0), new Color(0.6320754f, 0.5635012f, 0.5635012f, 0), 10f);
        //    _colorAdjustments.postExposure.Interp(-0.9f, 0.39f , 10f * Time.deltaTime);
        //}
    }

    IEnumerator TimeLightCo(bool Day)
    {
        if (!Day) 
        {
            float rTime = 0f;
            //Debug.Log("³·À¸·Î");
            while (true)
            {
                
                _colorAdjustments.postExposure.Interp(0.4f, -0.9f, rTime);
                rTime += Time.deltaTime;
                yield return null;
                if(rTime > 1)
                {
                    _colorAdjustments.postExposure.value = -0.9f;
                    break;
                }
            }
            
        }
        else 
        {
            float rTime = 0f;
            //Debug.Log("¹ãÀ¸·Î");
            while (true)
            {
                
                _colorAdjustments.postExposure.Interp(-0.9f, 0.4f, rTime);
                rTime += Time.deltaTime;
                yield return null;
                if (rTime > 1)
                {
                    _colorAdjustments.postExposure.value = 0.39f;
                    break;
                }
            }
        }
    }
}
