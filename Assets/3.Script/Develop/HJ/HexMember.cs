using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HexMember : MonoBehaviour
{
	//�������� ���⼭ 

	Mesh hexMesh;

	void Awake()
	{
		hexMesh = GetComponent<MeshFilter>().mesh;
	}

}
