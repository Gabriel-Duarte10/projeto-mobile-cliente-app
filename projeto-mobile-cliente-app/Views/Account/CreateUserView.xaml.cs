namespace projeto_mobile_cliente_app.Views.Account;

public partial class CreateUserView : ContentPage
{
	public CreateUserView()
	{
		InitializeComponent();
	}
    private void CreateUser(object sender, EventArgs e)
    {

    }

    private void ReturnLogin(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }
}