using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cat.Common;
using Cat.PostProcessing;
using Cat.CommonEditor;

namespace Cat.PostProcessingEditor {

	[CatPostProcessingEditorAttribute(typeof(CatBloom))]
	public class CatBloomEditor : CatPostProcessingEditorBase {

		public override void OnInspectorGUI(IEnumerable<AttributedProperty> properties) {
			
			serializedObject.Update();
			foreach (var property in properties) {
				PropertyField(property);
			}
			serializedObject.ApplyModifiedProperties();
			EditorGUILayout.Space();
			DrawResponseFunction();
			EditorGUILayout.Space();
		}
			
		void DrawResponseFunction() {
			var settings = target as CatBloom;
			var minLuminance = settings.minLuminance;
			var kneeStrength = settings.kneeStrength;
			var range = new Vector2(5f, 2f);

			var rect = GUILayoutUtility.GetRect(128, 80);
			var graph = new Graph(rect, new Rect(Vector2.zero, range));

			// Background
			graph.DrawRect(graph.m_Range, 0.1f, 0.4f);
			// Grid
			graph.DrawGrid(0.4f);
			// Label
			//Handles.Label(
			//	PointInRect(0, m_RangeY) + Vector3.right,
			//	"Brightness Response (linear)", EditorStyles.miniLabel
			//);

			// Threshold line
			graph.DrawLine(new Vector2(minLuminance, graph.m_Range.yMin), new Vector2(minLuminance, graph.m_Range.yMax), 0.85f);
			// Graph
			//graph.DrawFunction(x => x * Mathf.Pow(Mathf.Max(0, x - minLuminance) / (x + 1e-1f), kneeStrength + 1), 0.1f);
			graph.DrawFunction(x => Mathf.Pow(Mathf.Max(0, x-minLuminance), kneeStrength*4 + 1) / Mathf.Pow(Mathf.Max(0, x-minLuminance) + 1e-1f, kneeStrength*4), 0.90f);
		}
	}
}
