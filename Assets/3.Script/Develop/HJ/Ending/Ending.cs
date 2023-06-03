using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    //blackSmith, hunter, scholar
    [SerializeField] GameObject[] playerObj = new GameObject[3];

    Vector3[] case2 = new Vector3[2] { new Vector3(-25.7f, 85f, -2.8f), new Vector3(-9.7f, 85f, -2.8f) };
    Vector3[] case3 = new Vector3[3] { new Vector3(-34f, 85f, 0f), new Vector3(-18f, 85f, 0f), new Vector3(-1.8f, 85f, 0f) };

    private void Start()
    {
        switch (PlayerPrefs.GetInt("PlayerCnt"))
        {
            case 1:
                //�߾ӿ� ȥ��
                if (PlayerPrefs.GetString(string.Format("Class{0}", 0)).Equals("��������"))
                {
                    GameObject temp = Instantiate(playerObj[0]);
                    temp.transform.position = new Vector3(-18.2f, 85, -2.8f);
                }
                else if (PlayerPrefs.GetString(string.Format("Class{0}", 0)).Equals("��ɲ�"))
                {
                    GameObject temp = Instantiate(playerObj[1]);
                    temp.transform.position = new Vector3(-18.2f, 85, -2.8f);
                }
                else
                {
                    GameObject temp = Instantiate(playerObj[2]);
                    temp.transform.position = new Vector3(-18.2f, 85, -2.8f);
                }

                break;

            case 2:
                //��
                for (int i = 0; i < 2; i++)
                {
                    if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("��������"))
                    {
                        GameObject temp = Instantiate(playerObj[0]);
                        temp.transform.position = case2[i];
                    }
                    else if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("��ɲ�"))
                    {
                        GameObject temp = Instantiate(playerObj[1]);
                        temp.transform.position = case2[i];
                    }
                    else
                    {
                        GameObject temp = Instantiate(playerObj[2]);
                        temp.transform.position = case2[i];
                    }
                }
                break;

            case 3:
                //���� ������
                for (int i = 0; i < 3; i++)
                {
                    if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("��������"))
                    {
                        GameObject temp = Instantiate(playerObj[0]);
                        temp.transform.position = case3[i];
                    }
                    else if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("��ɲ�"))
                    {
                        GameObject temp = Instantiate(playerObj[1]);
                        temp.transform.position = case3[i];
                    }
                    else
                    {
                        GameObject temp = Instantiate(playerObj[2]);
                        temp.transform.position = case3[i];
                    }
                }
                break;
        }

        
    }






    public void LobbySceneButton()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
