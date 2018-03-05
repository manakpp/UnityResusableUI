using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Wraps UIButtonInstance which wraps Unity buttons.
/// Attempts to create a standard method to use buttons across the codebase.
/// The public properties and methods should be the only things needed ever
/// </summary>
[ExecuteInEditMode]
public class UIButton : MonoBehaviour 
{
	// TODO: Put this in a resource manager type class
	public UIButtonInstance m_buttonPrefab = null;

	private UIButtonInstance m_button = null;

	private List<System.Action> m_clickListeners = new List<System.Action>();

	public bool IsInteractable { get { return m_button.IsInteractable; } set { m_button.IsInteractable = value; }}
	public string TextTitle { get { return m_button.TextTitle; } set { m_button.TextTitle = value; } }
	public string TexSubtitle { get { return m_button.TextSubtitle; } set { m_button.TextSubtitle = value; } }

	private void Awake()
	{
		// Create the actual button instance
		if (Application.isPlaying)
		{
			if (m_button == null)
			{
				// This button is not hidden in editor because you may need it..
				// for debugging
				m_button = Instantiate<UIButtonInstance>(m_buttonPrefab, transform);

				m_button.TextTitle = "Runtime button";
			}
		}
		else
		{
#if UNITY_EDITOR
			// Create a preview version of the button instance
			if (m_button == null)
			{
				m_button = Instantiate<UIButtonInstance>(m_buttonPrefab, transform);

				// These flags will: 
				// 1) hide it
				// 2) ensure it doesn't save
				// 3) destroy it when running
				m_button.gameObject.hideFlags = 
					HideFlags.DontSave | 
					HideFlags.HideInHierarchy | 
					HideFlags.HideInInspector;

				m_button.TextTitle = "Editor button";
			}
#endif
		}
	}

	private void OnDestroy()
	{
#if UNITY_EDITOR
		// Only need to clean up in editor because run time will clean itself
		if (!Application.isPlaying)
		{
			if (m_button != null)
			{
				DestroyImmediate(m_button.gameObject);
			}
		}
#endif
	}

	private void OnEnable()
	{
		m_button.AddClickListener(OnClick);
	}

	private void OnDisable()
	{
		m_button.RemoveClickListener(OnClick);
	}

	private void OnClick()
	{
		// Ideally button click events should be handled via scripts
		// TODO: Possibly expose onClick so these can be hooked up like Unity buttons
		UnityEngine.Debug.Log("Clicked");

		for (int i = 0; i < m_clickListeners.Count; ++i)
		{
			m_clickListeners[i].Invoke();
		}
	}

	public void AddClickListener(System.Action action)
	{
		// Implemented with a list so that is consistent with Unity buttons...
		// which will/would make it easier to refactor existing code bases
		// Obviously a cost here for these lists
		m_clickListeners.Add(action);
	}

	public void RemoveClickListener(System.Action action)
	{
		m_clickListeners.Remove(action);
	}
}
