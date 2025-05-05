using Game.Core;
using UnityEditor;
using UnityEngine;

namespace Game.Presentation
{
    public class LayoutPreview : EditorWindow
    {
        private int seed = 1234;
        private int width = 64;
        private int height = 64;
        private UnityEngine.Vector2 scroll;
        private Texture2D previewTexture;

        private ILayout layoutGenerator = new DepthFirstLayout();

        [MenuItem("Tools/Layout Preview")]
        public static void ShowWindow()
        {
            GetWindow<LayoutPreview>("Layout Preview");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Layout Parameters", EditorStyles.boldLabel);

            // Horizontal group for seed input + randomize button
            EditorGUILayout.BeginHorizontal();
            seed = EditorGUILayout.IntField("Seed", seed);
            if (GUILayout.Button("🎲", GUILayout.Width(30)))
            {
                seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
                GUI.FocusControl(null); // Unfocus input field to update value visually
            }
            EditorGUILayout.EndHorizontal();

            width = EditorGUILayout.IntField("Width", width);
            height = EditorGUILayout.IntField("Height", height);

            if (GUILayout.Button("Generate"))
            {
                GenerateLayout();
            }

            if (previewTexture != null)
            {
                scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.Height(520)); // Slightly taller for padding
                float drawWidth = 512f;
                float drawHeight = 512f;

                Rect rect = GUILayoutUtility.GetRect(drawWidth, drawHeight, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));
                EditorGUI.DrawPreviewTexture(rect, previewTexture, null, ScaleMode.ScaleToFit);
                EditorGUILayout.EndScrollView();
            }
        }

        private void GenerateLayout()
        {
            bool[,] layout = layoutGenerator.Generate(seed, width, height);

            previewTexture = new Texture2D(layout.GetLength(0), layout.GetLength(1));
            for (int y = 0; y < layout.GetLength(1); y++)
            {
                for (int x = 0; x < layout.GetLength(0); x++)
                {
                    bool wall = !layout[x, y];
                    previewTexture.SetPixel(x, layout.GetLength(1) - y - 1, wall ? Color.black : Color.white);
                }
            }

            previewTexture.filterMode = FilterMode.Point;
            previewTexture.wrapMode = TextureWrapMode.Clamp;
            previewTexture.Apply();
        }
    }
}
