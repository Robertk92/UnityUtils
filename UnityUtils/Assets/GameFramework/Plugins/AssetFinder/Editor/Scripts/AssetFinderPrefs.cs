using System;
using System.Linq;
using UnityEngine;
using System.Text;
using UnityEditor;

namespace AssetFinder
{
	public class AssetFinderPrefs : PopupWindowContent
	{
		private class TypeFilter
		{
			public readonly string m_TypeName;
			public bool m_Filter;

			public TypeFilter(string typeName)
			{
				m_TypeName = typeName;
				m_Filter = false;
			}
		}

		// Type filters restrict the search results to certain type(s) of assets.
		private static readonly TypeFilter[] TypeFilters =
		{
			new TypeFilter("Script"),
			new TypeFilter("Prefab"),
			new TypeFilter("Scene"),
			new TypeFilter("Texture"),
			new TypeFilter("Material"),
			new TypeFilter("Shader"),
			new TypeFilter("AnimatorController"),
		};

		// Keys for storing data into EditorPrefs.
		private const string TypeFiltersKey = "AssetFinder.TypeFilters";
		private const string MaxResultsKey = "AssetFinder.MaxResults";
		private const string DrawSmallKey = "AssetFinder.DrawSmall";
		private const string SearchFoldersKey = "AssetFinder.SearchFolders";
		private const int DefaultMaxResults = 10000;

		// Max amount of search results to display in list (for performance).
		public static int MaxResults { get; private set; }
		// Toggle to draw smaller sized search results.
		public static bool DrawSmall { get; private set; }
		private static string _searchFoldersRaw;
		// Optional search folders
		public static string[] SearchFolders
		{
			get
			{
				if (!string.IsNullOrEmpty(_searchFoldersRaw))
				{
					string[] split = _searchFoldersRaw.Split(';');
					return split
						.Where(s => !string.IsNullOrEmpty(s))
						// By default, Unity won't look under the Assets folder.
						// This avoids the requirement to type Assets/FolderName each time.
						.Select(s => string.Format("Assets/{0}", s))
						.ToArray();
				}
				return null;
			}
		}

		private bool m_PrefsChanged;
		private readonly Action OnPrefsChanged;

		static AssetFinderPrefs()
		{
			if (!EditorPrefs.HasKey(TypeFiltersKey))
			{
				EditorPrefs.SetString(TypeFiltersKey, GetTypeFilterString());
			}
			else
			{
				string typeFiltersString = EditorPrefs.GetString(TypeFiltersKey);
				string[] typesToFilter = typeFiltersString.Split(new[] { "t:" }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string type in typesToFilter)
				{
					TypeFilter typeFilter = TypeFilters.First(t => t.m_TypeName == type.Trim());
					typeFilter.m_Filter = true;
				}
			}

			if (!EditorPrefs.HasKey(MaxResultsKey))
			{
				EditorPrefs.SetInt(MaxResultsKey, DefaultMaxResults);
			}
			MaxResults = EditorPrefs.GetInt(MaxResultsKey);

			if (!EditorPrefs.HasKey(DrawSmallKey))
			{
				EditorPrefs.SetBool(DrawSmallKey, false);
			}
			DrawSmall = EditorPrefs.GetBool(DrawSmallKey);
			_searchFoldersRaw = EditorPrefs.GetString(SearchFoldersKey);
		}

		public AssetFinderPrefs(Action prefsChanged)
		{
			OnPrefsChanged = prefsChanged;
		}

		private void DrawFilters()
		{
			GUIContent label = new GUIContent("Only look for", "Check the boxes if you only want to find results of 1 or more specific type(s).");
			GUILayout.Label(label, EditorStyles.boldLabel);

			foreach (TypeFilter typeFilter in TypeFilters)
			{
				bool filter = EditorGUILayout.ToggleLeft(typeFilter.m_TypeName, typeFilter.m_Filter);
				if (filter != typeFilter.m_Filter)
				{
					typeFilter.m_Filter = filter;
					EditorPrefs.SetString(TypeFiltersKey, GetTypeFilterString());
					m_PrefsChanged = true;
				}
			}
		}

		private void DrawMaxResults()
		{
			EditorGUILayout.BeginHorizontal();

			GUIContent label = new GUIContent("Max results",
				string.Format("The maximum number of results which will be displayed. For performance reasons, it is recommended to keep this under {0}.\nSet to -1 for no maximum.", DefaultMaxResults));
			EditorGUILayout.LabelField(label, EditorStyles.boldLabel, GUILayout.Width(80));
			int maxResults = EditorGUILayout.IntField(MaxResults);
			if (maxResults != MaxResults)
			{
				MaxResults = maxResults;
				EditorPrefs.SetInt(MaxResultsKey, maxResults);
				m_PrefsChanged = true;
			}

			EditorGUILayout.EndHorizontal();
		}

		private void DrawSearchFolders()
		{
			GUIContent label = new GUIContent("Search folders", "The folders to search inside, separated by a ;");
			EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
			string searchFolders = EditorGUILayout.TextField(_searchFoldersRaw);
			if (searchFolders != _searchFoldersRaw)
			{
				_searchFoldersRaw = searchFolders;
				EditorPrefs.SetString(SearchFoldersKey, searchFolders);
				m_PrefsChanged = true;
			}
		}

		// "Serialize" the list of active type filters to a string.
		public static string GetTypeFilterString()
		{
			StringBuilder builder = new StringBuilder();
			foreach (TypeFilter typeFilter in TypeFilters.Where(t => t.m_Filter))
			{
				builder.AppendFormat(" t:{0}", typeFilter.m_TypeName);
			}
			return builder.ToString().TrimStart();
		}

		public static bool HasFilters()
		{
			return TypeFilters.Any(t => t.m_Filter);
		}

		public override void OnGUI(Rect rect)
		{
			GUIStyle style = new GUIStyle();
			style.padding = new RectOffset(6, 6, 8, 6);
			EditorGUILayout.BeginVertical(style);
			{
				DrawFilters();
				GUILayout.Space(3);
				DrawMaxResults();
				GUILayout.Space(3);

				bool drawSmall = GUILayout.Toggle(DrawSmall, "Small UI");
				if (drawSmall != DrawSmall)
				{
					DrawSmall = drawSmall;
					EditorPrefs.SetBool(DrawSmallKey, DrawSmall);
				}

				GUILayout.Space(3);
				DrawSearchFolders();
			}
			EditorGUILayout.EndVertical();
		}

		public override Vector2 GetWindowSize()
		{
			return new Vector2(150, 115 + TypeFilters.Length * 18);
		}

		public override void OnClose()
		{
			if (m_PrefsChanged && OnPrefsChanged != null)
			{
				OnPrefsChanged();
			}
		}
	}
}
