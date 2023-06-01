using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
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

	[Header("Astar G H F ��")]
	public float G; //G : �������� ���� �̵��ߴ� �Ÿ�
	public float H; //H : ������ �� ����+���� ��ֹ��� �����Ͽ� ��ǥ������ �Ÿ� 
	public float F  //F : G+H
	{
		get { return H + G; }
	}
	public HexMember parentNode;

	[Header("�̿����")]
	[ReadOnly] public HexMember[] neighbors = new HexMember[6];

	public bool doNotUse = false; //���� ��忡 ������Ʈ ��ġ�� ��������

	public string eventtype = ""; //�׽�Ʈ��-------------------------------------------

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
