using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���� ����
public class HexSixe
{
    public float hexHeight = 3.332823f; 
    public float hexWidth = 2.886311f; 
}


public class HexMapCreator : MonoBehaviour
{
    HexSixe hexSixe = new HexSixe();

    //80*80 ������ ���� ������
    private int width = 80;
    private int height = 80;

    public int mapSize;

    //materials (�ٴ�=0, ��=1, ���=2)
    [SerializeField] Material[] materials= new Material[3];


    [SerializeField] HexMember hexMember; //hexOverlayGeo01_0
    private HexMember[] hexMembers;


    //�̵�Ƚ���� ǥ���� ĵ����
    private Canvas gridCanvas;
    [SerializeField] private Image gridImage; //���߿� �̹��� ��ü�Ұ���
    private Image[] gridImages; //�ε��� ������ �����ҿ��� (hexMembers[0] =¦��= gridImages[0])


    private void Start()
    {
        mapSize = height * width;

        gridCanvas = GetComponentInChildren<Canvas>();

        //������ ������ŭ ����
        hexMembers = new HexMember[mapSize];
        gridImages = new Image[mapSize];


        //x�� z�� ��ǥ�� �ȴ�
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateHex(x, z, i++);
            }
        }
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateGrid(x, z, i++);
            }
        }
    }

    private void CreateHex(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (hexSixe.hexWidth);
        position.y = -0.5f;
        position.z = z * (hexSixe.hexHeight * 0.75f);

        HexMember hex = hexMembers[i] = Instantiate(hexMember);
        hex.transform.SetParent(transform, false);
        hex.transform.localPosition = position;

        hex.SetHexMemberData(x, z, i, SetMaterial(x, z, i));


        ////�̿� ����
        //if (x > 0)
        //{
        //    hex.SetNeighbor(HexDirection.W, hexMembers[i - 1]);
        //}
        //if (z > 0)
        //{
        //    if ((z & 1) == 0)
        //    {
        //        hex.SetNeighbor(HexDirection.SE, hexMembers[i - width]);
        //        if (x > 0)
        //        {
        //            hex.SetNeighbor(HexDirection.SW, hexMembers[i - width - 1]);
        //        }
        //    }
        //    else
        //    {
        //        hex.SetNeighbor(HexDirection.SW, hexMembers[i - width]);
        //        if (x < width - 1)
        //        {
        //            hex.SetNeighbor(HexDirection.SE, hexMembers[i - width + 1]);
        //        }
        //    }
        //}
    }

    //���� �� ��带 �����Ѵ� (���߿� �ð��� �Ǹ� [���� �� ����] ����!~)
    private int SetMaterial(int x, int z, int i)
    {
        if (z < 34 && z > 15 && x > 40 && x < 57)
        {
            ChangeMaterial(i, 1);
            return 1;
        }
        else if (z < 35 && z > 23 && x > 33 && x < 47)
        {
            ChangeMaterial(i, 1);
            return 1;
        }
        else if (z < 24 && z > 18 && x > 36 && x < 41)
        {
            ChangeMaterial(i, 1);
            return 1;
        }

        if (z < 46 && z > 27 && x > 25 && x < 40)
        {
            ChangeMaterial(i, 2);
            return 2;
        }
        else if (z < 57 && z > 35 && x > 28 && x < 52)
        {
            ChangeMaterial(i, 2);
            return 2;
        }

        return 0;
    }

    //�Ű������� ���޹޴� index�� Material�� �ٲ۴�
    private void ChangeMaterial(int i, int materialsNumber)
    {
        Renderer renderer = hexMembers[i].gameObject.GetComponent<Renderer>();
        if (renderer.material != materials[materialsNumber])
        {
            renderer.material = materials[materialsNumber];
        }
    }

    private void CreateGrid(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (hexSixe.hexWidth);
        position.y = (z) * (hexSixe.hexHeight * 0.75f);
        position.z = 0f;

        Image image = gridImages[i] = Instantiate(gridImage);
        image.transform.SetParent(gridCanvas.transform, false);
        image.color = Color.green;
        image.transform.localPosition = position;
    }

}
