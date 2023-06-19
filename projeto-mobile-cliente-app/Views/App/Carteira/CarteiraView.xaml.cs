using projeto_mobile_cliente_app.Dto;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace projeto_mobile_cliente_app.Views.App.Carteira;

public partial class CarteiraView : ContentPage, INotifyPropertyChanged
{
    private List<TransacaoItemDto> _listTransacao;
    private List<TransacaoItemDto> _originalListTransacao;
    public List<TransacaoItemDto> ListTransacao
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
    public CarteiraView()
	{
		InitializeComponent();
        var _originalListPosto = new List<PostoDto>
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
        var _originalListTransacao = new List<TransacaoClienteDto>
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
                        Valor = 24,
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
                        Valor = 67,
                        CreatedAt = DateTime.Now,
                        Liquido = _originalListPosto[2].LiquidosAceitos[0].Liquido // Aqui estou utilizando o primeiro liquido do terceiro posto da lista de postos criada anteriormente
                    }
                }
            }
        };
        var _adm = new ClienteDto()
        {
            Id = 1,
            Saldo = 450,
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
        ListTransacao = _originalListTransacao.SelectMany(x => x.TransacaoItem).ToList();
        labelPontos.Text = _adm.Saldo.ToString() + "Pnts";
        BindingContext = this;
    }
    // Implementação de INotifyPropertyChanged.
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}