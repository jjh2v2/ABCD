using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDataBase
{

    public const int StagetotalCount = 20;
    public static uint[] StageHitCount = new uint[StagetotalCount]
    {
        3,
        3,
        4,
        4,
        5,
        5,
        6,
        6,
        7,
        7,//10
        8,
        8,
        9,
        9,
        10,
        10,
        11,
        11,
        12,
        12//20
    };// 150히트

    public static float[,] RangeMinMaxData = new float[StagetotalCount,2]
    {
        {0.25f, 0.25f},
        {0.25f, 0.25f},
        {0.24f, 0.25f},
        {0.24f, 0.25f},
        {0.23f, 0.25f},
        {0.22f, 0.25f},
        {0.22f, 0.25f},
        {0.19f, 0.22f},
        {0.19f, 0.22f},
        {0.16f, 0.19f},//10
        {0.16f, 0.19f},
        {0.13f, 0.16f},
        {0.13f, 0.16f},
        {0.10f, 0.13f},
        {0.10f, 0.13f},
        {0.07f, 0.10f},
        {0.07f, 0.10f},
        {0.05f, 0.07f},
        {0.05f, 0.07f},
        {0.05f, 0.25f}//20
    };

    public static float[,] SpeedMinMaxData = new float[StagetotalCount, 2]
    {
        {0.0f, 0.05f},
        {0.1f, 0.2f},
        {0.0f, 0.05f},
        {0.05f, 0.2f},
        {0.1f, 0.2f},
        {0.05f, 0.1f},
        {0.0f, 0.2f},
        {0.0f, 0.1f},
        {0.1f, 0.2f},
        {0.05f, 0.1f},//10
        {0.1f, 0.2f},
        {0.0f, 0.2f},
        {0.05f, 0.1f},
        {0.0f, 0.1f},
        {0.0f, 0.1f},
        {0.0f, 0.1f},
        {0.0f, 0.1f},
        {0.0f, 0.1f},
        {0.0f, 0.1f},
        {0.0f, 0.0f}//20
    };

}
