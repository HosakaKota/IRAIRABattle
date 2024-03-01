using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
	[SerializeField]
	//�@�|�[�Y�������ɕ\������UI�̃v���n�u
	private GameObject pauseUIPrefab;
	//�@�|�[�YUI�̃C���X�^���X
	private GameObject pauseUIInstance;

	public bool PauseFlag = true;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown("q"))
		{
			if (pauseUIInstance == null)
			{
				pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
				Time.timeScale = 0f;
				PauseFlag = false;
			}
			else
			{
				Destroy(pauseUIInstance);
				Time.timeScale = 1f;
				PauseFlag = true;
			}
		}
	}
}