using Fungus;
using Ink.Runtime;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class InkManager : MonoBehaviour
{
    public TextAsset _textAsset;
	public Character[] characters;
	public PlayerInteraction Interaction { get; set; }

	private Story _story;
	private CanvasManager _canvasManager;
	private PlayerMovement _playerMovement;
    private SayDialog _sayDialog;
	private Stage _stage;
	private GameManager _gameManager;
	private MenuDialog _menuDialog;
	private QuestController _questController;
	private TranslationManager _translationManager;
	private SceneName _currentScene;

	private void Awake()
	{
		_story = new Story(_textAsset.text);
	}

	public T GetVariable<T>(string var)
	{
		return (T) _story.variablesState[var];
	}

	public void StartInkManager()
	{
		_playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		_canvasManager = GetComponent<CanvasManager>();
		_gameManager = GetComponent<GameManager>();
		_questController = GetComponent<QuestController>();
		_sayDialog = GameObject.FindGameObjectWithTag("SayDialog").GetComponent<SayDialog>();
		_stage = GameObject.FindGameObjectWithTag("Stage").GetComponent<Stage>();
		_menuDialog = GameObject.FindGameObjectWithTag("MenuDialog").GetComponent<MenuDialog>();
		_translationManager = GetComponent<TranslationManager>();
	}

	public void InkJumpTo(string path)
	{
		_story.ChoosePathString(path);

		StartCoroutine(RunStory());
	}

	private IEnumerator RunStory()
	{
		Interaction.isInteracting = true;
		var block = _story.ContinueMaximally().Split('\n');

		foreach(string line in block)
		{
			if(line.Equals(""))
				continue;

			yield return StartCoroutine(ProcessPath(line));
		}

		if(_story.currentChoices.Count > 0)
		{
			_menuDialog.SetActive(true);
			_menuDialog.Clear();

			for(int i = 0; i < _story.currentChoices.Count; ++i)
			{
				int copy = i;
				void action()
				{
					_story.ChooseChoiceIndex(copy);
					StartCoroutine(RunStory());
					_menuDialog.SetActive(false);
					return;
				}

				var option = _translationManager.GetTranslatedLine(_story.currentChoices[i].text);
				_menuDialog.AddOption(option, true, false, action);
			}
		}
		else
		{
			Interaction.FinishInteraction();
		}

		if(_stage != null)
		{
			var characters = _stage.CharactersOnStage.Count;

			for(int i = 0; i < characters; i++)
			{
				_stage.Hide(_stage.CharactersOnStage[0]);
			}
		}
	}

	private IEnumerator ProcessPath(string line)
	{
		if(line.StartsWith(">>"))
		{
			_gameManager.SetCursorAction(CursorAction.Wait);

			yield return StartCoroutine(ProcessCommand(line));

			_gameManager.EndCursorWait();
		}
		else
		{
			var block = Regex.Split(line, "[\"]+");
			var characterAttributes = block[0].Split(' ');
			var character = GetCharacter(characterAttributes[0]);
			if(character != null)
				character.NameText = _translationManager.GetTranslatedName(characterAttributes[0]);

			if(_stage != null && character != null)
			{
				if(!_stage.CharactersOnStage.Contains(character))
					_stage.Show(character, character.Portraits[0].name, characterAttributes[1]);
			}

			var translatedLine = _translationManager.GetTranslatedLine(block[1]);

			_sayDialog.SetCharacter(character);
			yield return StartCoroutine(_sayDialog.DoSay(translatedLine, true, true, true, false, false, null, null));
		}
	}

	private IEnumerator ProcessCommand(string command)
	{
		var commandList = command[2..].Trim().Split(' ');;

		switch(commandList[0])
		{
			case "fadein":
				yield return StartCoroutine(_canvasManager.Fade("in", int.Parse(commandList[1])));
				break;

			case "fadeout":
				yield return StartCoroutine(_canvasManager.Fade("out", int.Parse(commandList[1])));
				break;

			case "bgchange":
				yield return StartCoroutine(_canvasManager.Fade("out", 1));
				GetComponent<CanvasManager>().UpdateBackground(commandList[1]);
				yield return new WaitForSeconds(1);
				yield return StartCoroutine(_canvasManager.Fade("in", 1));
				break;

			case "moveto":
				var actor = GameObject.Find(commandList[1]).GetComponent<NPCMovement>();
				var destination = GameObject.Find(commandList[2]).transform.position;
				actor.MoveTo(destination);

				break;

			case "additem":
				var itemType = (ItemType) Enum.Parse(typeof(ItemType), commandList[1]);
				var item = _gameManager.GetItemProperties(itemType);
				GetComponent<InventoryManager>().AddItem(item);

				break;

			case "removeitem":
				var i = (ItemType) Enum.Parse(typeof(ItemType), commandList[1]);
				GetComponent<InventoryManager>().RemoveItem(i);

				break;

			case "quest":
				Enum.TryParse(commandList[1], out QuestName quest);
				Enum.TryParse(commandList[2], out QuestStatus status);

				_questController.SetQuestStatus(quest, status);

				break;

			case "settrapped":
				var state = bool.Parse(commandList[1]);
				_playerMovement.IsTrapped = state;
				GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().speed = 1;

				break;

			case "screenshake":
				var action = bool.Parse(commandList[1]);
				Camera.main.GetComponent<CameraMotor>().IsShaking = action;

				break;

			case "sfx":
				var sound = commandList[1];
				var waitForFinish = bool.Parse(commandList[2]);
				var clip = _gameManager.GetSFXByName(sound);
				var audioSource = GetComponent<AudioSource>();

				audioSource.clip = clip;
				audioSource.Play();

				while(waitForFinish && audioSource.isPlaying)
					yield return null;

				break;

			case "setscene":
				Enum.TryParse(commandList[1], out _currentScene);
				break;

			case "alert":
				yield return _gameManager.DisplayMessage(commandList[1].Replace("\"", ""), "warning");
				break;

			case "message":
				yield return _gameManager.DisplayMessage(commandList[1].Replace("\"", ""), "message");
				break;

			case "showdatapad":
				var active = bool.Parse(commandList[1]);
				yield return _gameManager.SetDataPadVisibility(active);
				break;

			case "dim":
				yield return StartCoroutine(_canvasManager.Fade("dim-out", float.Parse(commandList[1])));
				break;

			case "undim":
				yield return StartCoroutine(_canvasManager.Fade("dim-in", float.Parse(commandList[1])));
				break;
		}
	}

	private Character GetCharacter(string name)
	{
		foreach(var character in characters)
		{
			if(character.name == $"{name}_Character")
				return character;
		}

		return null;
	}
}
