using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
	public struct GridPoint
	{
		public int x;
		public int y;
	};

	public static T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null) return null;
		var comp = go.GetComponent<T>();

		if (comp != null)
			return comp;

		var t = go.transform.parent;
		while (t != null && comp == null)
		{
			comp = t.gameObject.GetComponent<T>();
			t = t.parent;
		}
		return comp;
	}

	public static GridPoint PositionToGridPoint(Vector3 position)
    {
		GridPoint gridPoint;

		position.x = position.x > 0 ? position.x + TempConstant.GridWidth / 2f : position.x - TempConstant.GridWidth / 2f;
		position.z = position.z > 0 ? position.z + TempConstant.GridWidth / 2f : position.z - TempConstant.GridWidth / 2f;

		gridPoint.x = (int)(position.x / TempConstant.GridWidth) + TempConstant.HalfMapWidth;
		gridPoint.y = (int)(position.z / TempConstant.GridWidth) + TempConstant.HalfMapWidth;

		return gridPoint;
	}

    internal static Vector3 GridPointToPosition(GridPoint startPoint, float y)
    {
		float x = (startPoint.x - TempConstant.HalfMapWidth) * TempConstant.GridWidth;
		float z = (startPoint.y - TempConstant.HalfMapWidth) * TempConstant.GridWidth;

		return new Vector3(x, y, z);
    }

    internal static bool IsEqual(GridPoint gridPoint1, GridPoint gridPoint2)
    {
		return (gridPoint1.x == gridPoint2.x) && (gridPoint1.y == gridPoint2.y);
    }
}
