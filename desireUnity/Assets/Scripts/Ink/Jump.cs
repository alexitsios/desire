using Fungus;
using UnityEngine;

[CommandInfo("Ink", "Jump", "Jump to a knot or stitch")]
public class Jump : Command
{
    private static InkManager manager;

    public string path;

    protected InkManager Manager()
    {
        if(manager == null)
        {
            manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<InkManager>();
        }

        return manager;
    }

    public override string GetSummary()
    {
        return $"Jump to '{path}' in Ink";
    }

    public override void OnEnter()
    {
        Manager().InkJumpTo(path);

        Continue();
    }
}
