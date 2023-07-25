using UnityEngine;
using System;
using System.Collections;

public class DemoGUI : MonoBehaviour
{
	public ResizableUI Resize1;
	public DraggableUI Drag1;
	public BringToFrontUI Bring1;

	public GUISkin guiSkin;

	bool win1;

	void OnGUI ()
	{
		var skin = GUI.skin;
		GUI.skin = guiSkin;

		GUILayout.Label("<B>Thank you for downloading \"SimpleWindowUI\"</B>");

		var p1 = Resize1.rect.position;
		var s1 = Resize1.rect.sizeDelta;
		win1 = GUILayout.Toggle(win1,string.Format("Window 1 : ({0},{1}) ({2},{3})",p1.x,p1.y,s1.x,s1.y));
		if(win1)
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("edgeWidth");
				float f;
				if(float.TryParse(GUILayout.TextField(Resize1.edgeWidth.ToString()),out f))
					Resize1.edgeWidth = f;
			}
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("minSize");
				float x,y;
				if(float.TryParse(GUILayout.TextField(Resize1.minSize.x.ToString()),out x))
					Resize1.minSize.x = x;
				if(float.TryParse(GUILayout.TextField(Resize1.minSize.y.ToString()),out y))
					Resize1.minSize.y = y;
			}
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("maxSize");
				float x,y;
				if(float.TryParse(GUILayout.TextField(Resize1.maxSize.x.ToString()),out x))
					Resize1.maxSize.x = x;
				if(float.TryParse(GUILayout.TextField(Resize1.maxSize.y.ToString()),out y))
					Resize1.maxSize.y = y;
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("MinimizeMaximizePivot");
				Resize1.MinimizeMaximizePivot.x = GUILayout.HorizontalSlider(Resize1.MinimizeMaximizePivot.x,0f,1f);
				Resize1.MinimizeMaximizePivot.y = GUILayout.HorizontalSlider(Resize1.MinimizeMaximizePivot.y,0f,1f);
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("Resize_BlockScreenOut");
				Resize1.BlockScreenOut = GUILayout.Toggle(Resize1.BlockScreenOut,"");
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("padding");
				float p;
				if(float.TryParse(GUILayout.TextField(Resize1.padding.ToString()),out p))
					Resize1.padding = p;
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("Drag_BlockScreenOut");
				Drag1.BlockScreenOut = GUILayout.Toggle(Drag1.BlockScreenOut,"");
			}
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("padding");
				float p;
				if(float.TryParse(GUILayout.TextField(Drag1.padding.ToString()),out p))
					Drag1.padding = p;
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			{
				GUILayout.Label("BringTFront_Enable");
				Bring1.enabled = GUILayout.Toggle(Bring1.enabled,"");
			}
			GUILayout.EndHorizontal();
		}

		GUI.skin = skin;
	}
}
