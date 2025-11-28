using Godot;
using Cobilas.GodotEngine.Utility;
using Cobilas.GodotEngine.Utility.Input;
using Cobilas.GodotEngine.Utility.Runtime;
using Cobilas.GodotEngine.Utility.Numerics;

public class Rect2D_Test : Camera2D
{
    [Export] private NodePath _label;
	[Export] private NodePath _sprite;

	[Export] private NodePath _panel;
	[Export] private NodePath _offsetx;
	[Export] private NodePath _offsety;
	[Export] private NodePath _centeredT;

	[Export] private Vector2 pivot;
	[Export] private float rotation;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {

    }

	public override void _Process(float delta) {
		Vector2D s_offset = Vector2D.Zero;
		Sprite sprite = GetNode<Sprite>(_sprite);
		Label label = GetNode<Label>(_label);
		Panel panel = GetNode<Panel>(_panel);

		TextEdit offsetx = GetNode<TextEdit>(_offsetx),
				 offsety = GetNode<TextEdit>(_offsety);
		CheckButton centeredT = GetNode<CheckButton>(_centeredT);

		sprite.Centered = centeredT.Pressed;

		if (int.TryParse(offsetx.Text, out int offx))
			s_offset.x = offx;
		if (int.TryParse(offsety.Text, out int offy))
			s_offset.y = offy;

		sprite.Offset = s_offset;

		Rect2D rectdd = new Rect2D(512f, 185f, 64f, 64f, 0f, 0f, rotation, 1f, 1f, 0f, 0f).SetPivot(pivot);
		Rect2D rect = sprite.GetRect2D();
		Rect2D prect = panel.GetRect2D();
		Vector2D mpos = InputKeyBoard.MousePosition;
		Vector2D pos = this.ScreenToWorldPoint(mpos);

		if (InputKeyBoard.GetKeyPress(KeyCode.D))
			Position += Vector2.Right * 35f * RunTime.DeltaTime;
		if (InputKeyBoard.GetKeyPress(KeyCode.A))
			Position += Vector2.Left * 35f * RunTime.DeltaTime;

		DrawRect(rect);
		DrawRect(rectdd);
		DrawRect(Position);

		label.ClearText()
			.AppendLine($"Sprite-InRect : {rect.HasPoint(pos)}")
			.AppendLine($"Panel-InRect : {prect.HasPoint(mpos)}")
			.AppendLine($"rectdd-InRect : {rectdd.HasPoint(pos)}")
			.AppendLine($"Mouse position : {mpos}")
			.AppendLine($"Current resolution : {Screen.CurrentResolution}")
			.AppendLine($"Zoom : {Zoom}")
			.AppendLine($"Viewport rect : {base.GetViewportRect()}");

		prect.SetPosition(this.ScreenToWorldPoint(prect.Position));
		DrawRect(prect);
	}

	private void DrawRect(Vector2D pos) {

		Gizmos.Color = Color.Color8(255, 0, 0);

		Gizmos.DrawLine(pos, pos + Vector2D.Right * 5f);

		Gizmos.Color = Color.Color8(0, 255, 0);

		Gizmos.DrawLine(pos, pos + Vector2D.Up * 5f);
	}

	private void DrawRect(Rect2D rect) {
		Gizmos.Color = Color.Color8(0, 0, 0);

		Vector2D position = rect.Position;
		Quaternion quaternion = Quaternion.ToQuaternion(Vector3D.Forward * rect.RadianRotation);
		Vector2D dirx = quaternion.GenerateDirectionRight() * rect.SizeScale.x;
		Vector2D diry = quaternion.GenerateDirectionDown() * rect.SizeScale.y;
		Vector2D pivx = quaternion.GenerateDirectionLeft() * rect.PivotScale.x;
		Vector2D pivy = quaternion.GenerateDirectionUp() * rect.PivotScale.y;
		position += pivx + pivy;
		Vector2D px = position + dirx;
		Vector2D py = position + diry;
		Vector2D pxy = position + dirx + diry;

		Gizmos.DrawLine(position, px);
		Gizmos.DrawLine(position, py);
		Gizmos.DrawLine(px, pxy);
		Gizmos.DrawLine(py, pxy);

		Gizmos.Color = Color.Color8(255, 0, 0);

		Gizmos.DrawLine(rect.Position, rect.Position + Vector2D.Right * 5f);

		Gizmos.Color = Color.Color8(0, 255, 0);

		Gizmos.DrawLine(rect.Position, rect.Position + Vector2D.Up * 5f);
	}
}
