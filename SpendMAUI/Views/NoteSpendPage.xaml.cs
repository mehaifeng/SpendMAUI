using CommunityToolkit.Maui.Views;
using SpendMAUI.ViewModels;

namespace SpendMAUI.Views;

public partial class NoteSpendPage : ContentPage
{
    NoteSpendPageViewModel noteSpendPageViewModel;
    public NoteSpendPage()
	{
		InitializeComponent();
        noteSpendPageViewModel = new NoteSpendPageViewModel();
        this.BindingContext = noteSpendPageViewModel;
    }
}