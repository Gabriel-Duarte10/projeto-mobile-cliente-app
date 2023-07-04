using CommunityToolkit.Maui.Views;
using projeto_mobile_cliente_app.Dto;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace projeto_mobile_cliente_app.Views.App.Agendamento;

public partial class VisualizarAgendamento : Popup, INotifyPropertyChanged
{
    public TransacaoClienteDto TransacaoCliente { get; set; }
    private ObservableCollection<TransacaoItemDto> _liquidosSelecionados;
    public ObservableCollection<TransacaoItemDto> LiquidosSelecionados
    {
        get { return _liquidosSelecionados; }
        set
        {
            _liquidosSelecionados = value;
            OnPropertyChanged();
        }
    }
    private bool _isTransactionLocked = true;
    public bool IsTransactionLocked
    {
        get { return _isTransactionLocked; }
        set
        {
            if (_isTransactionLocked != value)
            {
                _isTransactionLocked = value;
                OnPropertyChanged(nameof(IsTransactionLocked));
            }
        }
    }
    public VisualizarAgendamento(TransacaoClienteDto transacaoCliente)
	{
		InitializeComponent();
        TransacaoCliente = transacaoCliente;
        LiquidosSelecionados = new ObservableCollection<TransacaoItemDto>(TransacaoCliente.TransacaoItem);
        LabelData.Text = "  " + TransacaoCliente.dataAgendada.ToString("dd/MM/yyyy HH:mm");
        LabelPostoNome.Text = TransacaoCliente.Posto.Nome;
        LabelEnderecoPosto.Text = TransacaoCliente.Posto.Rua + ", " + 
                TransacaoCliente.Posto.Numero + ", " +
                TransacaoCliente.Posto.Cidade + ", " +
                TransacaoCliente.Posto.UF;
        LabelAgendamentoCodigo.Text = "Codigo: " + TransacaoCliente.CodigoTransacao;
        if (TransacaoCliente.Status != StatusEnum.Pendente)
        {
            IsTransactionLocked = false;
        }
        OnPropertyChanged(nameof(IsTransactionLocked));
        BindingContext = this;
        MessagingCenter.Subscribe<Application>(Application.Current, "VisualizarAsync", (sender) =>
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                // Coloque um breakpoint aqui e inspecione a exceção
                Console.WriteLine(ex);
            }
        });

    }
    // Onde quer que você esteja fechando o popup
    private async void ClosePopup()
    {
        MessagingCenter.Unsubscribe<Application>(Application.Current, "VisualizarAsync");
        await Application.Current.MainPage.Navigation.PopModalAsync();
    }
    private void EditarAgendamento(object sender, TappedEventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var popupPage = new CriarAgendamentoPopup(TransacaoCliente);
            popupPage.Size = new Size(350, 580);     
            await Application.Current.MainPage.ShowPopupAsync(popupPage);
        });
    }

    // Implementação de INotifyPropertyChanged.
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}