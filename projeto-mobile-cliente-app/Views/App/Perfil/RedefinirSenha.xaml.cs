using Microsoft.Maui.ApplicationModel.Communication;
using projeto_mobile_cliente_app.Dto;
using projeto_mobile_cliente_app.Services;
using System.Text.Json;

namespace projeto_mobile_cliente_app.Views.App.Perfil;

public partial class RedefinirSenha : ContentPage
{
    public RedefinirSenha(ClienteDto cliente)
    {
        InitializeComponent();
        var formattedString = new FormattedString();
        var span1 = new Span { Text = "Uma mensagem foi enviada para o email: \n", TextColor = Color.FromHex("000000") };
        var span2 = new Span { Text = cliente.Usuario.Email, TextColor = Color.FromHex("0B6EFE") };

        formattedString.Spans.Add(span1);
        formattedString.Spans.Add(span2);
        LabelSuccess.FormattedText = formattedString;
        SendEmailAsync(cliente.Usuario.Email);
    }

    private void ReturnPerfil(object sender, TappedEventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private async void SendEmail(object sender, TappedEventArgs e)
    {
        string jsonString = Preferences.Get("usuarioLogado", string.Empty);
        var _cliente = JsonSerializer.Deserialize<ClienteDto>(jsonString);
        await SendEmailAsync(_cliente.Usuario.Email);
    }
    private async Task SendEmailAsync(String email)
    {
        try
        {
            var apiService = new ApiService();
            await apiService.PostAsync<String, object>("manter-conta/email?email=" + email, null);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}