using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewChapter", menuName = "Chapter", order = 0)]
public sealed class Chapter : ScriptableObject
{
    private Chapter _previousChapter;
    public Sprite backgroundImage;
    public AudioClip backGroundMusic;
    public Message[] messages;
    public Branch[] nextBranches = Array.Empty<Branch>();
    public string EndingName;
    public Chapter PreviousChapter
    {
        get
        {
            if (ReferenceEquals(_previousChapter, null))
            {
                return this;
            }

            return _previousChapter;
        }
        set
        {
            _previousChapter = value;
        }
    }
}
