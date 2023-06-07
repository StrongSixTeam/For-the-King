using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;


    //blackSmith, hunter, scholar
    [SerializeField] GameObject[] playerObj = new GameObject[3];

    Vector3[] case2 = new Vector3[2] { new Vector3(-25.7f, 85f, -2.8f), new Vector3(-9.7f, 85f, -2.8f) };
    Vector3[] case3 = new Vector3[3] { new Vector3(-34f, 85f, 0f), new Vector3(-18f, 85f, 0f), new Vector3(-1.8f, 85f, 0f) };

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        switch (PlayerPrefs.GetInt("PlayerCnt"))
        {
            case 1:
                //중앙에 혼자
                if (PlayerPrefs.GetString(string.Format("Class{0}", 0)).Equals("대장장이"))
                {
                    GameObject temp = Instantiate(playerObj[0]);
                    temp.transform.position = new Vector3(-18.2f, 85, -2.8f);
                }
                else if (PlayerPrefs.GetString(string.Format("Class{0}", 0)).Equals("사냥꾼"))
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
                //둘
                for (int i = 0; i < 2; i++)
                {
                    if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("대장장이"))
                    {
                        GameObject temp = Instantiate(playerObj[0]);
                        temp.transform.position = case2[i];
                    }
                    else if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("사냥꾼"))
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
                //셋이 나란히
                for (int i = 0; i < 3; i++)
                {
                    if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("대장장이"))
                    {
                        GameObject temp = Instantiate(playerObj[0]);
                        temp.transform.position = case3[i];
                    }
                    else if (PlayerPrefs.GetString(string.Format("Class{0}", i)).Equals("사냥꾼"))
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


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseInput();
        }
    }
    private void MouseInput()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 100f);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.GetComponent<Animator>() != null)
            {
                hits[i].transform.GetComponent<Animator>().SetBool("Battle", true);
                hits[i].transform.GetComponent<Animator>().SetBool("Hit", true);
                hits[i].transform.GetComponent<Animator>().SetBool("Die", true);
                audioSource.Play();
                return;
            }
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
