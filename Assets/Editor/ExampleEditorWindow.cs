using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ExampleEditorWindow : EditorWindow
{
    [MenuItem("Example/Open _%#T")]
    public static void ShowWindow()
    {
        // Opens the window, otherwise focuses it if it's already open.
        var window = GetWindow<ExampleEditorWindow>();

        // Adds a title to the window.
        window.titleContent = new GUIContent("ExampleEditorWindow");

        // Sets a minimum size to the window.
        window.minSize = new Vector2(500, 500);
    }

    private void CreateGUI()
    {
        // Reference to the root of the window.
        var root = rootVisualElement;

        // Creates our button and sets its Text property.
        var myButton = new Button() { text = "My Button" };

        // Give it some style.
        myButton.style.width = 160;
        myButton.style.height = 30;
        myButton.style.top = 50;
        myButton.style.left = 30;

        // Adds it to the root.
        root.Add(myButton);
    }

    // Code slightly modified from https://docs.unity3d.com/ScriptReference/GL.html below

    // When added to an object, draws colored rays from the
    // transform position.
    public int lineCount = 100;
    public float radius = 3.0f;

    static Material lineMaterial;
    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }

    public void OnGUI()
    {
        CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);

        GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
        // GL.MultMatrix(transform.localToWorldMatrix);

        // Draw lines
        GL.Begin(GL.LINES);
        for (int i = 0; i < lineCount; ++i)
        {
            float a = i / (float)lineCount;
            float angle = a * Mathf.PI * 2;
            // Vertex colors change from red to green
            GL.Color(new Color(a, 1 - a, 0, 0.8F));
            // One vertex at transform position
            GL.Vertex3(0, 0, 0);
            // Another vertex at edge of circle
            GL.Vertex3(100, 100, 0);
        }
        GL.End();
        GL.PopMatrix();
    }
}
