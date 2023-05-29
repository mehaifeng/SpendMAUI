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

    private void OpenPopupBtn_Clicked(object sender, EventArgs e)
    {
        noteSpendPageViewModel.popupNewItem = new();
        noteSpendPageViewModel.ReadyItem = new();
        this.ShowPopup(noteSpendPageViewModel.popupNewItem);
    }
}