using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class Brain : MonoBehaviour
{
    public int DNALength = 1;
    public float timeAlive;
    public DNA dna;

    private ThirdPersonCharacter m_Character;
    private Vector3 m_jump;
    private bool m_Jump;
    bool alive = true;
    
    private void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.tag == "dead")
        {
            alive = false;
        }
    }

    public void Init()
    {
        // Initialise DNA
        // 0 Forward
        // 1 Back
        // 2 Left
        // 3 Right
        // 4 Jump
        // 5 Crouch

        dna = new DNA(DNALength, 6);
        m_Character = GetComponent<ThirdPersonCharacter>();
        timeAlive = 0;
        alive = true;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // Read DNA
        float h = 0;
        float v = 0;
        bool crouch = false;
        if (dna.GetGene(0) == 1) v = -1;
        else if (dna.GetGene(0) == 2) h = -1;
        else if (dna.GetGene(0) == 3) h = 1;
        else if (dna.GetGene(0) == 4) m_jump = true;
        else if (dna.GetGene(0) == 5) crouch = true;

        m_Move = v * Vector3.forward + h h* Vector3.right;
        m_Character.Move(m_Move, crouch, m_Jump);
        m_jump = false;
        if(alive)
            timeAlive += Time.deltaTime;
    }
}
