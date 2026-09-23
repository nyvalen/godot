using Godot;
using System;
using System.Threading.Tasks;

public partial class MozgóPlatformok : Node
{
	// értékek
	public static short i = 0;
	public static short korlát = 30;
	public static short mozgásértéke = 4;
	
	// mozgás
	public async Task idő() {
		await ToSignal(GetTree().CreateTimer(0.001), SceneTreeTimer.SignalName.Timeout);
	}
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		Godot.Collections.Array<Node> nodes = GetChildren();
		
		while (true) {
			for (i = 0; i < korlát; ++i) {
				foreach (StaticBody2D child in nodes) {
					child.Position += Vector2.Up * mozgásértéke;
				}
			
				await idő();
			}
		
			for (i = 0; i < korlát; ++i) {
				foreach (StaticBody2D child in nodes) {
					child.Position -= Vector2.Up * mozgásértéke;
				}
			
				await idő();
			}
		}
	}
}
