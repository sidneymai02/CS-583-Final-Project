 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]

public class MusicPlayer : MonoBehaviour                                     

{
    public AudioClip[] MusicSections;
    // Music files which share the identifier 'Section' go in here. Example track: 'Adventure Inn Section 2.wav'.
    // In the editor, changing the 'Size' of the array increases/decreases the available slots. This is useful for removing unwanted music sections.
    public bool playIntro = true;
    // Disabling this in the Unity Inspector will skip Element 1 in MusicSections Array (by default this should be reserved for files with the 'Intro' identifier. Example track: 'Adventure Inn Section 1 Intro.wav').

    private AudioSource audioSource;
    // The audiosource is responsible for playing all of our 'MusicSections'.
    private int lastPlayed;
    // This keeps a log of the last played music section. Leave this alone unless you know what you are doing!
    private bool preloadBufferActive = true;
    // Necessary Preload buffer, leave this alone unless you know what you are doing!

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (MusicSections.Length == 0)
        {
            Debug.Log("Please add music segments!");                          
           // If you see this message, please check to see if you have music sections loaded!
        }
        else
        {
            StartCoroutine(PlayAudio());
        }
    }

        IEnumerator PlayAudio()                                             
    {
        if (preloadBufferActive)
        {
            audioSource.clip = MusicSections[0];
            audioSource.Play();
            yield return new WaitForSeconds(MusicSections[0].length);
            preloadBufferActive = false;
        }
        if (playIntro)
        // This is always 'Element 1' and should be assigned to audio files with the 'Intro' identifier. Example track: 'Adventure Inn Section 1 Intro.wav').
        {
            audioSource.clip = MusicSections[1];
            audioSource.Play();

            Debug.Log("Playing clip: " + MusicSections[1]);
            // Displays the currently playing clip in the editor console.
            yield return new WaitForSeconds(MusicSections[1].length);         
            // This tells us to wait for the duration of the audio clip before proceeding any further.
            playIntro = false;                                                
            // Ensures the Intro only plays once!
        }

        int section = Random.Range(2, MusicSections.Length);                  
        // Random number generator used to determine which music section from the array to play next.

        if (section != lastPlayed)                                            
        // Ensures we don't play the same section twice!
        {
            audioSource.clip = MusicSections[section];
            audioSource.Play();

            Debug.Log("Playing clip: " + MusicSections[section]);
            // Displays the currently playing clip in the editor console.
            yield return new WaitForSeconds(MusicSections[section].length);   
            // This tells us to wait for the duration of the audio clip before proceeding any further.
            lastPlayed = section;
            
            StartCoroutine(PlayAudio());                                      
            // This keeps us in our loop.
        }
        else
        {
            StartCoroutine(PlayAudio());                                      
            // This keeps us in our loop.
        }
    }
}
  

