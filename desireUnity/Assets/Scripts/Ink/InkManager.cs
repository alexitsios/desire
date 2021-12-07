using Fungus;
using Ink.Runtime;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class InkManager : MonoBehaviour
{
    public TextAsset _textAsset;
    public SayDialog _sayDialog;
	public Stage _stage;

	private Story _story;
	private CanvasManager _canvasManager;
	private PlayerInteraction _interaction;
	private GameManager _gameManager;

	public void StartInkManager()
	{
		_interaction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>();
		_story = new Story(_textAsset.text);
		_canvasManager = GetComponent<CanvasManager>();
		_gameManager = GetComponent<GameManager>();
	}

	public IEnumerator InkJumpTo(string path)
	{
		_story.ChoosePathString(path);

		var block = _story.ContinueMaximally().Split('\n');
		
		foreach(string line in block)
		{
			if(line.Equals(""))
				continue;

			yield return StartCoroutine(ProcessPath(line));
		}

		if(_stage != null)
		{
			var characters = _stage.CharactersOnStage.Count;

			for(int i = 0; i < characters; i++)
			{
				_stage.Hide(_stage.CharactersOnStage[0]);
			}
		}

		_interaction.FinishInteraction();
	}

	private IEnumerator ProcessPath(string line)
	{
		if(line.StartsWith(">>"))
		{
			yield return StartCoroutine(ProcessCommand(line));
		}
		else
		{
			var block = Regex.Split(line, "[\"]+");
			var characterAttributes = block[0].Split(' ');
			Character character = GameObject.Find(characterAttributes[0] + "_Character").GetComponent<Character>();

			if(_stage != null)
			{
				foreach(var currentChar in _stage.CharactersOnStage)
				{
					_stage.SetDimmed(currentChar, currentChar != character);
				}

				_stage.Show(character, character.Portraits[0].name, characterAttributes[1]);
			}

			_sayDialog.SetCharacter(character);
			yield return StartCoroutine(_sayDialog.DoSay(block[1], true, true, true, false, false, null, null));
		}
	}

	private IEnumerator ProcessCommand(string command)
	{
		var commandList = command.Substring(2).Trim().Split(' ');;

		switch(commandList[0])
		{
			case "fadein":
				yield return StartCoroutine(_canvasManager.Fade(1, int.Parse(commandList[1])));
				break;

			case "fadeout":
				yield return StartCoroutine(_canvasManager.Fade(0, int.Parse(commandList[1])));
				break;

			case "bgchange":
				break;

			case "moveto":
				var actor = GameObject.Find(commandList[1]).GetComponent<NPCMovement>();
				var destination = GameObject.Find(commandList[2]).transform.position;

				actor.MoveTo(destination);

				break;

			case "additem":
				var itemType = (ItemType) Enum.Parse(typeof(ItemType), commandList[1]);
				var item = _gameManager.GetItemProperties(itemType);
				GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().AddItem(item);

				break;
		}
	}
}
