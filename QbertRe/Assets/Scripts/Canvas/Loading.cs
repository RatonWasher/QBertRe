using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Loading : MonoBehaviour {



	void Start () {
        StartCoroutine(loadMenu());
	}


    IEnumerator loadMenu()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
