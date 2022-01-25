using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuBase
{
	public List<MenuOption> Options { get; set; }

	public void RenderMenu()
	{
		var canvas = GameObject.FindGameObjectWithTag("SettingsMenu");

		foreach(var option in Options)
		{
			var obj = new GameObject()
			{
				name = option.Name
			};

			var label = new GameObject()
			{
				name = $"{option.Name}_Label"
			};

			var text = label.AddComponent<TextMeshPro>();
			text.text = option.Name;

			var opt = new GameObject()
			{
				name = $"{option.Name}_Option"
			};

			switch(option.OptionType)
			{
				case MenuOptionType.Dropdown:
					opt.AddComponent<TMP_Dropdown>();
					break;

				case MenuOptionType.Select:
					opt.AddComponent<Toggle>();
					break;

				case MenuOptionType.Button:
					opt.AddComponent<Button>();
					break;
			}

			label.transform.SetParent(obj.transform, false);
			opt.transform.SetParent(obj.transform, false);
			obj.transform.SetParent(canvas.transform, false);
		}
	}
}
