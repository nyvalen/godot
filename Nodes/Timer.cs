using Godot;
using System;

public partial class Timer : Label
{
	public static double Time = 0;
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Time += delta;
		
		string hi = $"{(int) Time}.";
		string hi2 = $"{Time:F2}".Substring(hi.Length, 2);
		
		Text = hi + hi2;
	}
}
