using CommunityToolkit.Maui.Views;
using SpendMAUI.Models;
using SpendMAUI.ViewModels;

namespace SpendMAUI.Views;

public partial class NoteSpendPage : ContentPage
{
    readonly NoteSpendPageViewModel noteSpendPageViewModel;
    public NoteSpendPage()
	{
		InitializeComponent();
        noteSpendPageViewModel = new NoteSpendPageViewModel();
        this.BindingContext = noteSpendPageViewModel;
    }

    private void DeleteItemBtn_Clicked(object sender, EventArgs e)
    {
        if((sender as Button).CommandParameter is Item item)
        {
            noteSpendPageViewModel.RAndSItems.Remove(item);
            noteSpendPageViewModel.CaculateSum();
        }  
    }
}