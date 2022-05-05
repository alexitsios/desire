using System;

public class EnumName : Attribute
{
    private string _name;
    
    public EnumName(string name)
	{
        _name = name;
	}

    public string Name
	{
        get { return _name; }
	}
}
