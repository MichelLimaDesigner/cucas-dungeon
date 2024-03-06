using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  [Header("----------- Audio Source ------------")]
  [SerializeField] AudioSource musicSouce;
  [SerializeField] AudioSource backgroundSouce;
  [SerializeField] AudioSource SFXSouce;

  [Header("----------- Audio Clip ------------")]
  public AudioClip background;
  public AudioClip music;
  public AudioClip walkSFX;
  public AudioClip transformationSFX;
  public AudioClip cururuSFX;
  public AudioClip morcegoSFX;
  public AudioClip pombaSFX;
  public AudioClip flySFX;
  public AudioClip buttonSFX;

  // Start is called before the first frame update
  void Start()
  {
    musicSouce.clip = music;
    backgroundSouce.clip = background;
    musicSouce.Play();
    // backgroundSouce.Play();
  }

  public void PlaySFX(AudioClip clip)
  {
    SFXSouce.PlayOneShot(clip);
  }
}
