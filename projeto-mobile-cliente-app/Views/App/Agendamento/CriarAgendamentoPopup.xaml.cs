using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui;
using projeto_mobile_cliente_app.Dto;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace projeto_mobile_cliente_app.Views.App.Agendamento;

public partial class CriarAgendamentoPopup : Popup, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public TransacaoClienteDto TransacaoCliente { get; set; }

    private List<PostoDto> _listPosto;
    private List<LiquidoDto> _listLiquido;
    public List<PostoDto> Postos
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
    public CriarAgendamentoPopup(TransacaoClienteDto transacaoCliente)
	{
		InitializeComponent();
        _listPosto = new List<PostoDto>
        {
            // Pendente
            new PostoDto
            {
                Id = 1,
                Nome = "Posto 1",
                CEP = "12345-678",
                Rua = "Avenida 232323",
                Numero = 2323,
                UF = "SP",
                Cidade = "São Paulo",
                DonoPosto = new DonoPostoDto
                {
                    Id = 1,
                    Usuario = new UsuarioDto
                    {
                        Id = 1,
                        Nome = "Dono 1",
                        CPFouCNPJ = "123.456.789-00",
                        Email = "dono1@email.com",
                        PerfilEnum = PerfilEnum.DonoPosto,
                        CEP = "12345-678",
                        Rua = "Avenida Paulista",
                        Numero = 100,
                        UF = "SP",
                        Cidade = "São Paulo",
                        StatusEnum = StatusEnum.Pendente,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
                },
                LiquidosAceitos = new List<PostoAceitaLiquidoDto>
                {
                    new PostoAceitaLiquidoDto
                    {
                        Liquido = new LiquidoDto
                        {
                            Id = 1,
                            Nome = "Líquido 1",
                            ValorUnitario = 10.0
                        },
                        CapacidadeTotal = 100,
                        CapacidadeOcupada = 50
                    }
                }
            },
            // Aprovado
            new PostoDto
            {
                Id = 2,
                Nome = "Posto 2",
                CEP = "23456-789",
                Rua = "Rua Santa Clara",
                Numero = 200,
                UF = "RJ",
                Cidade = "Rio de Janeiro",
                DonoPosto = new DonoPostoDto
                {
                    Id = 2,
                    Usuario = new UsuarioDto
                    {
                        Id = 2,
                        Nome = "Dono 2",
                        CPFouCNPJ = "234.567.890-11",
                        Email = "dono2@email.com",
                        PerfilEnum = PerfilEnum.DonoPosto,
                        CEP = "23456-789",
                        Rua = "Rua Santa Clara",
                        Numero = 200,
                        UF = "RJ",
                        Cidade = "Rio de Janeiro",
                        StatusEnum = StatusEnum.Aprovado,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
                },
                LiquidosAceitos = new List<PostoAceitaLiquidoDto>
                {
                    new PostoAceitaLiquidoDto
                    {
                        Liquido = new LiquidoDto
                        {
                            Id = 2,
                            Nome = "Líquido 2",
                            ValorUnitario = 20.0
                        },
                        CapacidadeTotal = 200,
                        CapacidadeOcupada = 100
                    }
                }
            },
            // Reprovado
            new PostoDto
            {
                Id = 3,
                Nome = "Posto 3",
                CEP = "34567-890",
                Rua = "Avenida Altamiro Avelino Soares",
                Numero = 300,
                UF = "MG",
                Cidade = "Belo Horizonte",
                DonoPosto = new DonoPostoDto
                {
                    Id = 3,
                    Usuario = new UsuarioDto
                    {
                        Id = 3,
                        Nome = "Dono 3",
                        CPFouCNPJ = "345.678.901-22",
                        Email = "dono3@email.com",
                        PerfilEnum = PerfilEnum.DonoPosto,
                        CEP = "34567-890",
                        Rua = "Avenida Altamiro Avelino Soares",
                        Numero = 300,
                        UF = "MG",
                        Cidade = "Belo Horizonte",
                        StatusEnum = StatusEnum.Reprovado,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
                },
                LiquidosAceitos = new List<PostoAceitaLiquidoDto>
                {
                    new PostoAceitaLiquidoDto
                    {
                        Liquido = new LiquidoDto
                        {
                            Id = 3,
                            Nome = "Líquido 3",
                            ValorUnitario = 30.0
                        },
                        CapacidadeTotal = 300,
                        CapacidadeOcupada = 150
                    }
                }
            },
            // Bloqueado
            new PostoDto
            {
                Id = 4,
                Nome = "Posto 4",
                CEP = "45678-901",
                Rua = "Avenida Bento Gonçalves",
                Numero = 400,
                UF = "RS",
                Cidade = "Porto Alegre",
                DonoPosto = new DonoPostoDto
                {
                    Id = 4,
                    Usuario = new UsuarioDto
                    {
                        Id = 4,
                        Nome = "Dono 4",
                        CPFouCNPJ = "456.789.012-33",
                        Email = "dono4@email.com",
                        PerfilEnum = PerfilEnum.DonoPosto,
                        CEP = "45678-901",
                        Rua = "Avenida Bento Gonçalves",
                        Numero = 400,
                        UF = "RS",
                        Cidade = "Porto Alegre",
                        StatusEnum = StatusEnum.Bloqueado,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
        },
                LiquidosAceitos = new List<PostoAceitaLiquidoDto>
                {
                    new PostoAceitaLiquidoDto
                    {
                        Liquido = new LiquidoDto
                        {
                            Id = 4,
                            Nome = "Líquido 4",
                            ValorUnitario = 40.0
                        },
                        CapacidadeTotal = 400,
                        CapacidadeOcupada = 200
                    }
                }
            }
        };
        _listLiquido = new List<LiquidoDto>
        {
            new LiquidoDto { Id = 1, Nome = "Liquido 1", ValorUnitario = 16.99 },
            new LiquidoDto { Id = 2, Nome = "Liquido 2", ValorUnitario = 17.99 },
            new LiquidoDto { Id = 3, Nome = "Liquido 3", ValorUnitario = 18.99 },
            new LiquidoDto { Id = 4, Nome = "Liquido 4", ValorUnitario = 18.99 },
            new LiquidoDto { Id = 5, Nome = "Liquido 5", ValorUnitario = 18.99 },
            new LiquidoDto { Id = 6, Nome = "Liquido 6", ValorUnitario = 18.99 }
        };
        TransacaoCliente = transacaoCliente;
        LiquidosSelecionados = new ObservableCollection<TransacaoItemDto>();
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
        OnPropertyChanged(nameof(IsTransactionLocked));
        BindingContext = this;
    }
    //private void CriarLiquido(object sender, EventArgs e)
    //{
    //    DateTime selectedDateTime = SelectedDate.Add(SelectedTime);
    //    if (SelectedPosto != null)
    //    {
    //        Application.Current.MainPage.DisplayAlert("ID selecionado", $"O {SelectedPosto.Id} - {selectedDateTime}", "OK");
    //    }
    //}

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

    private void OnSaveButtonClicked(object sender, EventArgs e)
    {
        DateTime selectedDateTime = SelectedDate.Add(SelectedTime);
        if (SelectedPosto != null)
        {
            Application.Current.MainPage.DisplayAlert("ID selecionado", $"O {LiquidosSelecionados[0].QtdAgendada} - {selectedDateTime}", "OK");
        }
    }

    private void RemoverLiquidoSelecionado(object sender, TappedEventArgs e)
    {
        var item = (TransacaoItemDto)e.Parameter;
        LiquidosSelecionados.Remove(item);
        _listLiquido.Where(x => x.Id == item.Liquido.Id).FirstOrDefault().IsSelected = false;
    }
}