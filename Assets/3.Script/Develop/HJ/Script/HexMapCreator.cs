using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//�������� ���� ����
public class HexSixe
{
    public float hexHeight = 3.332823f;
    public float hexWidth = 2.886311f;
}

//[System.Serializable]
//public class MapSaveData
//{
//    public int index;
//    public int mapType = 0;
//}

public class HexMapCreator : MonoBehaviour
{
    HexSixe hexSixe = new HexSixe();

    //�Ʒ� �������� ������ ���� ������
    private int width = 60;
    private int height = 50;

    public int mapSize;

    //materials (�ٴ�=0, ��=1, ���=2)
    [SerializeField] Material[] materials = new Material[3];


    [SerializeField] HexMember hexMember; //hexOverlayGeo01_0
    public HexMember[] hexMembers;


    //�̵�Ƚ���� ǥ���� ĵ����
    private Canvas gridCanvas;

    //��ü �׸��忡 �̹����� ��ϱ� �ʹ� ���� �ɸ�
    //���� 0~10 �̹����� �迭�� ��Ƽ�
    //�̵���� ��忡 ���� �

    //���߿� ���̽�Ÿ �׽�Ʈ�Ҷ� ����Ұ���
    //[SerializeField] private Text gridText; //���߿� �̹��� ��ü�Ұ���
    //private Text[] gridTexts; //�ε��� ������ �����ҿ��� (hexMembers[0] =¦��= gridImages[0])


    //���� ����
    int centerIndex = 1350;
    //������ Ƚ��
    int extendSize = 50;
    int randomNum;
    bool isLoop = true;

    public int forestNodeCount = 0;
    public int plainsNodeCount = 0;


    bool plainsStartDir;

    [SerializeField] GameObject mapObjectCreatorObj;

    //MapSaveData[] mapSaveData;
    string path;


    private void Start()
    {
        path = Path.Combine(Application.dataPath + "MapSaveData.json");
        mapSize = height * width;

        gridCanvas = GetComponentInChildren<Canvas>();

        //������ ������ŭ ����
        hexMembers = new HexMember[mapSize];
        //gridTexts = new Text[mapSize];


        //x�� z�� ��ǥ�� �ȴ�
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateHex(x, z, i++);
            }
        }


        //�� ���� ����
        SetGround();
        //Debug.Log(path);
        //if (File.Exists(path))
        //{
        //    //�� ���� �ҷ�����
        //    string jsonData = File.ReadAllText(path);
        //    mapSaveData = JsonUtility.FromJson<MapSaveData[]>(jsonData);

        //    for (int i = 0; i > mapSize; i++)
        //    {
        //        if (mapSaveData[i].mapType != 0)
        //        {
        //            ChangeMaterial(i, mapSaveData[i].mapType);
        //        }
        //    }

        //}
        //else
        //{
        //    //�� ���� ����
        //    SetGround();


        //    mapSaveData = new MapSaveData[forestNodeCount + plainsNodeCount];
        //    int indexCount = 0;

        //    //�� ������ ����
        //    for (int i = 0; i < mapSize; i++)
        //    {
        //        if (hexMembers[i].mapType != 0)
        //        {
        //            mapSaveData[indexCount] = new MapSaveData
        //            {
        //                index = i,
        //                mapType = hexMembers[i].mapType
        //            };
        //            indexCount++;
        //        }
        //    }

        //    string jsonData = JsonUtility.ToJson(mapSaveData);
        //    Debug.Log(jsonData);
        //    //File.WriteAllText(path, jsonData);
        //}

        //�� ������Ʈ ũ�������� ����
        StartCoroutine(StartMapObjectCreate_co());
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

        hex.SetHexMemberData(x, z, i); //��Ÿ�� ���� �ʿ�


        //�̿� ����
        if (x > 0)
        {
            hex.SetNeighbor(HexDirection.W, hexMembers[i - 1]);
        }
        if (z > 0)
        {
            if ((z & 1) == 0)
            {
                hex.SetNeighbor(HexDirection.SE, hexMembers[i - width]);
                if (x > 0)
                {
                    hex.SetNeighbor(HexDirection.SW, hexMembers[i - width - 1]);
                }
            }
            else
            {
                hex.SetNeighbor(HexDirection.SW, hexMembers[i - width]);
                if (x < width - 1)
                {
                    hex.SetNeighbor(HexDirection.SE, hexMembers[i - width + 1]);
                }
            }
        }

    }

    //���� �� ��带 �����Ѵ�
    private void SetGround()
    {
        //������Ʈ
        ForestNode();
        while (forestNodeCount < 220)
        {
            ForestNode();
        }


        //Ȳ����� ������ ã��
        int loopNum = 0;
        if (hexMembers[centerIndex].xNum >= 30)
        {
            plainsStartDir = true;
        }
        else
        {
            plainsStartDir = false;
        }

        while (isLoop)
        {
            switch (plainsStartDir)
            {
                case true: //1���ؿ�������
                    centerIndex -= 2;
                    break;

                case false: //1���� ���������� 
                    centerIndex += 2;
                    break;
            }

            if (hexMembers[centerIndex].mapType != 1)
            {
                isLoop = false;
                break;
            }

            if (loopNum++ > 500)
            {
                throw new System.Exception("Infinite Loop : 1");
            }
        }
        isLoop = true;


        //Ȳ�����
        PlainsNode();
        while (plainsNodeCount < 200)
        {
            PlainsNode();
        }

    }


    IEnumerator StartMapObjectCreate_co()
    {
        yield return null; //hexMembers[]�� ä������ ���� �����ӿ� ����

        Instantiate(mapObjectCreatorObj);
        
        yield break;
    }


    void ForestNode()
    {
        for (int c = 0; c < extendSize; c++)
        {
            for (int d = 0; d < 6; d++)
            {
                ChangeMaterial(hexMembers[centerIndex].neighbors[d].index, 1);
            }

            while (isLoop)
            {
                randomNum = Random.Range(0, 6);
                if (hexMembers[centerIndex].neighbors[randomNum].xNum > 10 &&
                    hexMembers[centerIndex].neighbors[randomNum].xNum < 49 &&
                    hexMembers[centerIndex].neighbors[randomNum].zNum > 10 &&
                    hexMembers[centerIndex].neighbors[randomNum].zNum < 39)
                {
                    isLoop = false;
                }
            }

            isLoop = true;

            centerIndex = hexMembers[centerIndex].neighbors[randomNum].index;
        }
    }

    void PlainsNode()
    {
        for (int c = 0; c < extendSize; c++)
        {
            for (int d = 0; d < 6; d++)
            {
                ChangeMaterial(hexMembers[centerIndex].neighbors[d].index, 2);
            }


            int loopNum = 0;
            while (isLoop)
            {
                randomNum = Random.Range(0, 6);
                if (hexMembers[centerIndex].neighbors[randomNum].xNum > 10 &&
                    hexMembers[centerIndex].neighbors[randomNum].xNum < 49 &&
                    hexMembers[centerIndex].neighbors[randomNum].zNum > 10 &&
                    hexMembers[centerIndex].neighbors[randomNum].zNum < 39)
                {
                    if (hexMembers[centerIndex].neighbors[randomNum].mapType != 1)
                    {
                        isLoop = false;
                    }
                }

                if (loopNum++ > 500)
                {
                    throw new System.Exception("Infinite Loop : 2");
                }
            }


            isLoop = true;

            centerIndex = hexMembers[centerIndex].neighbors[randomNum].index;
        }
    }

    //�Ű������� ���޹޴� index�� Material�� �ٲ۴�
    private void ChangeMaterial(int i, int materialsNumber)
    {
        if (!hexMembers[i].isGround)
        {
            Renderer renderer = hexMembers[i].gameObject.GetComponent<Renderer>();
            renderer.material = materials[materialsNumber];
            hexMembers[i].mapType = materialsNumber;
            hexMembers[i].isGround = true;
            hexMembers[i].ispass = true;
            if (materialsNumber.Equals(1))
            {
                forestNodeCount++;
            }
            else if (materialsNumber.Equals(2))
            {
                plainsNodeCount++;
            }

            hexMembers[i].SetMapType(materialsNumber);
        }
    }

    //private void CreateGrid(int x, int z, int i)
    //{
    //    Vector3 position;
    //    position.x = (x + z * 0.5f - z / 2) * (hexSixe.hexWidth);
    //    position.y = (z) * (hexSixe.hexHeight * 0.75f);
    //    position.z = 0f;

    //    Text text = gridTexts[i] = Instantiate(gridText);
    //    text.text = "" + i;
    //    text.transform.SetParent(gridCanvas.transform, false);
    //    text.transform.localPosition = position;
    //}

}
