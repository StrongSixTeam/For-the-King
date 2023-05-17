using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HexMember : MonoBehaviour
{
	//렌더링도 여기서 

	Mesh hexMesh;

	void Awake()
	{
		hexMesh = GetComponent<MeshFilter>().mesh;
	}

}
