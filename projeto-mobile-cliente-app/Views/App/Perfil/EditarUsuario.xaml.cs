namespace projeto_mobile_cliente_app.Views.App.Perfil;

public partial class EditarUsuario : ContentPage
{
	public EditarUsuario()
	{
		InitializeComponent();
	}
    private void ReturnPerfil(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void SalvarUser(object sender, EventArgs e)
    {

    }
}