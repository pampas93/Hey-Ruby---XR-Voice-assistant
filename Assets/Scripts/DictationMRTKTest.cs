using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using TMPro;

public class DictationMRTKTest : BaseInputHandler, IMixedRealityDictationHandler, IMixedRealitySpeechHandler
{

    [SerializeField]
    private TMP_Text m_Hypotheses;
    [SerializeField]
    private TMP_Text m_Recognitions;
    [SerializeField]
    private bool startRecordingOnStart = false;

    private IMixedRealityDictationSystem dictationSystem;

    protected override void Start()
    {
        base.Start();

        dictationSystem = (InputSystem as IMixedRealityDataProviderAccess)?.GetDataProvider<IMixedRealityDictationSystem>();
        Debug.Assert(dictationSystem != null, "No dictation system found. Add a dictation system like 'Windows Dictation Input Provider' to the Data Providers in the Input System profile");

        if (startRecordingOnStart)
        {
            StartRecording();
        }
    }

    public void StartRecording()
    {
        if (dictationSystem != null)
        {
            dictationSystem.StartRecording(gameObject, 2);
        }
    }

    public void StopRecording()
    {
        if (dictationSystem != null)
        {
            dictationSystem.StopRecording();
        }
    }


    #region IMixedRealityDictationHandler implementation

    void IMixedRealityDictationHandler.OnDictationHypothesis(DictationEventData eventData)
    {
        // Debug.Log(eventData.DictationResult);
        m_Hypotheses.text = "";
        m_Hypotheses.text = eventData.DictationResult;
    }

    void IMixedRealityDictationHandler.OnDictationResult(DictationEventData eventData)
    {
        Debug.Log("Reults: " + eventData.DictationResult);
        m_Recognitions.text = "";
        m_Recognitions.text = eventData.DictationResult;
    }

    void IMixedRealityDictationHandler.OnDictationComplete(DictationEventData eventData)
    {
        
    }

    void IMixedRealityDictationHandler.OnDictationError(DictationEventData eventData)
    {
        
    }
    #endregion

    #region IMixedRealitySpeechHandler Implementation

    void IMixedRealitySpeechHandler.OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        Debug.Log(eventData.Command.Keyword.ToLower());
    }

    #endregion
}