using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace AssetFinder
{
	public class SearchResult
	{
		private static GUIStyle m_ListRowStyle;
		private static GUIStyle m_ListRowStyleSelected;
		private static GUIStyle m_ListIconStyle;
		private static GUIStyle m_AssetLabelStyle;
		private static Texture2D m_RowNormalTexture;
		private static Texture2D m_RowSelectedTexture;

		private readonly GUIContent m_HighlightedAssetNameContent;
		private readonly Texture m_Icon;
		private readonly GUIContent m_AssetPathContent;
		private Object m_Asset;

		public Rect Bounds { get; private set; }

		public SearchResult(string assetPath, string search)
		{
			m_AssetPathContent = new GUIContent(assetPath, assetPath);
			// Highlight part of the asset name based on the search string.
			string assetNameHighlighted = Path.GetFileNameWithoutExtension(assetPath);
			int index = assetNameHighlighted.IndexOf(search, System.StringComparison.OrdinalIgnoreCase);
			if (index != -1)
			{
				string subString = assetNameHighlighted.Substring(index, search.Length);
				StringBuilder builder = new StringBuilder(assetNameHighlighted);
				builder.Remove(index, search.Length);
				builder.Insert(index, string.Format("<color=#{0}>{1}</color>",
					EditorGUIUtility.isProSkin ? "4bffa8" : "c900a6", subString));
				assetNameHighlighted = builder.ToString();
			}
			m_HighlightedAssetNameContent = new GUIContent(assetNameHighlighted, assetPath);
			m_Icon = AssetDatabase.GetCachedIcon(assetPath);
		}

		public void Draw(Rect rect, bool selected)
		{
			Bounds = rect;
			GUI.BeginGroup(rect, selected ? m_ListRowStyleSelected : m_ListRowStyle);
			if (AssetFinderPrefs.DrawSmall)
			{
				DrawSmall(rect);
			}
			else
			{
				DrawNormal(rect);
			}
			GUI.EndGroup();
		}

		private void DrawNormal(Rect rect)
		{
			const float padding = 4;
			GUI.Box(new Rect(padding, padding, 32, 32), m_Icon, m_ListIconStyle);
			const float labelX = 40;
			GUI.Label(new Rect(labelX, 4, rect.width - labelX - padding, 20), m_HighlightedAssetNameContent, m_AssetLabelStyle);
			GUI.Label(new Rect(labelX, 20, rect.width - labelX - padding, 20), m_AssetPathContent);
		}

		private void DrawSmall(Rect rect)
		{
			const float padding = 4;
			GUI.Box(new Rect(padding, padding, 16, 16), m_Icon, m_ListIconStyle);
			const float labelX = 24;
			GUI.Label(new Rect(labelX, 4, rect.width - labelX - padding, 20), m_HighlightedAssetNameContent, m_AssetLabelStyle);
		}

		public Object GetAsset()
		{
			if (m_Asset == null)
			{
				m_Asset = AssetDatabase.LoadAssetAtPath<Object>(m_AssetPathContent.text);
			}
			return m_Asset;
		}

		public static void CreateStyles()
		{
			// Due to some inexplicable bug the textures need to be recreated every frame,
			// or at least once.
			CreateRowTextures();

			// Row
			m_ListRowStyle = new GUIStyle(EditorStyles.helpBox);
			m_ListRowStyle.normal.background = m_RowNormalTexture;
			const int padding = 0;
			m_ListRowStyle.padding = new RectOffset(padding, padding, padding, padding);
			m_ListRowStyle.border = new RectOffset();
			m_ListRowStyle.margin = new RectOffset(padding, padding, 0, padding);

			m_ListRowStyleSelected = new GUIStyle(m_ListRowStyle);
			m_ListRowStyleSelected.normal.background = m_RowSelectedTexture;

			// Icon
			m_ListIconStyle = new GUIStyle(EditorStyles.helpBox);
			m_ListIconStyle.padding = new RectOffset();
			m_ListIconStyle.margin = new RectOffset();
			m_ListIconStyle.imagePosition = ImagePosition.ImageOnly;
			m_ListIconStyle.normal.background = null;

			// Label
			m_AssetLabelStyle = new GUIStyle(EditorStyles.boldLabel);
			m_AssetLabelStyle.richText = true;
		}

		private static void CreateRowTextures()
		{
			m_RowNormalTexture = new Texture2D(1, 1);
			Color normalColor = EditorGUIUtility.isProSkin
				? new Color(0.25f, 0.25f, 0.25f)
				: new Color(0.7f, 0.7f, 0.7f);
			m_RowNormalTexture.SetPixels(new[] {normalColor});

			m_RowSelectedTexture = new Texture2D(1, 1);
			Color selectedColor = EditorGUIUtility.isProSkin
				? new Color(0.15f, 0.3f, 0.4f)
				: new Color(0.55f, 0.7f, 0.8f);
			m_RowSelectedTexture.SetPixels(new[] {selectedColor});
		}
	}
}
