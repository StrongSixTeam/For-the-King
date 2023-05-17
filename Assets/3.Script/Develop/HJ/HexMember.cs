using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HexDirection
{
	/*
	NW:ÁÂ»ó NE:¿ì»ó
	W:ÁÂ E: ¿ì
	SW:ÁÂÇÏ SE:¿ìÇÏ
	*/
	NW, NE, W, E, SW, SE
}

public class HexMember : MonoBehaviour
{
	
	public int xNum;
	public int zNum;
	public int index;

	public bool isGround = false;
	
	public float elevation;

    public void Setelevation()
    {
        if (isGround)
        {
			elevation = 0f;
			Vector3 position = transform.localPosition;
			position.y = elevation;
			transform.localPosition = position;
		}

	}

	[SerializeField] HexMember[] neighbors = new HexMember[6];

	public HexMember GetNeighbor(HexDirection direction)
	{
		return neighbors[(int)direction];
	}

	public void SetNeighbor(HexDirection direction, HexMember neighbor)
	{
		neighbors[(int)direction] = neighbor;
		neighbor.neighbors[(int)direction + 1] = this;
	}
}
