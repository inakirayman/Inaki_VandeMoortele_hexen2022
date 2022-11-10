using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHelper
{
    public const float HexSize = 0.5f;
    public static Hex WorldToHexPosition(Vector3 worldpostion)
    {
        var hexPostionQ = (Mathf.Sqrt(3f) / 3f * worldpostion.x - 1f / 3f * worldpostion.z) / HexSize;
        var hexPostionR = (2f / 3f * worldpostion.z) / HexSize;

        return HexHelper.AxialRound(hexPostionQ,hexPostionR);
        
    }

    public static Vector3 HexToWorldPosition(Hex hexPostion)
    {
        var worldPositionX = HexSize * (3f / 2f * hexPostion.Q);
        var worldPositionZ = HexSize * (Mathf.Sqrt(3f) / 2 * hexPostion.Q + Mathf.Sqrt(3) * hexPostion.R);

        return new Vector3(worldPositionX, 0, worldPositionZ);
    }

    
}
