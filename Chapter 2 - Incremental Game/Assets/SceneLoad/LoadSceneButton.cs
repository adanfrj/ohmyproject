using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class LoadSceneButton : MonoBehaviour
{
	[SerializeField]
	private string sceneToLoad;
	
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			SceneManager.LoadScene(1);
		}
	}

	public void LoadScene()
	{
		gameObject.SetActive(false);
		FindObjectOfType<ProgressSceneLoader>().LoadScene(sceneToLoad);
	}
}