using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    protected static int checkPointCounter = 0;
    public static Dictionary<Vector2, int> enemyPrefabPositionInMap = new Dictionary<Vector2, int>();

    void Start()
    {
        enemyPrefabPositionInMap[new Vector2(102.3831f, -35.1f)] = 1;
        enemyPrefabPositionInMap[new Vector2(102.3831f, 53.98661f)] = 1;
    }

}
