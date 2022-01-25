public class MenuOption
{
	public string Name { get; set; }
	public MenuOptionType OptionType { get; set; }

	public MenuOption(string name, MenuOptionType optionType)
	{
		Name = name;
		OptionType = optionType;
	}
}
