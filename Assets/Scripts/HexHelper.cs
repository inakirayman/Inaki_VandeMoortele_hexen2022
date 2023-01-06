using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexHelper
{
    public static Position CubeToAxial(Vector3 cube)
    {
        return new Position((int)cube.x, (int)cube.y);
    }

    public static Vector3 AxialToCube(float q, float r)
    {
        var qCube = q;
        var rCube = r;
        var sCube = -q - r;

        return new Vector3(qCube, rCube, sCube);
    }

    public static Position AxialRound(float q, float r)
    {
        Position hex = CubeToAxial(CubeRound(AxialToCube(q, r)));
        return hex;
    }

    public static Vector3 CubeRound(Vector3 cube)
    {
        var q = Mathf.Round(cube.x);
        var r = Mathf.Round(cube.y);
        var s = Mathf.Round(cube.z);


        var qDiff = Mathf.Abs(q - cube.x);
        var rDiff = Mathf.Abs(r - cube.y);
        var sDiff = Mathf.Abs(s - cube.z);

        if (qDiff > rDiff && qDiff > sDiff)
            q = -r - s;
        else if (rDiff > sDiff)
            r = -q - s;
        else
            s = -q - r;


        return new Vector3(q, r, s);
    }

    public static Position AxialSubtract(Position a, Position b) => new Position(a.Q - b.Q, a.R - b.R);
    public static Position AxialAdd(Position a, Position b) => new Position(a.Q + b.Q, a.R + b.R);

    public static Vector3 CubeAdd(Vector3 a, Vector3 b)
    {
        var x = a.x + b.x;
        var y = a.y + b.y;
        var z = a.z + b.z;

        return new Vector3(x,y,z);
    }

    public static List<Vector3> CubeDirectionVectors = new List<Vector3>
    {
        AxialToCube(0,1),
        AxialToCube(1,0),
        AxialToCube(1,-1),
        AxialToCube(0,-1),
        AxialToCube(-1,0),
        AxialToCube(-1,1)
    };

    public static Vector3 CubeDirection(int direction)
    {
        return CubeDirectionVectors[direction];
    }

    public static Vector3 CubeNeighbor(Vector3 cube, int direction)
    {
        return CubeAdd(cube, CubeDirection(direction));
    }


    public static int AxialDistance(Position a, Position b)
    {
        return (Mathf.Abs(a.Q - b.Q) + Mathf.Abs(a.Q + a.R - b.Q - b.R) + Mathf.Abs(a.R - b.R)) / 2;
    }

    public static Vector3 CubeScale(Vector3 cube, int factor)
    {
        return new Vector3(cube.x * factor, cube.y * factor, cube.z * factor);
    }

    public static List<Vector3> CubeRing(Vector3 center, int radius)
    {
        List<Vector3> results = new List<Vector3>();


        var hex = CubeAdd(center, CubeScale(CubeDirection(4), radius));
        for (int i = 0; i < 6; i++)
        {

            for (int j = 0; j < radius; j++)
            {
               
                results.Add(hex);
                hex = CubeNeighbor(hex, i);
            }

        }
        return results;
    }

    public static List<Position> AxialRing(Position center, int radius)
    {
        // Convert the center position from axial to cube coordinates
        Vector3 centerCube = AxialToCube(center.Q, center.R);

        // Get the cube ring around the center position
        var vectors = CubeRing(centerCube, radius);

        // Convert the cube coordinates to axial coordinates and create a Position object for each one
        List<Position> positions = new List<Position>();
        foreach (Vector3 v in vectors)
        {
            Position position = CubeToAxial(v);
            if ((PositionHelper.Distance >= HexHelper.AxialDistance(new Position(0, 0), position)))
            positions.Add(position);
        }

        return positions;
    }

    public static List<Vector3> CubeSpiral(Vector3 center, int radius)
    {
        var results = new List<Vector3> { center };
        for (int i = 1; i <= radius; i++)
        {
            results.AddRange(CubeRing(center, i));
        }
        return results;
    }

    public static List<Position> AxialSpiral(Position center, int radius)
    {
        Vector3 centerCube = AxialToCube(center.Q, center.R);

        var vectors = CubeSpiral(centerCube, radius);

        List<Position> positions = new List<Position>();
        foreach (Vector3 v in vectors)
        {
            Position position = CubeToAxial(v);
            if ((PositionHelper.Distance >= HexHelper.AxialDistance(new Position(0, 0), position)))
                positions.Add(position);
        }

        return positions;


    } 

    public static Position ReflectQ(Position position)
    {
        Vector3 cube = AxialToCube(position.Q,position.R);
        return CubeToAxial(new Vector3(cube.x,cube.z,cube.y));
    }

    public static Position ReflectR(Position position)
    {
        Vector3 cube = AxialToCube(position.Q, position.R);
        return CubeToAxial(new Vector3(cube.z, cube.y, cube.x));
    }

    public static Position ReflectS(Position position)
    {
        Vector3 cube = AxialToCube(position.Q, position.R);
        return CubeToAxial(new Vector3(cube.y, cube.x, cube.z));
    }

    public static Position InvertPosition(Position position)
    {
        var vector = AxialToCube(position.Q, position.R);

        vector *= -1;

        return CubeToAxial(vector);

    }



}
