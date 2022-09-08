using UnityEngine;
using MoreMountains.Tools;
using UnityEditor;
using UnityEngine.UI;

namespace MoreMountains.Tools
{	
	[CustomEditor(typeof(MMXPBar),true)]
	/// <summary>
	/// Custom editor for XP bars (mostly a switch for prefab based / drawn bars)
	/// </summary>
	public class XPBarEditor : Editor 
	{
		public MMXPBar XPBarTarget 
		{ 
			get 
			{ 
				return (MMXPBar)target;
			}
		} 

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			if (XPBarTarget.XPBarType == MMXPBar.XPBarTypes.Prefab)
			{
				Editor.DrawPropertiesExcluding(serializedObject, new string[] {"Size","BackgroundPadding", "SortingLayerName", "InitialRotationAngles", "ForegroundColor", "DelayedColor", "BorderColor", "BackgroundColor", "Delay", "LerpFrontBar", "LerpFrontBarSpeed", "LerpDelayedBar", "LerpDelayedBarSpeed", "BumpScaleOnChange", "BumpDuration", "BumpAnimationCurve" });
            }

			if (XPBarTarget.XPBarType == MMXPBar.XPBarTypes.Drawn)
			{
				Editor.DrawPropertiesExcluding(serializedObject, new string[] {"XPBarPrefab" });
			}

			serializedObject.ApplyModifiedProperties();
		}

	}
}