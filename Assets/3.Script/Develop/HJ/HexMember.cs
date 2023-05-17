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
	NW, NE, W, E, SW, SE
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
	private float elevation = 0f;

	public void SetHexMemberData(int xNum, int zNum, int index, int mapType)
    {
		if (!isGround)
		{
			Vector3 position = transform.localPosition;
			position.y = elevation;
			transform.localPosition = position;

			this.xNum = xNum;
			this.zNum = zNum;
			this.index = index;

			this.mapType = mapType;
            if (mapType != 0)
            {
				isGround = true;
				ispass = true;
			}
		}
	}

	//[SerializeField] HexMember[] neighbors = new HexMember[6];

	//public HexMember GetNeighbor(HexDirection direction)
	//{
	//	return neighbors[(int)direction];
	//}

	//public void SetNeighbor(HexDirection direction, HexMember neighbor)
	//{
	//	neighbors[(int)direction] = neighbor;
	//	neighbor.neighbors[(int)direction + 1] = this;
	//}
}
