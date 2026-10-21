using ActiproSoftware.UI.Avalonia.Controls.Bars;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace ModelUI;

/// <summary>
/// A notepad-like window hosted in a <see cref="RibbonWindow"/> with a ribbon bar,
/// wired together without any MVVM infrastructure.
/// </summary>
public partial class RibbonWindow : Window
{
	private const double DefaultFontSize = 14;
	private double _fontSize = DefaultFontSize;

	/// <summary>
	/// Initializes a new instance of the <see cref="RibbonWindow"/> class.
	/// </summary>
	public RibbonWindow()
	{
		InitializeComponent();
	}

	private void SetStatus(string message)
	{
		notepadStatusTextBlock.Text = message;
	}

	private void NewButton_OnClick(object? sender, RoutedEventArgs e)
	{
		editorTextBox.Text = string.Empty;
		SetStatus("New document created");
	}

	private void OpenButton_OnClick(object? sender, RoutedEventArgs e)
	{
		SetStatus("Open is a placeholder in this sample");
	}

	private void SaveButton_OnClick(object? sender, RoutedEventArgs e)
	{
		SetStatus("Saved (sample only)");
	}

	private void CutButton_OnClick(object? sender, RoutedEventArgs e)
	{
		editorTextBox.Cut();
		SetStatus("Cut");
	}

	private void CopyButton_OnClick(object? sender, RoutedEventArgs e)
	{
		editorTextBox.Copy();
		SetStatus("Copied");
	}

	private void PasteButton_OnClick(object? sender, RoutedEventArgs e)
	{
		editorTextBox.Paste();
		SetStatus("Pasted");
	}

	private void BoldButton_OnClick(object? sender, RoutedEventArgs e)
	{
		editorTextBox.FontWeight = ((BarToggleButton)sender).IsChecked is true ? FontWeight.Bold : FontWeight.Normal;
	}

	private void ItalicButton_OnClick(object? sender, RoutedEventArgs e)
	{
		editorTextBox.FontStyle = ((BarToggleButton)sender).IsChecked is true ? FontStyle.Italic : FontStyle.Normal;
	}

	private void UndoButton_OnClick(object? sender, RoutedEventArgs e)
	{
		editorTextBox.Undo();
		SetStatus("Undo");
	}

	private void RedoButton_OnClick(object? sender, RoutedEventArgs e)
	{
		editorTextBox.Redo();
		SetStatus("Redo");
	}

	private void WordWrapButton_OnClick(object? sender, RoutedEventArgs e)
	{
		editorTextBox.TextWrapping = ((BarToggleButton)sender).IsChecked is true
			? TextWrapping.Wrap
			: TextWrapping.NoWrap;
		SetStatus("Word wrap " + (((BarToggleButton)sender).IsChecked is true ? "on" : "off"));
	}

	private void ZoomInButton_OnClick(object? sender, RoutedEventArgs e)
	{
		_fontSize = System.Math.Min(48, _fontSize + 1);
		editorTextBox.FontSize = _fontSize;
		SetStatus($"Zoom: {_fontSize:0} pt");
	}

	private void ZoomOutButton_OnClick(object? sender, RoutedEventArgs e)
	{
		_fontSize = System.Math.Max(8, _fontSize - 1);
		editorTextBox.FontSize = _fontSize;
		SetStatus($"Zoom: {_fontSize:0} pt");
	}
}