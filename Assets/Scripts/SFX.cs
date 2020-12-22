using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    [SerializeField]
    AudioSource _audioSourceShot;
    [SerializeField]
    AudioSource _audioSourceOpen;
    [SerializeField]
    AudioSource _audioSourceClose;
    [SerializeField]
    AudioSource _audioSourceCock;

    [SerializeField]
    List<AudioClip> _audioClips = new List<AudioClip>();

    [SerializeField]
    List<AudioClip> _audioClipsOpen = new List<AudioClip>();
    [SerializeField]
    List<AudioClip> _audioClipsClose = new List<AudioClip>();
    [SerializeField]
    List<AudioClip> _audioClipsCock = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        _audioSourceShot = transform.GetChild(1).GetComponent<AudioSource>();
    }

    public void ReloadSound()
    {
        _audioSourceOpen.clip = _audioClipsOpen[Random.Range(0, _audioClipsOpen.Count)];
        _audioSourceOpen.Play();
        StartCoroutine(ReloadCRSound());

    }

    public void ShotSound()
    {
        _audioSourceShot.clip = _audioClips[Random.Range(0, _audioClips.Count)];
        _audioSourceShot.Play();
    }
    
    IEnumerator ReloadCRSound()
    {
        yield return new WaitForSeconds(0.4f);
        _audioSourceClose.clip = _audioClipsClose[Random.Range(0, _audioClipsClose.Count)];
        _audioSourceClose.Play();
        yield return new WaitForSeconds(0.4f);
        _audioSourceCock.clip = _audioClipsCock[Random.Range(0, _audioClipsCock.Count)];
        _audioSourceCock.Play();

    }

}
