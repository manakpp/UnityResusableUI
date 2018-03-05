using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Wraps Unity button
/// </summary>
public class UIButtonInstance : MonoBehaviour 
{
	[SerializeField]
	private Button m_button = null;

	[SerializeField]
	private Text m_textTitle = null;

	[SerializeField]
	private Text m_textSubtitle = null;

	public bool IsInteractable { get { return m_button.interactable; } set { m_button.interactable = value; }}
	public string TextTitle { get { return m_textTitle.text; } set { m_textTitle.text = value; }}
	public string TextSubtitle { get { return m_textSubtitle.text; } set { m_textSubtitle.text = value; } }

	public void AddClickListener(UnityEngine.Events.UnityAction action)
	{
		m_button.onClick.AddListener(action);
	}

	public void RemoveClickListener(UnityEngine.Events.UnityAction action)
	{
		m_button.onClick.RemoveListener(action);
	}
}
