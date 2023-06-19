using CommunityToolkit.Maui.Views;
using projeto_mobile_cliente_app.Dto;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

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
    public AgendamentoView()
	{
		InitializeComponent();
        _originalListPosto = new List<PostoDto>
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
        _originalListTransacao = new ObservableCollection<TransacaoClienteDto>
        {
            // Transacao pendente
            new TransacaoClienteDto
            {
                Id = 1,
                Status = StatusEnum.Pendente,
                dataAgendada = DateTime.Now.AddDays(10),
                CodigoTransacao = "TR1",
                Posto = _originalListPosto[0], // Aqui estou utilizando o primeiro posto da lista de postos criada anteriormente
                TransacaoItem = new List<TransacaoItemDto>
                {
                    new TransacaoItemDto
                    {
                        QtdAgendada = 10,
                        Valor = 100.0,
                        CreatedAt = DateTime.Now,
                        Liquido = _originalListPosto[0].LiquidosAceitos[0].Liquido // Aqui estou utilizando o primeiro liquido do primeiro posto da lista de postos criada anteriormente
                    }
                }
            },
            // Transacao aprovada
            new TransacaoClienteDto
            {
                Id = 2,
                Status = StatusEnum.Aprovado,
                dataAgendada = DateTime.Now,
                CodigoTransacao = "TR2",
                Posto = _originalListPosto[1], // Aqui estou utilizando o segundo posto da lista de postos criada anteriormente
                TransacaoItem = new List<TransacaoItemDto>
                {
                    new TransacaoItemDto
                    {
                        QtdAgendada = 20,
                        Valor = 100.0,
                        CreatedAt = DateTime.Now,
                        Liquido = _originalListPosto[1].LiquidosAceitos[0].Liquido // Aqui estou utilizando o primeiro liquido do segundo posto da lista de postos criada anteriormente
                    }
                }
            },
            // Transacao cancelada
            new TransacaoClienteDto
            {
                Id = 3,
                Status = StatusEnum.Cancelado,
                dataAgendada = DateTime.Now,
                CodigoTransacao = "TR3",
                Posto = _originalListPosto[2], // Aqui estou utilizando o terceiro posto da lista de postos criada anteriormente
                TransacaoItem = new List<TransacaoItemDto>
                {
                    new TransacaoItemDto
                    {
                        QtdAgendada = 30,
                        Valor = 100.0,
                        CreatedAt = DateTime.Now,
                        Liquido = _originalListPosto[2].LiquidosAceitos[0].Liquido // Aqui estou utilizando o primeiro liquido do terceiro posto da lista de postos criada anteriormente
                    }
                }
            }
        };
        ListTransacao = new ObservableCollection<TransacaoClienteDto>(_originalListTransacao);
        selectStatus.SelectedIndex = 0;
        BindingContext = this;
    }

    private void CriarAgendamento(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var popupPage = new CriarAgendamentoPopup(null);
            popupPage.Size = new Size(350, 530);
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

    private void CancelarAgendamento(object sender, TappedEventArgs e)
    {
        var TransacaoCliente = (TransacaoClienteDto)e.Parameter;
        var Transacao = ListTransacao
            .FirstOrDefault(x => x.Id == TransacaoCliente.Id);
        Transacao.Status = StatusEnum.Cancelado;

        // Cria uma nova lista a partir da lista atual
        var updatedList = new ObservableCollection<TransacaoClienteDto>(ListTransacao);

        // Atribui a nova lista à ListTransacao, disparando a notificação de alteração de propriedade
        ListTransacao = updatedList;

        OnPropertyChanged(nameof(ListTransacao));
        Application.Current.MainPage.DisplayAlert("Sucesso", $"O Agendamento foi cancelado com sucesso", "OK");
    }
}