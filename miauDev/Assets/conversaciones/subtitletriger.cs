using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class SubtitleTriggerTMP : MonoBehaviour
{
    [System.Serializable]
    public class SubtitleLine
    {
        public string speakerName;
        [TextArea(2, 4)]
        public string content;
        public float duration = 3f;
    }

    [Header("Configuración de subtítulos")]
    public List<SubtitleLine> subtitles = new List<SubtitleLine>();

    [Header("Referencias UI")]
    public GameObject subtitlePanel;
    public TextMeshProUGUI speakerNameTMP;
    public TextMeshProUGUI subtitleTMP;
    public AudioSource typeSound;

    [Header("Efectos visuales")]
    public float fadeDuration = 0.7f;
    public float typingSpeed = 0.03f;

    [Header("Opciones")]
    public bool playOnce = true;
    public bool allowSkip = true;

    private bool hasPlayed = false;
    private Coroutine subtitleCoroutine;
    private bool skipTyping = false;
    private CanvasGroup panelGroup;

    private void Start()
    {
        if (subtitlePanel != null)
        {
            panelGroup = subtitlePanel.GetComponent<CanvasGroup>();
            if (panelGroup == null)
                panelGroup = subtitlePanel.AddComponent<CanvasGroup>();

            panelGroup.alpha = 0;
            subtitlePanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && (!playOnce || !hasPlayed))
        {
            if (subtitleCoroutine == null)
            {
                subtitleCoroutine = StartCoroutine(PlaySubtitles());
                hasPlayed = true;
            }
        }
    }

    private IEnumerator PlaySubtitles()
    {
        if (subtitlePanel == null || speakerNameTMP == null || subtitleTMP == null)
            yield break;

        subtitlePanel.SetActive(true);
        yield return StartCoroutine(FadePanel(panelGroup, 1f, fadeDuration));

        foreach (SubtitleLine line in subtitles)
        {
            speakerNameTMP.text = line.speakerName;
            yield return StartCoroutine(TypeText(line.content));

            float timer = 0f;
            while (timer < line.duration)
            {
                if (allowSkip && Keyboard.current.eKey.wasPressedThisFrame)
                    break;

                timer += Time.deltaTime;
                yield return null;
            }

            // Pequeño fade entre líneas
            yield return StartCoroutine(FadePanel(panelGroup, 0f, fadeDuration));
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(FadePanel(panelGroup, 1f, fadeDuration));
        }

        // Fade out al final
        yield return StartCoroutine(FadePanel(panelGroup, 0f, fadeDuration));
        subtitlePanel.SetActive(false);
        subtitleCoroutine = null;
    }

    private IEnumerator TypeText(string fullText)
    {
        subtitleTMP.text = "";
        skipTyping = false;

        foreach (char c in fullText)
        {
            if (allowSkip && Keyboard.current.eKey.wasPressedThisFrame)
            {
                subtitleTMP.text = fullText;
                skipTyping = true;
                break;
            }

            subtitleTMP.text += c;

            if (typeSound != null)
                typeSound.Play();

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    //  Efecto de desvanecimiento suave
    private IEnumerator FadePanel(CanvasGroup canvasGroup, float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
