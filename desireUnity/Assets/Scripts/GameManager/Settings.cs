public class Settings
{
	public Language Language { get; set; } = Language.English;
	public float MasterVolume { get; set; } = 1f;
	public float FXVolume { get; set; } = 1f;
	public float BGVolume { get; set; } = 1f;
	public bool BeepSound { get; set; } = true;
	public bool ShowHints { get; set; }

}
