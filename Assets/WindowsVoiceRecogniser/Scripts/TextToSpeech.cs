using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TextToSpeech : MonoBehaviour{
#if UNITY_EDITOR
    public AudioSource _audio;

    public static TextToSpeech Instance;

    private string text;

    void Start()
    {

        Instance = this;
        _audio = gameObject.GetComponent<AudioSource>(); 
    }

    // Метод произносит вслух текст:
    public void sey(string text) {
        this.text = text;

        StartCoroutine(downloadTheAudio(text, AudioType.MPEG));
    }

    // Энумератор выполняет запрос к гоогле транслит и пишет аудио:
    IEnumerator downloadTheAudio(string text, AudioType audioType) {

        string url = "https://transliate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tv-ob&q=SampleText&tl=En-gb";

        //string url = "https://translate.google.ru/translate_tts?ie=UTF-8&q=Work&tl=en&total=1&idx=0&textlen=4&tk=981155.552246&client=webapp&prev=input&ttsspeed=0.24";

        using(UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, audioType))
        {
            yield return www.SendWebRequest();

            if(www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                _audio.clip = DownloadHandlerAudioClip.GetContent(www);
                _audio.Play();
            }
        }
    }
#endif
}