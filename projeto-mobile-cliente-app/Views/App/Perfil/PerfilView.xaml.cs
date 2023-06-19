using projeto_mobile_cliente_app.Dto;

namespace projeto_mobile_cliente_app.Views.App.Perfil;

public partial class PerfilView : ContentPage
{
    private readonly ClienteDto _adm;
    public PerfilView()
	{
		InitializeComponent();
        _adm = new ClienteDto()
        {
            Id = 1,
            Usuario = new UsuarioDto
            {
                Id = 1,
                Nome = "Usuario Adm",
                CPFouCNPJ = "12345678910",
                Email = "usuario1@email.com",
                PerfilEnum = PerfilEnum.Cliente,
                CEP = "12345678",
                Rua = "Rua A",
                Numero = 1,
                UF = "UF",
                Cidade = "Cidade",
                StatusEnum = StatusEnum.Bloqueado,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }
        };
        lblNome.Text = _adm.Usuario.Nome;
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
            Navigation.PushModalAsync(new RedefinirSenha(_adm));
        }
    }
    private void SairConta(object sender, EventArgs e)
    {

    }
}