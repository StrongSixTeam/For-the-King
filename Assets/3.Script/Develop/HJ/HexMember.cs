using System.Collections;
using System.Collections.Generic;
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

	//바다(디폴트) y값은 -0.5f이지만, 육지라면 y가 0임



	public void SetHexMemberData(int xNum, int zNum, int index, int mapType)
    {
		if (!isGround)
		{
			this.xNum = xNum;
			this.zNum = zNum;
			this.index = index;

			this.mapType = mapType;
            if (mapType != 0)
            {
				Vector3 position = transform.localPosition;
				position.y = 0;
				transform.localPosition = position;

				isGround = true;
				ispass = true;

                switch (mapType)
                {
					case 1:
						//맵오브젝트크리에이터 스크립트에 자신을 넘김
						break;
					case 2:

						break;
					default:
						Debug.Log("mapType null");
						break;
                }
			}
		}
	}

	

	//[SerializeField] HexMember[] neighbors = new HexMember[6];

	//   public HexMember GetNeighbor(HexDirection direction)
	//   {
	//       return neighbors[(int)direction];
	//   }

	//   public void SetNeighbor(HexDirection direction, HexMember neighbor)
	//   {
	//       neighbors[(int)direction] = neighbor;
	//	neighbor.neighbors[(int)direction.Opposite()] = this;
	//}
}
