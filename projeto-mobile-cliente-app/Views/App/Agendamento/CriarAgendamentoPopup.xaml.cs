using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui;
using projeto_mobile_cliente_app.Dto;
using projeto_mobile_cliente_app.Requests;
using projeto_mobile_cliente_app.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace projeto_mobile_cliente_app.Views.App.Agendamento;

public partial class CriarAgendamentoPopup : Popup, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public TransacaoClienteDto TransacaoCliente { get; set; }

    private List<LiquidoDto> _listLiquido;
    private ObservableCollection<PostoDto> _listPosto;
    public ObservableCollection<PostoDto> Postos
    {
        get { return _listPosto; }
        set
        {
            _listPosto = value;
            OnPropertyChanged();
        }
    }

    private PostoDto _selectedPosto;
    public PostoDto SelectedPosto
    {
        get { return _selectedPosto; }
        set
        {
            _selectedPosto = value;
            OnPropertyChanged();
        }
    }
    private DateTime _selectedDate = DateTime.Today;
    public DateTime SelectedDate
    {
        get { return _selectedDate; }
        set
        {
            _selectedDate = value;
            OnPropertyChanged();
        }
    }

    private TimeSpan _selectedTime = DateTime.Now.TimeOfDay;
    public TimeSpan SelectedTime
    {
        get { return _selectedTime; }
        set
        {
            _selectedTime = value;
            OnPropertyChanged();
        }
    }
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
    public ClienteDto _cliente;
    public CriarAgendamentoPopup(TransacaoClienteDto transacaoCliente)
	{
		InitializeComponent();
        string jsonString = Preferences.Get("usuarioLogado", string.Empty);
        _cliente = JsonSerializer.Deserialize<ClienteDto>(jsonString);
        TransacaoCliente = transacaoCliente;
        
        LiquidosSelecionados = new ObservableCollection<TransacaoItemDto>();

        LoadDataAsync();

        OnPropertyChanged(nameof(IsTransactionLocked));
        BindingContext = this;
    }
    private async Task LoadDataAsync()
    {
        try
        {
            var apiService = new ApiService();

            Postos = new ObservableCollection<PostoDto>(await apiService.GetAsync<List<PostoDto>>("posto"));

            _listLiquido = await apiService.GetAsync<List<LiquidoDto>>("liquido");

            if (TransacaoCliente != null)
            {
                LabelTitle.Text = "Editar Líquido";
                LiquidosSelecionados = new ObservableCollection<TransacaoItemDto>(TransacaoCliente.TransacaoItem);
                SelectedPosto = _listPosto.FirstOrDefault(x => x.Id == TransacaoCliente.Posto.Id);
                SelectedDate = TransacaoCliente.dataAgendada.Date;
                SelectedTime = TransacaoCliente.dataAgendada.TimeOfDay;
                if (TransacaoCliente.Status != StatusEnum.Pendente)
                {
                    IsTransactionLocked = false;
                }

                // Notificar a alteração das propriedades para atualizar a UI
                OnPropertyChanged(nameof(SelectedDate));
                OnPropertyChanged(nameof(SelectedTime));
            }
            else
            {
                IsTransactionLocked = true;
                LabelTitle.Text = "Criar Líquido";
            }

        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SelecionarLiquido(object sender, EventArgs e)
    {
        var liquidoDisponivel = _listLiquido;
        if (LiquidosSelecionados.Count() > 0)
        {
            var LiquidosSelecionadosIds = LiquidosSelecionados.Select(x => x.Liquido.Id).ToList();
            liquidoDisponivel = _listLiquido.Where(x => !LiquidosSelecionadosIds.Contains(x.Id)).ToList();
        }

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var popupPage = new AdicionarLiquidoPopup(liquidoDisponivel);
            popupPage.Size = new Size(350, 450);
            popupPage.OnSaveClicked += (selectedLiquidos) =>
            {
                foreach (var item in selectedLiquidos)
                {
                    LiquidosSelecionados.Add(new TransacaoItemDto()
                    {
                        Liquido = item,
                        QtdAgendada = 0
                    });
                }
            };
            await Application.Current.MainPage.ShowPopupAsync(popupPage);
        });
        
    }

    private void OnCancelButtonClicked(object sender, EventArgs e)
    {
        this.Close();
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        try
        {
            if (pickerPosto.SelectedItem == null)
            {
                Application.Current.MainPage.DisplayAlert("Erro", "Selecione um posto", "OK");
                return;
            }
            if (SelectedDate < DateTime.Now)
            {
                Application.Current.MainPage.DisplayAlert("Erro", "Selecione uma data maior que o dia atual", "OK");
                return;
            }
            if (SelectedDate == DateTime.Now && SelectedTime < DateTime.Now.TimeOfDay)
            {
                Application.Current.MainPage.DisplayAlert("Erro", "Selecione uma hora maior que a atual", "OK");
                return;
            }
            if (LiquidosSelecionados.Count() == 0)
            {
                Application.Current.MainPage.DisplayAlert("Erro", "Selecione ao menos um líquido", "OK");
                return;
            }
            if (LiquidosSelecionados.Any(x => x.QtdAgendada == 0))
            {
                Application.Current.MainPage.DisplayAlert("Erro", "Selecione uma quantidade maior que 0 para o liquido", "OK");
                return;
            }

            var transacaoRequest = new TransacaoClienteRequest()
            {
                Id = 0,
                DataAgendada = SelectedDate.Add(SelectedTime),
                IdCliente = _cliente.Id,
                IdPosto = SelectedPosto.Id,
                TransacaoItens = LiquidosSelecionados.Select(x => new TransacaoItensRequest()
                {
                    IdLiquido = x.Liquido.Id,
                    QtdAgendada = x.QtdAgendada
                }).ToList()
            };
            if (TransacaoCliente != null)
            {
                transacaoRequest.Id = TransacaoCliente.Id;
                await UpdateAgendamentoAsync(transacaoRequest);
            }
            else
            {
                await CreateAgendamentoAsync(transacaoRequest);
            }
           

        }
        catch(Exception ex)
        {

            throw;
        }
        finally
        {
            this.Close();
            MessagingCenter.Send(Application.Current, "VisualizarAsync");
            MessagingCenter.Send(Application.Current, "AgendamentoLoadDataAsync");

        }
    }
    private async Task CreateAgendamentoAsync(TransacaoClienteRequest transacaoClienteRequest)
    {
        try
        {
            var apiService = new ApiService();
            await apiService.PostAsync<TransacaoClienteRequest, object>("cliente/agendamento", transacaoClienteRequest);

            await Application.Current.MainPage.DisplayAlert("Sucesso", "Agendamento criado com sucesso", "Ok");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private async Task UpdateAgendamentoAsync(TransacaoClienteRequest transacaoClienteRequest)
    {
        try
        {
            var apiService = new ApiService();
            await apiService.PutAsync<TransacaoClienteRequest, object>("cliente/agendamento", transacaoClienteRequest);

            await Application.Current.MainPage.DisplayAlert("Sucesso", "Agendamento Atualizado com sucesso", "Ok");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void RemoverLiquidoSelecionado(object sender, TappedEventArgs e)
    {
        var item = (TransacaoItemDto)e.Parameter;
        LiquidosSelecionados.Remove(item);
        _listLiquido.Where(x => x.Id == item.Liquido.Id).FirstOrDefault().IsSelected = false;
    }
}