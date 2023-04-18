using Gadz.Crawler.Imobiliario.Application.UseCases;
using Gadz.Crawler.Imobiliario.Domain.Ofertas;
using Gadz.Crawler.Imobiliario.Domain.Pesquisas;
using Moq;

namespace Gadz.Crawler.Imobiliario.Application.Tests
{
    public class BuscarOfertasUseCaseTests
    {
        private Mock<IOfertaRepository> _ofertaRepository;
        private Mock<IPesquisasRepository> _pesquisasRepository;
        private Mock<IPlataformaDeBusca> _plataformaDeBusca;
        private BuscarOfertasUseCase _usecase;
        private BuscarOfertasInput _input;

        [SetUp]
        public void Setup()
        {
            _ofertaRepository = new();
            _pesquisasRepository = new();
            _plataformaDeBusca = new();
            _input = new()
            {
                Estado = "MG",
                Cidade = "Belo Horizonte"
            };
            _usecase = new BuscarOfertasUseCase(_ofertaRepository.Object, _pesquisasRepository.Object, _plataformaDeBusca.Object);

            // mocksetup
            _plataformaDeBusca.Setup(s => s.BuscarOfertas(It.IsAny<CriteriosDeBusca>())).ReturnsAsync(new Pesquisa
            {

            });
        }

        [Test]
        public void New_WhenInstanced_ShouldNotBeNull()
        {
            Assert.That(_usecase, Is.Not.Null);
        }

        [Test]
        public async Task Executar_WhenCalled_ShouldReturnAnOutput()
        {
            var output = await _usecase.Executar(_input);

            Assert.That(output, Is.Not.Null);
        }

        [Test]
        public async Task Executar_WhenCalled_ShouldSavePesquisaInDatabase()
        {
            var output = await _usecase.Executar(_input);

            _pesquisasRepository.Verify(s => s.Add(It.IsAny<Pesquisa>()), Times.Once);
        }

        [Test]
        public async Task Executar_WhenCalled_ShouldSaveOfertasInDatabase()
        {
            var output = await _usecase.Executar(_input);

            _ofertaRepository.Verify(s => s.AddRange(It.IsAny<IEnumerable<Oferta>>()), Times.Once);
        }

        [Test]
        public async Task Executar_WhenFailed_ShouldTriggerOnErrorEvent()
        {
            _plataformaDeBusca.Setup(s => s.BuscarOfertas(It.IsNotNull<CriteriosDeBusca>())).ThrowsAsync(new Exception());
            Exception exception = null;

            _usecase.OnErrorOccurred += (ex) =>
            {
                exception = ex;
            };

            var output = await _usecase.Executar(_input);

            Assert.That(exception, Is.Not.Null);
        }
    }
}