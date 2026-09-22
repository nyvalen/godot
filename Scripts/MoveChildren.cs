using Godot;
using System;
using System.Threading.Tasks;

public partial class MoveChildren : Node2D
{
	// mozgás
	public async Task idő() {
		await ToSignal(GetTree().CreateTimer(0.001), SceneTreeTimer.SignalName.Timeout);
	}
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		Godot.Collections.Array<Node> nodes = GetChildren();
		
		while (true) {
			for (int i = 0; i < 30; i++) {
				foreach (StaticBody2D child in nodes) {
					child.Position += Vector2.Right * 2;
				}
			
				await idő();
			}
		
			for (int i = 0; i < 30; i++) {
				foreach (StaticBody2D child in nodes) {
					child.Position -= Vector2.Right * 2;
				}
			
				await idő();
			}
		}
	}
}
