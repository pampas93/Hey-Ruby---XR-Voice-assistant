using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using TMPro;

public class DictationTest : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_Hypotheses;
    [SerializeField]
    private TMP_Text m_Recognitions;
    
    private DictationRecognizer m_DictationRecognizer;

    void Start()
    {
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
}