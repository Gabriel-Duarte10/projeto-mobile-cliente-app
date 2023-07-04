using CommunityToolkit.Maui.Views;
using projeto_mobile_cliente_app.Dto;
using projeto_mobile_cliente_app.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace projeto_mobile_cliente_app.Views.App.Agendamento;

public partial class AgendamentoView : ContentPage, INotifyPropertyChanged
{
    private ObservableCollection<TransacaoClienteDto> _listTransacao;
    private ObservableCollection<TransacaoClienteDto> _originalListTransacao;
    public ObservableCollection<TransacaoClienteDto> ListTransacao
    {
        get { return _listTransacao; }
        set
        {
            if (_listTransacao != value)
            {
                _listTransacao = value;
                OnPropertyChanged();
            }
        }
    }
    private string _filterText;
    private CancellationTokenSource _cts = new CancellationTokenSource();
    public string FilterText
    {
        get { return _filterText; }
        set
        {
            if (_filterText != value)
            {
                _filterText = value;
                OnPropertyChanged();

                _cts.Cancel(); // cancel any previous search
                _cts = new CancellationTokenSource();

                Task.Delay(500, _cts.Token) // delay for half a second
                    .ContinueWith(
                        t => PesquisaFiltroList(),
                        CancellationToken.None,
                        TaskContinuationOptions.OnlyOnRanToCompletion,
                        TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
    public List<PostoDto> _originalListPosto { get; set; }
    public ClienteDto _cliente;
    public AgendamentoView()
	{
		InitializeComponent();
        string jsonString = Preferences.Get("usuarioLogado", string.Empty);
        _cliente = JsonSerializer.Deserialize<ClienteDto>(jsonString);
  ;
        _originalListTransacao = new ObservableCollection<TransacaoClienteDto>();
        LoadDataAsync();

        selectStatus.SelectedIndex = 0;
        BindingContext = this;

        MessagingCenter.Subscribe<Application>(Application.Current, "AgendamentoLoadDataAsync", (sender) =>
        {
            LoadDataAsync();
        });
    }
    private async Task LoadDataAsync()
    {
        try
        {
            LoadingIndicator.IsRunning = true;
            LoadingIndicator.IsVisible = true;
      
            ClienteCollectionView.IsVisible = false;
   
            var apiService = new ApiService();
            
            _originalListPosto = await apiService.GetAsync<List<PostoDto>>("posto");

            var transacaoReq = await apiService.GetAsync<List<TransacaoClienteDto>>("cliente/agendamento?IdCliente=" + _cliente.Id);

            _originalListTransacao = new ObservableCollection<TransacaoClienteDto>(transacaoReq);

            ListTransacao = new ObservableCollection<TransacaoClienteDto>(_originalListTransacao);

        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;

            ClienteCollectionView.IsVisible = true;
            FiltroStatus(selectStatus, EventArgs.Empty);
        }
    }

    private void CriarAgendamento(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var popupPage = new CriarAgendamentoPopup(null);
            popupPage.Size = new Size(350, 580);
            await Application.Current.MainPage.ShowPopupAsync(popupPage);
        });
    }
    void FiltroStatus(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        
        if (selectedIndex == 0)
        {
            ListTransacao = new ObservableCollection<TransacaoClienteDto>(_originalListTransacao);
        }
        if (selectedIndex == 1)
        {
            ListTransacao = new ObservableCollection<TransacaoClienteDto>(_originalListTransacao
                    .Where(x => x.Status == StatusEnum.Pendente).ToList());
        }
        if (selectedIndex == 2)
        {
            ListTransacao = new ObservableCollection<TransacaoClienteDto>(_originalListTransacao
                    .Where(x => x.Status == StatusEnum.Aprovado).ToList());
        }
        if (selectedIndex == 3)
        {
            ListTransacao = new ObservableCollection<TransacaoClienteDto>(_originalListTransacao
                    .Where(x => x.Status == StatusEnum.Reprovado).ToList());
        }
        if (selectedIndex == 4)
        {
            ListTransacao = new ObservableCollection<TransacaoClienteDto>(_originalListTransacao
                    .Where(x => x.Status == StatusEnum.Bloqueado).ToList());
        }
        OnPropertyChanged(nameof(ListTransacao));
       
    }
    private void PesquisaFiltroList()
    {
        if (string.IsNullOrWhiteSpace(FilterText))
        {
            ListTransacao = new ObservableCollection<TransacaoClienteDto>(_originalListTransacao);
        }
        else
        {
            ListTransacao = new ObservableCollection<TransacaoClienteDto>(_originalListTransacao
                .Where(l => l.Posto.Nome.Contains(FilterText, StringComparison.OrdinalIgnoreCase)).ToList());           
        }
        OnPropertyChanged(nameof(ListTransacao));
    }
    // Implementação de INotifyPropertyChanged.
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Chama a função para carregar os dados
        LoadDataAsync();
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        MessagingCenter.Unsubscribe<Application>(Application.Current, "AgendamentoLoadDataAsync");
    }
    private void AbrirAgendamento(object sender, TappedEventArgs e)
    {
        var TransacaoCliente = (TransacaoClienteDto)e.Parameter;
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var popupPage = new VisualizarAgendamento(TransacaoCliente);
            popupPage.Size = new Size(350, 500);
            await Application.Current.MainPage.ShowPopupAsync(popupPage);
        });
    }

    private async void CancelarAgendamento(object sender, TappedEventArgs e)
    {
        try
        {
            var TransacaoCliente = (TransacaoClienteDto)e.Parameter;
            await DeleteLiquidoAsync(TransacaoCliente);
            var Transacao = ListTransacao
                .FirstOrDefault(x => x.Id == TransacaoCliente.Id);
            Transacao.Status = StatusEnum.Cancelado;

            // Cria uma nova lista a partir da lista atual
            var updatedList = new ObservableCollection<TransacaoClienteDto>(ListTransacao);

            // Atribui a nova lista à ListTransacao, disparando a notificação de alteração de propriedade
            ListTransacao = updatedList;

            OnPropertyChanged(nameof(ListTransacao));
            await Application.Current.MainPage.DisplayAlert("Sucesso", $"O Agendamento foi cancelado com sucesso", "OK");
        }
        catch (Exception)
        {
            throw;
        }
    }
    private async Task DeleteLiquidoAsync(TransacaoClienteDto item)
    {
        try
        {
            var apiService = new ApiService();
            await apiService.DeleteAsync("cliente/agendamento/" + item.Id.ToString());
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}