using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public enum HexDirection
{
	/*
	NW:좌상 NE:우상
	W:좌 E: 우
	SW:좌하 SE:우하
	*/
	NE, E, SE, SW, W, NW
}

public static class HexDirectionExtensions
{
	public static HexDirection Opposite(this HexDirection direction)
	{
		return (int)direction < 3 ? (direction + 3) : (direction - 3);
	}
}

public class HexMember : MonoBehaviour
{
	[Header("좌표")]
	public int xNum;
	public int zNum;
	public int index;

	[Header("땅인가요?")]
	public bool isGround = false;
	public int mapType = 0; //1=수호의숲, 2=황금평원

	[Header("플레이어가 지나갈 수 있나요?")]
	public bool ispass = false; //통행 가능한가요?

	[Header("Astar G H F 값")]
	public float G; //G : 시작으로 부터 이동했던 거리
	public float H; //H : 추정값 즉 가로+세로 장애물을 무시하여 목표까지의 거리 
	public float F  //F : G+H
	{
		get { return H + G; }
	}
	public HexMember parentNode;

	[Header("이웃노드")]
	[ReadOnly] public HexMember[] neighbors = new HexMember[6];

	public bool doNotUse = false; //본인 노드에 오브젝트 설치가 가능한지

	public string eventtype = ""; //테스트용-------------------------------------------

	public void SetHexMemberData(int xNum, int zNum, int index)
    {
		if (!isGround)
		{
			this.xNum = xNum;
			this.zNum = zNum;
			this.index = index;
		}
	}

	public void SetMapType(int mapType)
    {
		this.mapType = mapType;
		if (mapType != 0)
		{
			Vector3 position = transform.localPosition;
			position.y = 0;
			transform.localPosition = position;

			isGround = true;
			ispass = true;
        }
        else
        {
			Vector3 position = transform.localPosition;
			position.y = -0.5f;
			transform.localPosition = position;

			isGround = false;
			ispass = false;
		}
	}


    public HexMember GetNeighbor(HexDirection direction)
    {
        return neighbors[(int)direction];
    }

    public void SetNeighbor(HexDirection direction, HexMember neighbor)
    {
        neighbors[(int)direction] = neighbor;
        neighbor.neighbors[(int)direction.Opposite()] = this;
    }
}
