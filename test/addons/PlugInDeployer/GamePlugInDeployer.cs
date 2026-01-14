#if TOOLS
using Cobilas.GodotEngine.Utility.Runtime;

#pragma warning disable IDE0130
namespace Godot.PlugIn {
#pragma warning restore IDE0130
	/// <inheritdoc/>
	[Tool]
	public class GamePlugInDeployer : PlugInDeployer {
		private WindowDialog _window;

		public override void _EnterTree()
		{
			// Cria item de menu
			AddToolMenuItem("Meu Plugin/Abrir Janela", this, nameof(OnMenuPressed));

			// Cria janela (nao mostra ainda)
			_window = new WindowDialog
			{
				WindowTitle = "Minha Janela de Plugin",
				RectMinSize = new Vector2(400, 300)
			};

			// Layout
			var vbox = new VBoxContainer();
			_window.AddChild(vbox);

			var btn = new Button { Text = "Executar acao" };
			btn.Connect("pressed", this, nameof(OnButtonPressed));

			vbox.AddChild(btn);

			// Janela pertence ao editor
			GetEditorInterface().GetBaseControl().AddChild(_window);
		}

		public override void _ExitTree()
		{
			RemoveToolMenuItem("Meu Plugin/Abrir Janela");
			_window.QueueFree();
		}

		private void OnMenuPressed(object uc)
		{
			_window.PopupCentered();
		}

		private void OnButtonPressed()
		{
			GD.Print("Botao clicado!");
		}
	}
}
#endif
