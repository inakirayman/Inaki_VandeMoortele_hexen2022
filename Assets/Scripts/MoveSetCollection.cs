using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSetCollection 
{
    

    public static List<List<Position>> GetValidTilesForShoot(PieceView player, Board board)
    {
        List<List<Position>> positions = new List<List<Position>>();

        positions.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).RightUp().CollectValidPositions());
        positions.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).Right().CollectValidPositions());
        positions.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).RightDown().CollectValidPositions());
        positions.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).LeftUp().CollectValidPositions());
        positions.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).Left().CollectValidPositions());
        positions.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).LeftDown().CollectValidPositions());

        return positions;
    }

    public static List<List<Position>> GetValidTilesForCone(PieceView player , Board board)
    {
        List<List<Position>> positions = new List<List<Position>>();

        positions.Add(GetTileConeRightUp(player, board));
        positions.Add(GetTileConeRight(player, board));
        positions.Add(GetTileConeRightDown(player, board));
        positions.Add(GetTileConeLeftUp(player, board));
        positions.Add(GetTileConeLeft(player, board));
        positions.Add(GetTileConeLeftDown(player, board));

        return positions;
    }


    private static List<Position> GetTileConeRightUp(PieceView player, Board board)
    {
        List<List<Position>> positionsList = new List<List<Position>>();

        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).LeftUp(1).CollectValidPositions()); //Left
        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).RightUp(1).CollectValidPositions()); //Center
        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).Right(1).CollectValidPositions()); //Right


        List<Position> validPositions = new List<Position>();

        foreach (List<Position> positions in positionsList)
        {
            foreach (Position position in positions)
            {
                validPositions.Add(position);
            }
        }


        return validPositions;
    }

    private static List<Position> GetTileConeRight(PieceView player, Board board)
    {
        List<List<Position>> positionsList = new List<List<Position>>();

        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).RightUp(1).CollectValidPositions()); //Left
        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).Right(1).CollectValidPositions()); //Center
        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).RightDown(1).CollectValidPositions()); //Right


        List<Position> validPositions = new List<Position>();

        foreach (List<Position> positions in positionsList)
        {
            foreach (Position position in positions)
            {
                validPositions.Add(position);
            }
        }


        return validPositions;
    }
    private static List<Position> GetTileConeRightDown(PieceView player, Board board)
    {
        List<List<Position>> positionsList = new List<List<Position>>();

        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).Right(1).CollectValidPositions()); //Left
        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).RightDown(1).CollectValidPositions()); //Center
        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).LeftDown(1).CollectValidPositions()); //Right


        List<Position> validPositions = new List<Position>();

        foreach (List<Position> positions in positionsList)
        {
            foreach (Position position in positions)
            {
                validPositions.Add(position);
            }
        }


        return validPositions;
    }
    private static List<Position> GetTileConeLeftUp(PieceView player, Board board)
    {
        List<List<Position>> positionsList = new List<List<Position>>();

        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).RightUp(1).CollectValidPositions()); //Left
        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).LeftUp(1).CollectValidPositions()); //Center
        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).Left(1).CollectValidPositions()); //Right


        List<Position> validPositions = new List<Position>();

        foreach (List<Position> positions in positionsList)
        {
            foreach (Position position in positions)
            {
                validPositions.Add(position);
            }
        }


        return validPositions;
    }

    private static List<Position> GetTileConeLeft(PieceView player, Board board)
    {
        List<List<Position>> positionsList = new List<List<Position>>();

        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).LeftUp(1).CollectValidPositions()); //Left
        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).Left(1).CollectValidPositions()); //Center
        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).LeftDown(1).CollectValidPositions()); //Right


        List<Position> validPositions = new List<Position>();

        foreach (List<Position> positions in positionsList)
        {
            foreach (Position position in positions)
            {
                validPositions.Add(position);
            }
        }


        return validPositions;
    }
    private static List<Position> GetTileConeLeftDown(PieceView player, Board board)
    {
        List<List<Position>> positionsList = new List<List<Position>>();

        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).Left(1).CollectValidPositions()); //Left
        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).LeftDown(1).CollectValidPositions()); //Center
        positionsList.Add(new MoveSetHelper(board, PositionHelper.WorldToHexPosition(player.WorldPosition)).RightDown(1).CollectValidPositions()); //Right


        List<Position> validPositions = new List<Position>();

        foreach (List<Position> positions in positionsList)
        {
            foreach (Position position in positions)
            {
                validPositions.Add(position);
            }
        }


        return validPositions;
    }





}
