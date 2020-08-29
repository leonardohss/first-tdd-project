using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using System;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private readonly CursoDto _cursoDto;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;

        public ArmazenadorDeCursoTest()
        {
            var fake = new Faker();

            _cursoDto = new CursoDto
            {
                Nome = fake.Random.Words(),
                CargaHoraria = fake.Random.Double(50, 1000),
                PublicoAlvo = "Estudante",
                Valor = fake.Random.Double(1000, 2000),
                Descricao = fake.Lorem.Paragraph()
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();

            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorDeCurso.Armazenar(_cursoDto);

            _cursoRepositorioMock.Verify(r => r.Adicionar(
                It.Is<Curso>(
                    c => c.Nome == _cursoDto.Nome && 
                    c.Descricao == _cursoDto.Descricao
                    )
                ));
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            var publicoAlvoInvalido = "Médico";
            _cursoDto.PublicoAlvo = publicoAlvoInvalido;

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem("Publico Alvo inválido");
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem("Nome do curso já consta no banco de dados");

        }
    }
}
