using System.Text.Json;
using CommunityToolkit.Maui.Views;
using projeto_mobile_cliente_app.Dto;
using projeto_mobile_cliente_app.Views.Account;

namespace projeto_mobile_cliente_app.Views.App.Perfil;

public partial class PerfilView : ContentPage
{
    public ClienteDto _cliente;
    public PerfilView()
	{
        InitializeComponent();
        LoadDataAsync();
    }
    private async Task LoadDataAsync()
    {
        string jsonString = Preferences.Get("usuarioLogado", string.Empty);
        _cliente = JsonSerializer.Deserialize<ClienteDto>(jsonString);
        lblNome.Text = _cliente.Usuario.Nome;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Chama a função para carregar os dados
        LoadDataAsync();
    }
    private void EditarPerfil(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new EditarUsuario());
    }
    private async void RedefinirSenha(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Confirmação", "Tem certeza que desja redefinir sua senha?", "Confirmar", "Cancelar");
        if (answer)
        {
            Navigation.PushModalAsync(new RedefinirSenha(_cliente));
        }
    }
    private void SairConta(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);

        Preferences.Remove("usuarioLogado");
        Application.Current.MainPage = new NavigationPage(new LoginView());

        popup.Close();
    }
}