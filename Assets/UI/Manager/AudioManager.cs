using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	public AudioSource backgroundMusic;

	private static AudioManager instance;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		backgroundMusic.Play();
	}

	private void Update()
	{
		// 3번 씬으로 전환할 때 배경음악을 중지
		if (SceneManager.GetActiveScene().buildIndex == 2)
		{
			backgroundMusic.Stop();
		}
	}
}
