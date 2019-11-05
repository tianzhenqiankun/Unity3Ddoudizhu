using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Sound:MonoSingleton<Sound>
{
    AudioSource m_Bg;
    AudioSource m_Effect;
    public string Resourcedir = "";

   protected override void Awake()
    {
        base.Awake();
        m_Bg = gameObject.AddComponent<AudioSource>();
        m_Bg.loop = true;
        m_Bg.playOnAwake = false;
        m_Bg.volume = 0.5f;

        m_Effect = gameObject.AddComponent<AudioSource>();
        
    }

    public void PlayBG(string name)
    {
        string oldName;
        if (m_Bg.clip == null)
            oldName = "";
        else
            oldName = m_Bg.clip.name;
        if (oldName!=name)
        {
            AudioClip clip = Resources.Load<AudioClip>(Resourcedir + "/" + name);
            if (clip!=null)
            {
                m_Bg.clip = clip;
                m_Bg.Play();
            }
        }
    }

    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(Resourcedir + "/" + name);
        m_Effect.PlayOneShot(clip);
    }
}

