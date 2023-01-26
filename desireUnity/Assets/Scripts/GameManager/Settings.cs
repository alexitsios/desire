public class Settings
{
	public Language Language { get; set; } = Language.English;
	public float MasterVolume { get; set; } = 0.3f;
	public float FXVolume { get; set; } = 1f;
	public float BGVolume { get; set; } = 0.3f;
	public bool BeepSound { get; set; } = true;
	public bool ShowHints { get; set; }

}
