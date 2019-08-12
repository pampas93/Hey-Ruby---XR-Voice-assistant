using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using TMPro;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.Events;

public class DictationTest : MonoBehaviour, IMixedRealitySpeechHandler
{
    [SerializeField]
    private TMP_Text m_Hypotheses;
    [SerializeField]
    private TMP_Text m_Recognitions;

    private DictationRecognizer m_DictationRecognizer;

    [SerializeField]
    private bool m_enableListening = false;
    public bool EnableListening 
    {
        get { return m_enableListening; }
        set { m_enableListening = value; }
    }

    void Start()
    {
        if (!m_enableListening)
        {
            return;
        }
        m_DictationRecognizer = new DictationRecognizer();

        m_DictationRecognizer.DictationResult += VoiceResult;
        m_DictationRecognizer.DictationHypothesis += VoiceHypothesis;

        m_DictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
        };

        m_DictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };

        m_DictationRecognizer.Start();
    }

    private void VoiceResult(string text, ConfidenceLevel confidence) 
    {
        Debug.Log("Reults: " + text);
        m_Recognitions.text = "";
        m_Recognitions.text = text;
    }

    private void VoiceHypothesis(string text) 
    {
        // Debug.Log("Continueing Hypothesis: " + text);
        m_Hypotheses.text = "";
        m_Hypotheses.text = text;
    }

    #region IMixedRealitySpeechHandler Implementation

    void IMixedRealitySpeechHandler.OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        Debug.Log(eventData.Command.Keyword.ToLower());
    }
    #endregion
}