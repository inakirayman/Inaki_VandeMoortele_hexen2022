﻿
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class SpawnHelper : MonoBehaviour
{
    public static Position PlayerSpawn = new Position(0, 0);
    
    public static List<Position> ValidPositions = new List<Position>();

    public static void SpawnEnemies(GameObject entity, int amount) 
    {
        var positionViews = FindObjectsOfType<PositionView>();

        foreach(PositionView positionView in positionViews)
        {
            if (positionView.HexPosition.Q != PlayerSpawn.Q && positionView.HexPosition.R != PlayerSpawn.R)
                ValidPositions.Add(positionView.HexPosition);

        }
        

        for(int i = 0; i < amount; i++)
        {
            int random = Random.Range(0, ValidPositions.Count);
            Position position = ValidPositions[random];
            Instantiate(entity, PositionHelper.HexToWorldPosition(position), entity.transform.rotation);
            ValidPositions.RemoveAt(random);
        }

        Debug.Log("Enemies Generated");
    }

    
}

