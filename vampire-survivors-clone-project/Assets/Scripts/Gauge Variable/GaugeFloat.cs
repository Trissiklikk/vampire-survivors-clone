using UnityEngine;
using System;

[Serializable]
public struct GaugeFloat
{
    [SerializeField]
    private float m_current;

    /// <summary>
    /// Get or set the current value of the gauge float.
    /// </summary>
    public float Current
    {
        get
        {
            return m_current;
        }
        set
        {
            m_current = value < 0 ? 0 : value;
            UpdateValue();
        }
    }

    [SerializeField]
    private float m_max;

    /// <summary>
    /// Get or set the max value of the gauge float.
    /// </summary>
    public float Max
    {
        get
        {
            return m_max;
        }
        set
        {
            m_max = value < 0 ? 0 : value;
            UpdateValue();
        }
    }

    /// <summary>
    /// Get or set the precent of the gauge float.
    /// </summary>
    public float Precent
    {
        get
        {
            return m_current / m_max;
        }
        set
        {
            m_current = m_max * value;
            UpdateValue();
        }
    }

    /// <summary>
    /// Update the current value of the gauge float when the value is modified.
    /// </summary>
    private void UpdateValue()
    {
        m_current = m_current > m_max ? m_max : m_current;
    }
}