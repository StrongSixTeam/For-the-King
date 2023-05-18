using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HexDirection
{
	/*
	NW:�»� NE:���
	W:�� E: ��
	SW:���� SE:����
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
	[Header("��ǥ")]
	public int xNum;
	public int zNum;
	public int index;

	[Header("���ΰ���?")]
	public bool isGround = false;
	public int mapType = 0; //1=��ȣ�ǽ�, 2=Ȳ�����

	[Header("�÷��̾ ������ �� �ֳ���?")]
	public bool ispass = false; //���� �����Ѱ���?

	//�ٴ�(����Ʈ) y���� -0.5f������, ������� y�� 0��



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
						//�ʿ�����Ʈũ�������� ��ũ��Ʈ�� �ڽ��� �ѱ�
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
