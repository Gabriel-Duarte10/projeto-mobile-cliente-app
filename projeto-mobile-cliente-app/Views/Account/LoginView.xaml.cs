using CommunityToolkit.Maui.Views;
using projeto_mobile_cliente_app.Dto;
using projeto_mobile_cliente_app.Requests;
using projeto_mobile_cliente_app.Services;
using projeto_mobile_cliente_app.Views.App;
using System.Text.Json;

namespace projeto_mobile_cliente_app.Views.Account;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
	}
    private async void LoginPost(object sender, EventArgs e)
    {
        var popup = new SpinnerPopup();
        this.ShowPopup(popup);

        if (EntryLogin.Text == null || EntrySenha.Text == null)
        {
            await DisplayAlert("Erro", "Preencha todos os campos", "OK");
            popup.Close(); // Feche o popup se houver um erro
            return;
        }
        var apiService = new ApiService();
        LoginRequest loginRequest = new LoginRequest
        {
            Login = EntryLogin.Text,
            Senha = EntrySenha.Text,
            PerfilEnum = PerfilEnum.Cliente
        };
        try
        {
            LoginDto loginResponse = await apiService.PostAsync<LoginRequest, LoginDto>("manter-conta/login", loginRequest);
            if (loginResponse != null)
            {
                string jsonString = JsonSerializer.Serialize(loginResponse.Cliente);
                Preferences.Default.Set("usuarioLogado", jsonString);
                Application.Current.MainPage = new NavigationPage(new AppView());
            }
            popup.Close(); // Feche o popup se houver um erro
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
            popup.Close(); // Feche o popup se houver um erro
        }
    }

    private void NavigationForgotPassword(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new ForgotPasswordView());
    }

    private void NavigationCreateUser(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new CreateUserView());
    }
}