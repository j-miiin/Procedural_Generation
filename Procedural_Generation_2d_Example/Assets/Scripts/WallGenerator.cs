using System;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPositions = FindWallInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        var cornerWallPositions = FindWallInDirections(floorPositions, Direction2D.diagonalDirectionsList);
        CreateBasicWalls(tilemapVisualizer, basicWallPositions, floorPositions);
        CreateCornerWalls(tilemapVisualizer, cornerWallPositions, floorPositions);
    }

    private static void CreateBasicWalls(TilemapVisualizer tilemapVisualizer
        , HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPositions)
        {
            string neighborBinaryType = "";
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                var neighborPosition = position + direction;
                if (floorPositions.Contains(neighborPosition))
                {
                    neighborBinaryType += "1";
                } else
                {
                    neighborBinaryType += "0";
                }
            }
            tilemapVisualizer.PaintSingleBasicWall(position, neighborBinaryType);
        }
    }

    private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer
        , HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPositions)
        {
            string neighborBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionList)
            {
                var neighborPosition = position + direction;
                if (floorPositions.Contains(neighborPosition))
                {
                    neighborBinaryType += "1";
                } else
                {
                    neighborBinaryType += "0";
                }
            }
            tilemapVisualizer.PaintSingleCornerWall(position, neighborBinaryType);
        }
    }

    private static HashSet<Vector2Int> FindWallInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionsList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in directionsList)
            {
                var neighborPosition = position + direction;
                if (!floorPositions.Contains(neighborPosition))
                    wallPositions.Add(neighborPosition);
            }
        }
        return wallPositions;
    }
}
