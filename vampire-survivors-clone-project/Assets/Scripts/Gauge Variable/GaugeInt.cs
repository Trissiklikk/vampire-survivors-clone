using UnityEngine;
using System;

[Serializable]
public struct GaugeInt
{
    [SerializeField]
    private int m_current;

    /// <summary>
    /// Get or set the current value of the gauge int.
    /// </summary>
    public int Current
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
    private int m_max;

    /// <summary>
    /// Get or set the max value of the gauge int.
    /// </summary>
    public int Max
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
    /// Get or set the precent of the gauge int.
    /// </summary>
    public float Precent
    {
        get
        {
            return (float)m_current / (float)m_max;
        }
        set
        {
            m_current = (int)(m_max * value);
            UpdateValue();
        }
    }

    /// <summary>
    /// Update the current value of the gauge int when the value is modified.
    /// </summary>
    private void UpdateValue()
    {
        m_current = m_current > m_max ? m_max : m_current;
    }
}