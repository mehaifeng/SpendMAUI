using CommunityToolkit.Maui.Views;
using SpendMAUI.ViewModels;

namespace SpendMAUI.Views;

public partial class NoteSpendPage : ContentPage
{
    public NoteSpendPage()
	{
		InitializeComponent();
		this.BindingContext = new NoteSpendPageViewModel();
    }
}