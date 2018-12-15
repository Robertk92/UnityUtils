using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AssetFinder
{
	public class AssetFinder : EditorWindow
	{
		private static AssetFinder m_CurrentWindow;

		private string m_SearchString = string.Empty;
		private int m_Selected;
		private readonly List<SearchResult> m_SearchResults = new List<SearchResult>();
		private Vector2 m_ListScrollPosition;
		private bool m_SelectionDirty;
		private bool m_DownOrUpPressedThisFrame;
		private int m_CurrentTextCursorIndex;
		private Texture2D m_OptionsIcon;

		private const float OptionsButtonWidth = 18;
		private const float SpaceToResultsList = 28;

		[MenuItem("Tools/m_Asset Finder %,")] // CTRL + ,
		private static void OpenAssetFinder()
		{
			if (m_CurrentWindow == null)
			{
				m_CurrentWindow = CreateInstance<AssetFinder>();
				m_CurrentWindow.titleContent = new GUIContent("Asset Finder");
				m_CurrentWindow.ShowUtility();
			}
		}

		private void CloseAssetFinder()
		{
			Close();
			GUIUtility.ExitGUI();
			m_CurrentWindow = null;
		}

		private void OnGUI()
		{
			SearchResult.CreateStyles();
			UpdateInputEvents();
			UpdateSearch();
			DrawOptions();
			UpdateAssetList();
			CheckClick();

			if (m_SearchResults.Count > 0)
			{
				GUILayout.FlexibleSpace();
				string filterWarning = AssetFinderPrefs.HasFilters()
					? string.Format(" <color={0}>Filters are enabled.</color>", EditorGUIUtility.isProSkin ? "orange" : "#FF4500")
					: string.Empty;
				GUIStyle labelStyle = new GUIStyle(EditorStyles.label);
				labelStyle.richText = true;
				GUILayout.Label(string.Format("{0} assets found.{1}", m_SearchResults.Count, filterWarning), labelStyle);
			}
		}

		// General input handling.
		private void UpdateInputEvents()
		{
			m_DownOrUpPressedThisFrame = false;
			Event e = Event.current;
			if (e.isKey && e.type == EventType.KeyDown)
			{
				KeyCode key = e.keyCode;
				if (key == KeyCode.DownArrow)
				{
					if (m_Selected < m_SearchResults.Count - 1)
					{
						m_Selected++;
						OnSelectionChanged();
					}
					OnDownOrUpPressed();
				}
				else if (key == KeyCode.UpArrow)
				{
					if (m_Selected > 0)
					{
						m_Selected--;
						OnSelectionChanged();
					}
					OnDownOrUpPressed();
				}
				else if ((key == KeyCode.KeypadEnter || key == KeyCode.Return))
				{
					if (m_SearchResults.Count > m_Selected)
					{
						AssetDatabase.OpenAsset(m_SearchResults[m_Selected].GetAsset());
					}
					CloseAssetFinder();
				}
				else if (key == KeyCode.Escape)
				{
					CloseAssetFinder();
				}
			}
		}

		// We want to use down or up to navigate the search results. However, those keys also change the
		// cursor index in the text field. We "revert" that behavior like this.
		private void OnDownOrUpPressed()
		{
			TextEditor txt = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
			m_CurrentTextCursorIndex = txt.cursorIndex;
			m_DownOrUpPressedThisFrame = true;
		}

		// Check if the user clicked any search result.
		// If yes, we open that asset.
		private void CheckClick()
		{
			Event e = Event.current;
			if (e.isMouse && e.type == EventType.MouseUp && e.mousePosition.y > SpaceToResultsList)
			{
				Vector2 mousePosInList = e.mousePosition + m_ListScrollPosition;
				mousePosInList.y -= SpaceToResultsList;
				foreach (SearchResult searchResult in m_SearchResults)
				{
					if (searchResult.Bounds.Contains(mousePosInList))
					{
						AssetDatabase.OpenAsset(searchResult.GetAsset());
						CloseAssetFinder();
						break;
					}
				}
			}
		}

		private void OnSelectionChanged()
		{
			m_SelectionDirty = true;
		}

		private void UpdateSearch()
		{
			const string searchFieldName = "SearchField";
			GUI.SetNextControlName(searchFieldName);
			float width = position.width - OptionsButtonWidth - 15;
			string search = GUI.TextField(new Rect(5, 5, width, 18), m_SearchString);
			EditorGUI.FocusTextInControl(searchFieldName);

			if (m_DownOrUpPressedThisFrame)
			{
				TextEditor txt = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
				txt.cursorIndex = m_CurrentTextCursorIndex;
				txt.SelectNone();
			}

			if (search != m_SearchString)
			{
				m_SearchString = search;
				OnSearchChanged(search);
			}
		}

		private void OnSearchChanged(string searchString)
		{
			m_Selected = 0;
			m_SearchResults.Clear();
			if (searchString.Length > 0)
			{
				string searchWithFilter = string.Format("{0} {1}", AssetFinderPrefs.GetTypeFilterString(), searchString);
				string[] searchFolders = AssetFinderPrefs.SearchFolders;
				bool hasSearchFolders = searchFolders != null && searchFolders.Length > 0;
				string[] assetGUIDs = hasSearchFolders ?
					AssetDatabase.FindAssets(searchWithFilter, searchFolders) :
					AssetDatabase.FindAssets(searchWithFilter);
				foreach (string GUID in assetGUIDs)
				{
					string assetPath = AssetDatabase.GUIDToAssetPath(GUID);
					if (!AssetDatabase.IsValidFolder(assetPath))
					{
						if (AssetFinderPrefs.MaxResults != -1 && m_SearchResults.Count >= AssetFinderPrefs.MaxResults)
						{
							break;
						}
						m_SearchResults.Add(new SearchResult(assetPath, searchString));
					}
				}
			}
		}

		private void UpdateAssetList()
		{
			float rowHeight = AssetFinderPrefs.DrawSmall ? 26 : 40;
			const float scrollViewY = 28;
			const float spaceBottom = 22;
			const float padding = 5;

			float scrollPositionHeight = position.height - scrollViewY - spaceBottom;
			float scrollPositionWidth = position.width - padding;
			float listHeight = m_SearchResults.Count * (rowHeight + padding) - padding;
			Rect scrollPosition = new Rect(padding, scrollViewY, scrollPositionWidth, scrollPositionHeight);

			bool hasScrolling = listHeight > scrollPositionHeight;
			float rowWidth = hasScrolling ? scrollPositionWidth - 14 - padding : scrollPositionWidth - padding;

			m_ListScrollPosition = GUI.BeginScrollView(scrollPosition, m_ListScrollPosition, new Rect(0, 0, rowWidth, listHeight));

			Rect searchResultRect = new Rect(0, 0, rowWidth, rowHeight);

			for (int i = 0; i < m_SearchResults.Count; ++i)
			{
				SearchResult searchResult = m_SearchResults[i];
				bool selected = i == m_Selected;
				bool inView = searchResultRect.y + rowHeight > m_ListScrollPosition.y &&
				              searchResultRect.y < scrollPositionHeight + m_ListScrollPosition.y;

				if (inView)
				{
					searchResult.Draw(searchResultRect, selected);
				}

				if (selected && m_SelectionDirty)
				{
					if (m_SelectionDirty)
					{
						m_SelectionDirty = false;
						if (!inView || searchResultRect.y + searchResultRect.height > scrollPositionHeight + m_ListScrollPosition.y)
						{
							m_ListScrollPosition.y = searchResultRect.y;
							Repaint();
						}

						EditorGUIUtility.PingObject(searchResult.GetAsset());
					}
				}

				searchResultRect.y += rowHeight + padding;
			}
			GUI.EndScrollView();
		}

		private void DrawOptions()
		{
			GUIStyle buttonStyle = new GUIStyle(EditorStyles.miniButton);
			buttonStyle.padding = new RectOffset(4, 4, 4, 4);
			Rect buttonRect = new Rect(position.width - OptionsButtonWidth - 5, 5, OptionsButtonWidth, 18);
			if (GUI.Button(buttonRect, new GUIContent(GetOptionsIcon(), "Options"), buttonStyle))
			{
				AssetFinderPrefs prefs = new AssetFinderPrefs(OnPrefsChanged);
				Rect popupPosition = new Rect(position.width - prefs.GetWindowSize().x - 5, SpaceToResultsList, 0, 0);
				PopupWindow.Show(popupPosition, prefs);
			}
		}

		private void OnPrefsChanged()
		{
			OnSearchChanged(m_SearchString);
		}

		private Texture2D GetOptionsIcon()
		{
			if (m_OptionsIcon == null)
			{
				string iconPath = string.Format("Assets/AssetFinder/Editor/Resources/{0}", EditorGUIUtility.isProSkin
					? "cog.png"
					: "cog_black.png");
				m_OptionsIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(iconPath);
			}
			return m_OptionsIcon;
		}
	}
}
